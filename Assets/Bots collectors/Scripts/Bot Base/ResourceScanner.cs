using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private MeshCollider _ground;

    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _groundLayer;

    public event Action<List<Resource>> ScanCompleted;

    public void ScanGround()
    {
        List<Resource> results = new();
        Vector3 scanPosition = GetRandomPosition();

        Collider[] hits = Physics.OverlapSphere(scanPosition, _scanRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource))
                results.Add(resource);
        }

        ScanCompleted?.Invoke(results);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition =  new Vector3(UnityEngine.Random.Range(_ground.bounds.min.x, _ground.bounds.max.x),
                                              _ground.bounds.max.y,
                                              UnityEngine.Random.Range(_ground.bounds.min.z, _ground.bounds.max.z));

        Physics.Raycast(randomPosition, Vector3.down, out RaycastHit randomMaxYhit, _ground.bounds.max.y - _ground.bounds.min.y, _groundLayer);

        return randomMaxYhit.point;
    }
}
