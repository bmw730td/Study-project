using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ReturnAnnouncer _objectPrefab;
    [SerializeField, Min(0)] private int _maxPoolSize;

    private ObjectPool _pool;
    private List<ReturnAnnouncer> _createdObjects;

    public event Action<ReturnAnnouncer> CreatedNewObject;
    public event Action<ReturnAnnouncer> WillSpawnObject;
    public event Action<ReturnAnnouncer> WillDestroyObject;

    public List<ReturnAnnouncer> CreatedObjects => _createdObjects;

    private void Awake()
    {
        _pool = new(_objectPrefab, transform, ProcessNewObject, RemoveObject, _maxPoolSize);
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
        _createdObjects.Add(newObject);
        CreatedNewObject?.Invoke(newObject);
    }

    private void RemoveObject(ReturnAnnouncer objectToDelete)
    {
        if (_createdObjects.Contains(objectToDelete))
        {
            _createdObjects.Remove(objectToDelete);
            WillDestroyObject?.Invoke(objectToDelete);
        }
    }
}
