using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private MeshCollider _ground;

    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _groundLayer;

    public event Action ScanCompleted;

    public List<Resource> LastResults { get; private set; }

    public void ScanGround()
    {
        LastResults = new();
        Vector3 scanPosition = new Vector3(
            UnityEngine.Random.Range(_ground.bounds.min.x, _ground.bounds.max.x),
            _ground.bounds.max.y,
            UnityEngine.Random.Range(_ground.bounds.min.z, _ground.bounds.max.z));

        Physics.Raycast(scanPosition, Vector3.down, out RaycastHit hitInfo, _ground.bounds.max.y - _ground.bounds.min.y, _groundLayer);
        scanPosition = hitInfo.point;

        Collider[] hits = Physics.OverlapSphere(scanPosition, _scanRadius);
        
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
                LastResults.Add(resource);
        }

        ScanCompleted?.Invoke();
    }
}
