using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ReturnAnnouncer _objectPrefab;
    [SerializeField, Min(0)] private int _maxPoolSize;

    private ObjectPool _pool;
    private Queue<ReturnAnnouncer> _createdObjects;

    public event Action<ReturnAnnouncer> CreatedNewObject;
    public event Action<ReturnAnnouncer> WillSpawnObject;

    public Queue<ReturnAnnouncer> CreatedObjects => _createdObjects;

    private void Awake()
    {
        _pool = new(_objectPrefab, transform, ProcessNewObject, _maxPoolSize);
        _createdObjects = new();
    }

    public void SpawnObject()
    {
        ReturnAnnouncer obj = _pool.GetObject();
        
        WillSpawnObject?.Invoke(obj);
        obj.gameObject.SetActive(true);
    }

    private void ProcessNewObject(ReturnAnnouncer newObject)
    {
        _createdObjects.Enqueue(newObject);
        CreatedNewObject?.Invoke(newObject);
    }
}
