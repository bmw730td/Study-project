using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private SelfReturner _objectPrefab;
    [SerializeField, Min(0)] private int _maxPoolSize;

    private Queue<SelfReturner> _pool = new();
    private Queue<SelfReturner> _createdObjects = new();

    public event Action<SelfReturner> CreatedNewObject;
    public event Action<SelfReturner> WillSpawnObject;

    protected event Action GameStarted;

    public void Reset()
    {
        while (_createdObjects.Count > 0)
        {
            SelfReturner obj = _createdObjects.Dequeue();

            obj.gameObject.SetActive(true);

            if (obj.gameObject.TryGetComponent(out ObjectSpawner spawner))
                spawner.Reset();

            Destroy(obj.gameObject);
        }

        _pool.Clear();
        GameStarted?.Invoke();
    }

    public SelfReturner SpawnObject()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(CreateObject());

        ResetObject(_pool.Peek());
        WillSpawnObject?.Invoke(_pool.Peek());
        _pool.Peek().gameObject.SetActive(true);

        return _pool.Dequeue();
    }

    private SelfReturner CreateObject()
    {
        SelfReturner obj = Instantiate(_objectPrefab);
        
        obj.ShouldBeReturned += PutObject;
        _createdObjects.Enqueue(obj);
        CreatedNewObject?.Invoke(obj);

        return obj;
    }

    private void ResetObject(SelfReturner obj)
    {
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;

        if (obj.TryGetComponent(out Rigidbody2D rigidbody))
            rigidbody.velocity = Vector2.zero;
    }

    private void PutObject(SelfReturner obj)
    {
        obj.gameObject.SetActive(false);

        _pool.Enqueue(obj);

        while (_pool.Count > _maxPoolSize)
        {
            Destroy(_pool.Dequeue());
        }
    }
}
