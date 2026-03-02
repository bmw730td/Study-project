using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class ResourceTypeSetter : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    
    private ObjectSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
    }

    private void OnEnable()
    {
        _spawner.WillSpawnObject += SetResourceType;
    }

    private void OnDisable()
    {
        _spawner.WillSpawnObject -= SetResourceType;
    }

    private void SetResourceType(ReturnAnnouncer obj)
    {
        if (obj.TryGetComponent(out Resource resource))
        {
            resource.SetType(_type);
        }
    }
}
