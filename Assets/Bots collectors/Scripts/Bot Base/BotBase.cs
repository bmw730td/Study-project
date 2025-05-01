using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]
[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ResourceStorage))]

public class BotBase : MonoBehaviour
{
    private readonly int InitialExpectedValue = 0;
    
    [SerializeField, Min(0f)] private float _scanCooldown;
    [SerializeField, Min(0f)] private float _resourceCheckInterval;
    
    private ObjectSpawner _spawner;
    private ResourceScanner _scanner;
    private ResourceStorage _storage;

    private List<int> _expectedValues;
    private List<Resource> _knownResources;
    private List<Resource> _ignoringResources;
    private bool _needToScan;

    private WaitForSeconds _waitScanCooldown;
    private WaitUntil _waitUntilScanRequired;
    private WaitForSeconds _waitResourseCheckInterval;

    private Coroutine _scanningCoroutine;
    private Coroutine _checkingCoroutine;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
        _scanner = GetComponent<ResourceScanner>();
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

        if (_expectedValues == null)
        {
            _expectedValues = new();
            
            foreach (ResourceStorageUnit unit in _storage.Units)
            {
                _expectedValues.Add(InitialExpectedValue);
            }
        }

        _knownResources ??= new();
        _ignoringResources ??= new();
        _needToScan = false;

        StartScanningCoroutine();
        StartCheckingCoroutine();
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

    private void StartScanningCoroutine()
    {
        if (_scanningCoroutine != null)
            StopCoroutine(_scanningCoroutine);

        _scanningCoroutine = StartCoroutine(DoScans());
    }

    private void StartCheckingCoroutine()
    {
        if (_checkingCoroutine != null)
            StopCoroutine(_checkingCoroutine);

        _checkingCoroutine = StartCoroutine(CheckStorage());
    }

    private void OnTaskDone(ResourceBringer bringer)
    {
        Resource broughtResource = bringer.GiveResource();

        DecreaseExpectedValues(broughtResource.Type);
        _ignoringResources.Remove(broughtResource);
        _storage.PutResourceIn(broughtResource);

        if (broughtResource.TryGetComponent(out ReturnAnnouncer announcer))
            announcer.AnnounceReturn();
    }

    private void OnTaskFailed(EnumResourceType targetedType, Resource targetedResource)
    {
        DecreaseExpectedValues(targetedType);

        if (targetedResource != null && targetedResource.IsGrabbable)
            _knownResources.Add(targetedResource);
    }

    private void SubscribeNewBot(ReturnAnnouncer announcer)
    {
        if (announcer.TryGetComponent(out ResourceBringer bringer))
        {
            bringer.TaskDone += OnTaskDone;
            bringer.TaskFailed += OnTaskFailed;
        }
    }

    private void DecreaseExpectedValues(EnumResourceType type)
    {
        for (int i = 0; i < _storage.Units.Count; i++)
        {
            if (_storage.Units[i].Type == type)
            {
                _expectedValues[i] -= Resource.Value;

                return;
            }
        }
    }

    private bool TrySendBot(Resource target)
    {
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

    private IEnumerator DoScans()
    {
        _waitScanCooldown ??= new WaitForSeconds(_scanCooldown);
        _waitUntilScanRequired ??= new WaitUntil(() => _needToScan);

        while (enabled)
        {
            if (_needToScan == false)
                yield return _waitUntilScanRequired;

            foreach (Resource resourse in _scanner.ScanGround())
            {
                if (_knownResources.Contains(resourse) == false && _ignoringResources.Contains(resourse) == false)
                    _knownResources.Add(resourse);
            }

            _needToScan = false;

            yield return _waitScanCooldown;
        }
    }

    private IEnumerator CheckStorage()
    {
        _waitResourseCheckInterval ??= new WaitForSeconds(_resourceCheckInterval);

        while (enabled)
        {
            for (int i = 0; i < _storage.Units.Count; i++)
            {
                if (_storage.Units[i].Amount + _expectedValues[i] < _storage.Units[i].Capacity)
                {
                    int index = 0;

                    while (index < _knownResources.Count)
                    {
                        if (_knownResources[index].Type == _storage.Units[i].Type)
                        {
                            if (TrySendBot(_knownResources[index]))
                            {
                                _expectedValues[i] += Resource.Value;
                                _ignoringResources.Add(_knownResources[index]);
                                _knownResources.RemoveAt(index);
                            }
                            else
                            {
                                index++;
                            }
                        }
                        else
                        {
                            index++;
                        }
                    }

                    if (_needToScan == false)
                        _needToScan = _storage.Units[i].Amount + _expectedValues[i] < _storage.Units[i].Capacity;
                }
            }

            yield return _waitResourseCheckInterval;
        }
    }
}
