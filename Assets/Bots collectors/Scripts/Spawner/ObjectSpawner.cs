using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ReturnAnnouncer _objectPrefab;
    [SerializeField, Min(0)] private int _maxPoolSize;

    private ObjectPool<ReturnAnnouncer> _pool;
    private List<ReturnAnnouncer> _createdObjects;

    public event Action<ReturnAnnouncer> CreatedNewObject;
    public event Action<ReturnAnnouncer> WillSpawnObject;
    public event Action<ReturnAnnouncer> WillDestroyObject;
    public event Action ReceivedObject;

    public int ActiveObjectsAmount => _createdObjects.Count - _pool.Count;
    public bool WillDestroyNewObject => ActiveObjectsAmount >= _maxPoolSize;

    private void Awake()
    {
        _pool = new(CreateNewObject, DestroyObject, PrepareObject, ResetObject, _maxPoolSize);
        _createdObjects = new();
    }

    public void SpawnObject()
    {
        _pool.GetObject().gameObject.SetActive(true);
    }

    public List<ReturnAnnouncer> GetCreatedObjects()
    {
        _createdObjects ??= new();

        return _createdObjects.ToList();
    }

    private ReturnAnnouncer CreateNewObject()
    {
        ReturnAnnouncer newObject = Instantiate(_objectPrefab, position: transform.position, _objectPrefab.transform.rotation);

        _createdObjects.Add(newObject);
        newObject.ShouldReturn += _pool.PutObject;
        CreatedNewObject?.Invoke(newObject);

        return newObject;
    }

    private void PrepareObject(ReturnAnnouncer obj)
    {
        obj.transform.SetPositionAndRotation(transform.position, _objectPrefab.transform.rotation);
        WillSpawnObject?.Invoke(obj);
    }

    private void ResetObject(ReturnAnnouncer obj)
    {
        obj.gameObject.SetActive(false);
        ReceivedObject?.Invoke();
    }

    private void DestroyObject(ReturnAnnouncer obj)
    {
        _createdObjects.Remove(obj);
        obj.ShouldReturn -= _pool.PutObject;
        WillDestroyObject?.Invoke(obj);
        Destroy(obj.gameObject);
    }
}
