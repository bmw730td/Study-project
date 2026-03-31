using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class Resource : MonoBehaviour
{
    public readonly int Value = 1;

    private ResourceConfig _config;
    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private BoxCollider _collider;

    public event Action<Resource> Disabled;

    public ResourceType Type => _config.Type;

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnDisable()
    {
        Disabled?.Invoke(this);
    }

    public void SetConfig(ResourceConfig config)
    {
        if (_config != config)
        {
            _config = config;

            transform.localScale = _config.Scale;

            _filter.mesh = _config.Mesh;
            _renderer.material = _config.Material;

            _collider.center = _config.ColliderCenter;
            _collider.size = _config.ColliderSize;
        }
    }
}
