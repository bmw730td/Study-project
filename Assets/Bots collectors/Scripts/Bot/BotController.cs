using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(ResourceCarrier))]

public class BotController : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _maxInteractionRange;

    private BotMover _mover;
    private ResourceCarrier _carrier;

    private Coroutine _bringingCoroutine;
    private WaitUntil _waitUntilTargetReached;

    public event Action<BotController> ResourceBrought;

    public bool IsBusy { get; private set; }
    public Resource TargetResource { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
        _carrier = GetComponent<ResourceCarrier>();

        IsBusy = false;
        TargetResource = null;
    }

    public Resource ReleaseResource() => _carrier.ReleaseResource();

    public void StartBringingResource(Resource target, Transform to)
    {
        if (_bringingCoroutine != null)
            StopCoroutine(_bringingCoroutine);

        _bringingCoroutine = StartCoroutine(BringResource(target, to));
    }

    private IEnumerator BringResource(Resource target, Transform to)
    {
        TargetResource = target;
        IsBusy = true;
        _mover.StartMoving(target.transform, _maxInteractionRange);
        _waitUntilTargetReached ??= new WaitUntil(() => _mover.TargetReached);

        yield return _waitUntilTargetReached;

        _carrier.GrabResource(target);
        _mover.StartMoving(to, _maxInteractionRange);

        yield return _waitUntilTargetReached;

        IsBusy = false;

        ResourceBrought?.Invoke(this);
        TargetResource = null;
    }
}
