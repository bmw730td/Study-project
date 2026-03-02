using System.Collections;
using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed;

    private Coroutine _movingCoroutine;
    private WaitForFixedUpdate _waitFixedUpdate;

    public bool TargetReached { get; private set; }

    private void Awake()
    {
        TargetReached = false;
    }

    public void StartMoving(Transform target, float maxDistance = 0f)
    {
        if (_movingCoroutine != null)
            StopCoroutine(_movingCoroutine);

        _movingCoroutine = StartCoroutine(Move(target, maxDistance));
    }

    private IEnumerator Move(Transform target, float maxDistance)
    {
        _waitFixedUpdate ??= new WaitForFixedUpdate();
        TargetReached = false;

        while ((target.position - transform.position).sqrMagnitude > maxDistance * maxDistance)
        {
            yield return _waitFixedUpdate;

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.fixedDeltaTime);
        }

        TargetReached = true;
    }
}
