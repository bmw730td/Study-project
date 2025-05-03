using System;
using System.Collections;
using UnityEngine;

public class ResourceBringer : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed;
    [SerializeField, Min(0f)] private float _minInteractionRange;

    private Resource _targetResource;
    private Resource _resourceInHands;

    private Transform _target;
    private Coroutine _bringingCoroutine;
    private Coroutine _movingCoroutine;
    private WaitUntil _waitUntil;
    private WaitForFixedUpdate _waitFixed;

    private bool _needToSwitchTarget;
    private bool _isTaskFailed;

    public EnumResourceType TargetedType { get; private set; }
    public bool IsBusy { get; private set; }
    public Resource Resource => _resourceInHands;


    public event Action<ResourceBringer> TaskDone;
    public event Action<EnumResourceType, Resource> TaskFailed;

    private void Awake()
    {
        _waitUntil = new WaitUntil(() => _needToSwitchTarget);
        _waitFixed = new WaitForFixedUpdate();
        IsBusy = false;
    }

    public Resource GiveResource()
    {
        Resource resourceToGive = _resourceInHands;
        _resourceInHands = null;
        resourceToGive.transform.SetParent(null);

        return resourceToGive;
    }

    public void StartBringingResource(Resource resource, Transform to)
    {
        _isTaskFailed = false;
        TargetedType = resource.Type;
        _targetResource = resource;

        if (_bringingCoroutine != null)
            StopCoroutine(_bringingCoroutine);

        _bringingCoroutine = StartCoroutine(BringResource(to));
    }

    private void StartMovingTo(Transform target, Func<bool> failCheckFunc = null)
    {
        if (_movingCoroutine != null)
            StopCoroutine(_movingCoroutine);

        _movingCoroutine = StartCoroutine(MoveTo(target, failCheckFunc));
    }

    private IEnumerator BringResource(Transform to)
    {
        IsBusy = true;
        _needToSwitchTarget = false;
        _target = _targetResource.transform;
        StartMovingTo(_target, CheckResourceUngrabbability);

        yield return _waitUntil;

        if (_isTaskFailed == false)
            GrabResource();

        _needToSwitchTarget = false;
        _target = to;
        StartMovingTo(_target);

        yield return _waitUntil;

        _target = null;
        IsBusy = false;

        if (_isTaskFailed == false)
        {
            TaskDone?.Invoke(this);
        }
    }

    private IEnumerator MoveTo(Transform target, Func<bool> failCheckFunc)
    {
        while (Vector3.Distance(transform.position, target.position) > _minInteractionRange)
        {
            yield return _waitFixed;
            
            if (failCheckFunc != null && failCheckFunc())
            {
                TaskFailed?.Invoke(TargetedType, _targetResource);
                _needToSwitchTarget = true;
                _isTaskFailed = true;

                continue;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.fixedDeltaTime);
        }

        _needToSwitchTarget = true;
    }

    private bool CheckResourceUngrabbability()
    {
        return _targetResource == null || _targetResource.IsGrabbable == false;
    }

    private void GrabResource()
    {
        if (Vector3.Distance(transform.position, _targetResource.transform.position) <= _minInteractionRange && _targetResource.IsGrabbable)
        {
            _resourceInHands = _targetResource;
            _targetResource.transform.SetParent(transform);
        }
        else
        {
            TaskFailed?.Invoke(TargetedType, _targetResource);
            _isTaskFailed = true;
        }
    }
}
