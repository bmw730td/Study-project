using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Queue<ReturnAnnouncer> _pool;

    private ReturnAnnouncer _objectPrefab;
    private Transform _spawnPoint;
    private Action<ReturnAnnouncer> _onObjectCreated;
    private int _capacity;

    public ObjectPool(ReturnAnnouncer objectPrefab, Transform spawnPoint, Action<ReturnAnnouncer> onObjectCreated, int capacity)
    {
        _objectPrefab = objectPrefab;
        _spawnPoint = spawnPoint;
        _onObjectCreated = onObjectCreated;
        _capacity = capacity;

        _pool = new();
    }

    public ReturnAnnouncer GetObject()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(CreateObject());

        ResetObject(_pool.Peek());

        return _pool.Dequeue();
    }

    private ReturnAnnouncer CreateObject()
    {
        ReturnAnnouncer obj = UnityEngine.Object.Instantiate(_objectPrefab);

        obj.ShouldBeReturned += PutObject;
        _onObjectCreated?.Invoke(obj);

        return obj;
    }

    private void ResetObject(ReturnAnnouncer obj)
    {
        obj.transform.SetPositionAndRotation(_spawnPoint.position, _spawnPoint.rotation);

        if (obj.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.velocity = Vector2.zero;
    }

    private void PutObject(ReturnAnnouncer obj)
    {
        obj.gameObject.SetActive(false);

        _pool.Enqueue(obj);

        while (_pool.Count > _capacity)
        {
            UnityEngine.Object.Destroy(_pool.Dequeue());
        }
    }
}
