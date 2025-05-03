using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]
[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ScanProcessor))]
[RequireComponent(typeof(ResourceStorage))]

public class BotBase : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _scanCooldown;
    [SerializeField, Min(0f)] private float _resourceCheckInterval;
    
    private ObjectSpawner _spawner;
    private ResourceScanner _scanner;
    private ScanProcessor _scanProcessor;
    private ResourceStorage _storage;

    private bool _needToScan;
    private WaitUntil _waitUntilScanRequired;
    private WaitForSeconds _waitScanCooldown;

    private WaitForSeconds _waitResourseCheckInterval;

    private Coroutine _scanningCoroutine;
    private Coroutine _fillingCoroutine;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
        _scanner = GetComponent<ResourceScanner>();
        _scanProcessor = GetComponent<ScanProcessor>();
        _storage = GetComponent<ResourceStorage>();
    }

    private void OnEnable()
    {
        foreach (ReturnAnnouncer announcer in _spawner.CreatedObjects)
        {
            if (announcer != null && announcer.TryGetComponent(out ResourceBringer bringer))
            {
                bringer.TaskDone += OnTaskDone;
                bringer.TaskFailed += OnTaskFailed;
            }
        }

        _spawner.CreatedNewObject += SubscribeNewBot;

        _needToScan = false;

        StartScanningGround();
        StartFillingStorage();
    }

    private void OnDisable()
    {
        foreach (ReturnAnnouncer announcer in _spawner.CreatedObjects)
        {
            if (announcer != null && announcer.TryGetComponent(out ResourceBringer bringer))
            {
                bringer.TaskDone -= OnTaskDone;
                bringer.TaskFailed -= OnTaskFailed;
            }
        }

        _spawner.CreatedNewObject -= SubscribeNewBot;
    }

    private void StartScanningGround()
    {
        if (_scanningCoroutine != null)
            StopCoroutine(_scanningCoroutine);

        _scanningCoroutine = StartCoroutine(DoScans());
    }

    private void StartFillingStorage()
    {
        if (_fillingCoroutine != null)
            StopCoroutine(_fillingCoroutine);

        _fillingCoroutine = StartCoroutine(FillStorageOnNeed());
    }

    private void OnTaskDone(ResourceBringer bringer)
    {
        Resource broughtResource = bringer.GiveResource();

        _storage.PutResourceIn(broughtResource);

        if (broughtResource.TryGetComponent(out ReturnAnnouncer announcer))
            announcer.AnnounceReturn();
    }

    private void OnTaskFailed(EnumResourceType targetedType, Resource targetedResource)
    {
        _storage.TypeSlotPairs[targetedType].ChangeExpectedAmount(Resource.Value * -1);

        if (targetedResource != null)
        {
            targetedResource.UnReserve();
            
            if (targetedResource.IsGrabbable)
            {
                if (_scanProcessor.KnownResources.ContainsKey(targetedResource.Type) == false)
                    _scanProcessor.KnownResources.Add(targetedResource.Type, new());

                _scanProcessor.KnownResources[targetedResource.Type].Add(targetedResource);
            }
        }
    }

    private void SubscribeNewBot(ReturnAnnouncer announcer)
    {
        if (announcer.TryGetComponent(out ResourceBringer bringer))
        {
            bringer.TaskDone += OnTaskDone;
            bringer.TaskFailed += OnTaskFailed;
        }
    }

    private bool TrySendBot(Resource target)
    {
        if (target.IsReserved)
            return false;

        foreach (ReturnAnnouncer announcer in _spawner.CreatedObjects)
        {
            if (announcer.TryGetComponent(out ResourceBringer bringer) && bringer.IsBusy == false)
            {
                bringer.StartBringingResource(target, transform);

                return true;
            }
        }

        return false;
    }

    private bool TryFillStorage(EnumResourceType type)
    {
        if (_scanProcessor.KnownResources.ContainsKey(type))
        {
            foreach (Resource resource in _scanProcessor.KnownResources[type])
            {
                if (TrySendBot(resource))
                {
                    _storage.TypeSlotPairs[type].ChangeExpectedAmount(Resource.Value);
                    resource.Reserve();

                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator DoScans()
    {
        _waitScanCooldown ??= new WaitForSeconds(_scanCooldown);
        _waitUntilScanRequired ??= new WaitUntil(() => _needToScan);

        while (enabled)
        {
            yield return _waitUntilScanRequired;

            _scanner.ScanGround();
            _needToScan = false;

            yield return _waitScanCooldown;
        }
    }

    private IEnumerator FillStorageOnNeed()
    {
        _waitResourseCheckInterval ??= new WaitForSeconds(_resourceCheckInterval);

        while (enabled)
        {
            if (_storage.IsFull == false)
                _storage.TryFillSelf(TryFillStorage);

            if (_needToScan == false)
                _needToScan = _storage.IsFull == false;

            yield return _waitResourseCheckInterval;
        }
    }
}
