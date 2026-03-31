using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(ResourceCarrier))]

public class Bot : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _maxInteractionRange;
    [SerializeField] private BotSender _botBasePrefab;

    private BotMover _mover;
    private ResourceCarrier _carrier;

    private Coroutine _bringingCoroutine;
    private WaitUntil _waitUntilTargetReached;

    public event Action<Bot> TaskDone;

    public bool IsBusy { get; private set; }
    public Resource TargetResource { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _carrier = GetComponent<ResourceCarrier>();

        _waitUntilTargetReached = new WaitUntil(() => _mover.TargetReached);
        IsBusy = false;
        TargetResource = null;
    }

    public Resource ReleaseResource()
    {
        TargetResource = null;

        return _carrier.ReleaseResource();
    }

    public void StartBringingResource(Resource target, Transform to)
    {
        if (_bringingCoroutine != null)
            StopCoroutine(_bringingCoroutine);

        _bringingCoroutine = StartCoroutine(BringResource(target, to));
    }

    public void StartBuildingBase(Vector3 target, BotSender currentBase)
    {
        if (_bringingCoroutine != null)
            StopCoroutine(_bringingCoroutine);

        _bringingCoroutine = StartCoroutine(BuildBase(target, currentBase));
    }

    private IEnumerator BringResource(Resource target, Transform to)
    {
        TargetResource = target;
        IsBusy = true;
        _mover.StartMoving(target.transform, _maxInteractionRange);

        yield return _waitUntilTargetReached;

        _carrier.GrabResource(target);
        _mover.StartMoving(to, _maxInteractionRange);

        yield return _waitUntilTargetReached;

        IsBusy = false;
        TaskDone?.Invoke(this);
    }

    private IEnumerator BuildBase(Vector3 target, BotSender currentBase)
    {
        IsBusy = true;
        _mover.StartMoving(target, _maxInteractionRange);

        yield return _waitUntilTargetReached;

        BotSender newBase = Instantiate(_botBasePrefab, target, _botBasePrefab.transform.rotation);

        newBase.SetScanProcessor(currentBase.ScanProcessor);

        IsBusy = false;
        TaskDone?.Invoke(this);
    }
}
