using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]
[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(StorageChecker))]
[RequireComponent(typeof(ScanProcessor))]

public class BotSender : MonoBehaviour
{
    private readonly int BaseResourceGain = 0;

    private ObjectSpawner _spawner;
    private ResourceStorage _storage;
    private StorageChecker _storageChecker;
    private ScanProcessor _scanProcessor;

    private List<BotController> _bots;

    private Coroutine _sendingCoroutine;
    private Dictionary<ResourceType, int> _resourceGain;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
        _storage = GetComponent<ResourceStorage>();
        _storageChecker = GetComponent<StorageChecker>();
        _scanProcessor = GetComponent<ScanProcessor>();

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

    private void StartSendingBots()
    {
        if (_sendingCoroutine != null)
            StopCoroutine(_sendingCoroutine);

        _sendingCoroutine = StartCoroutine(SendBots());
    }

    private IEnumerator SendBots()
    {
        UpdateResourceGain(_storageChecker.GetRequiredResources().Keys);

        BotController freeBot;

        while (CheckIfRequireResources())
        {
            yield return null;

            freeBot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

            if (freeBot != null)
            {
                if (GetWantedResource(out Resource wantedResource))
                {
                    _resourceGain[wantedResource.Type] += wantedResource.Value;
                    freeBot.ResourceBrought += ProcessBot;
                    freeBot.StartBringingResource(wantedResource, transform);
                }
            }
        }
    }

    public bool CheckIfRequireResources()
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
        _bots.Clear();

        if (_spawner.CreatedObjects != null)
        {
            foreach (ReturnAnnouncer obj in _spawner.CreatedObjects)
            {
                if (obj.TryGetComponent(out BotController bot))
                    _bots.Add(bot);
            }
        }
    }

    private void AddBot(ReturnAnnouncer obj)
    {
        if (obj.TryGetComponent(out BotController bot))
        {
            if (_bots.Contains(bot) == false)
            {
                _bots.Add(bot);
            }
        }
    }

    private void RemoveBot(ReturnAnnouncer obj)
    {
        if (obj.TryGetComponent(out BotController bot))
        {
            if (_bots.Contains(bot))
            {
                _bots.Remove(bot);
            }
        }
    }

    private void ProcessBot(BotController bot)
    {
        bot.ResourceBrought -= ProcessBot;

        Resource broughtResource = bot.ReleaseResource();

        _resourceGain[broughtResource.Type] -= broughtResource.Value;
        _storage.PutResourceIn(broughtResource);
    }

    private void UpdateResourceGain(IEnumerable<ResourceType> trackingTypes)
    {
        _resourceGain.Clear();

        foreach (ResourceType type in trackingTypes)
        {
            _resourceGain.Add(type, BaseResourceGain);
        }

        foreach (BotController bot in _bots)
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
