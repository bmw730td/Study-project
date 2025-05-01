using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ReturnAnnouncer _objectPrefab;
    [SerializeField, Min(0)] private int _maxPoolSize;

    private Queue<ReturnAnnouncer> _pool = new();
    private Queue<ReturnAnnouncer> _createdObjects = new();

    public event Action<ReturnAnnouncer> CreatedNewObject;
    public event Action<ReturnAnnouncer> WillSpawnObject;

    public Queue<ReturnAnnouncer> CreatedObjects => _createdObjects;

    public virtual void Reset()
    {
        ResetPool();
    }

    public ReturnAnnouncer SpawnObject()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(CreateObject());

        ResetObject(_pool.Peek());
        WillSpawnObject?.Invoke(_pool.Peek());
        _pool.Peek().gameObject.SetActive(true);

        return _pool.Dequeue();
    }

    protected void ResetPool()
    {
        while (_createdObjects.Count > 0)
        {
            ReturnAnnouncer obj = _createdObjects.Dequeue();

            obj.gameObject.SetActive(true);

            if (obj.gameObject.TryGetComponent(out ObjectSpawner spawner))
                spawner.Reset();

            Destroy(obj.gameObject);
        }

        _pool.Clear();
    }

    private ReturnAnnouncer CreateObject()
    {
        ReturnAnnouncer obj = Instantiate(_objectPrefab);

        obj.ShouldBeReturned += PutObject;
        _createdObjects.Enqueue(obj);
        CreatedNewObject?.Invoke(obj);

        return obj;
    }

    private void ResetObject(ReturnAnnouncer obj)
    {
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;

        if (obj.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.velocity = Vector2.zero;
    }

    private void PutObject(ReturnAnnouncer obj)
    {
        obj.gameObject.SetActive(false);

        _pool.Enqueue(obj);

        while (_pool.Count > _maxPoolSize)
        {
            Destroy(_pool.Dequeue());
        }
    }
}
