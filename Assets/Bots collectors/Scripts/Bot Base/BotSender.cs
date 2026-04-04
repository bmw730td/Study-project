using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]
[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(StorageChecker))]

public class BotSender : MonoBehaviour
{
    private readonly int BaseResourceGain = 0;

    [SerializeField] private ScanProcessor _scanProcessor;

    private ObjectSpawner _spawner;
    private ResourceStorage _storage;
    private StorageChecker _storageChecker;

    private List<Bot> _bots;

    private Coroutine _sendingCoroutine;
    private Dictionary<ResourceType, int> _resourceGain;

    public event Action<Bot> SentBotBuildBase;

    public ScanProcessor ScanProcessor => _scanProcessor;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
        _storage = GetComponent<ResourceStorage>();
        _storageChecker = GetComponent<StorageChecker>();

        _bots = new();
        _resourceGain = new();
    }

    private void OnEnable()
    {
        UpdateBotList();

        _spawner.CreatedNewObject += AddBot;
        _spawner.WillDestroyObject += RemoveBot;

        _storageChecker.GoalSet += StartSendingBots;
    }

    private void OnDisable()
    {
        _spawner.CreatedNewObject -= AddBot;
        _spawner.WillDestroyObject -= RemoveBot;

        _storageChecker.GoalSet += StartSendingBots;

        if (_sendingCoroutine != null)
            StopCoroutine(_sendingCoroutine);
    }

    public void SetScanProcessor(ScanProcessor processor)
    {
        _scanProcessor = processor;
    }

    public void StartBuildingBase(Vector3 target)
    {
        _storageChecker.GoalSet -= StartSendingBots;

        if (_sendingCoroutine != null)
            StopCoroutine(_sendingCoroutine);

        _sendingCoroutine = StartCoroutine(SendBotBuildBase(target));
    }

    private IEnumerator SendBotBuildBase(Vector3 target)
    {
        Bot freeBot;

        while(TryGetFreeBot(out freeBot) == false)
        {
            yield return null;
        }

        freeBot.StartBuildingBase(target, this);
        freeBot.TaskDone += DestroyBot;
        SentBotBuildBase?.Invoke(freeBot);

        _storageChecker.GoalSet += StartSendingBots;
        StartSendingBots();
    }

    private void StartSendingBots()
    {
        if (_sendingCoroutine != null)
            StopCoroutine(_sendingCoroutine);

        _sendingCoroutine = StartCoroutine(SendBotsAfterResources());
    }

    private IEnumerator SendBotsAfterResources()
    {
        UpdateResourceGain(_storageChecker.GetRequiredResources().Keys);

        Bot freeBot;

        while (CheckIfRequireResources())
        {
            yield return null;

            freeBot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

            if (TryGetFreeBot(out freeBot))
            {
                if (GetWantedResource(out Resource wantedResource))
                {
                    _resourceGain[wantedResource.Type] += wantedResource.Value;
                    freeBot.TaskDone += StoreBotResource;
                    freeBot.StartBringingResource(wantedResource, transform);
                }
            }
        }
    }

    private bool TryGetFreeBot(out Bot freeBot)
    {
        freeBot = _bots.FirstOrDefault(bot => bot.IsBusy == false && bot.gameObject.activeSelf == true);

        return freeBot != null;
    }

    private bool CheckIfRequireResources()
    {
        foreach (ResourceType type in _storageChecker.GetRequiredResources().Keys)
        {
            if (_storageChecker.GetRequiredAmount(type) > _resourceGain[type])
                return true;
        }

        return false;
    }

    private bool GetWantedResource(out Resource resource)
    {
        resource = null;

        foreach (ResourceType type in _storageChecker.GetRequiredResources().Keys)
        {
            if (_storageChecker.GetRequiredAmount(type) > _resourceGain[type])
            {
                resource = _scanProcessor.GetKnownResource(type);

                if (resource != null)
                    return true;
            }
        }

        return false;
    }

    private void UpdateBotList()
    {
        List<ReturnAnnouncer> createdObjects = _spawner.GetCreatedObjects();
        
        _bots.Clear();

        if (createdObjects != null)
        {
            foreach (ReturnAnnouncer obj in createdObjects)
            {
                if (obj.TryGetComponent(out Bot bot))
                    _bots.Add(bot);
            }
        }
    }

    private void AddBot(ReturnAnnouncer obj)
    {
        if (obj.TryGetComponent(out Bot bot))
        {
            if (_bots.Contains(bot) == false)
            {
                _bots.Add(bot);
            }
        }
    }

    private void RemoveBot(ReturnAnnouncer obj)
    {
        if (obj.TryGetComponent(out Bot bot))
        {
            if (_bots.Contains(bot))
            {
                _bots.Remove(bot);
            }
        }
    }

    private void StoreBotResource(Bot bot)
    {
        bot.TaskDone -= StoreBotResource;

        Resource broughtResource = bot.ReleaseResource();

        _resourceGain[broughtResource.Type] -= broughtResource.Value;
        _storage.PutResourceIn(broughtResource);
    }

    private void DestroyBot(Bot bot)
    {
        bot.TaskDone -= DestroyBot;

        if (bot.TryGetComponent(out ReturnAnnouncer announcer))
        {
            announcer.InvokeReturn();
        }
        else
        {
            Destroy(bot.gameObject);
        }
    }

    private void UpdateResourceGain(IEnumerable<ResourceType> trackingTypes)
    {
        _resourceGain.Clear();

        foreach (ResourceType type in trackingTypes)
        {
            _resourceGain.Add(type, BaseResourceGain);
        }

        foreach (Bot bot in _bots)
        {
            if (bot.TargetResource != null)
            {
                if (_resourceGain.ContainsKey(bot.TargetResource.Type))
                {
                    _resourceGain[bot.TargetResource.Type] += bot.TargetResource.Value;
                }
            }
        }
    }
}
