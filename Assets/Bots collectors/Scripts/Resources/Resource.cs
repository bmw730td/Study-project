using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class Resource : MonoBehaviour
{
    public readonly int Value = 1;

    [SerializeField] private ResourceType _resourceType;

    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private BoxCollider _collider;

    public event Action<Resource> Disabled;

    public ResourceType Type => _resourceType;

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

    [ContextMenu(nameof(SetType))]
    public void SetType(ResourceType type)
    {
        if (_resourceType != type)
        {
            _resourceType = type;

            transform.localScale = _resourceType.Scale;

            _filter.mesh = _resourceType.Mesh;
            _renderer.material = _resourceType.Material;

            _collider.center = _resourceType.ColliderCenter;
            _collider.size = _resourceType.ColliderSize;
        }
    }
}
