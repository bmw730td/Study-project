using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class ResourceConfigSetter : MonoBehaviour
{
    [SerializeField] private ResourceConfig _config;
    
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
            resource.SetConfig(_config);
        }
    }
}
