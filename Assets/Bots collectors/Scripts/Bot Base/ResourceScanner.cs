using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private MeshCollider _ground;

    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _groundLayer;

    public List<Resource> ScanGround()
    {
        List<Resource> resourceList = new();
        Vector3 scanPosition = new Vector3(
            Random.Range(_ground.bounds.min.x, _ground.bounds.max.x),
            _ground.bounds.max.y,
            Random.Range(_ground.bounds.min.z, _ground.bounds.max.z));

        Physics.Raycast(scanPosition, Vector3.down, out RaycastHit hitInfo, _ground.bounds.max.y - _ground.bounds.min.y, _groundLayer);
        scanPosition = hitInfo.point;

        Collider[] hits = Physics.OverlapSphere(scanPosition, _scanRadius);
        
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
                resourceList.Add(resource);
        }

        return resourceList;
    }
}
