using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Config", menuName = "Resource Config", order = 52)]

public class ResourceConfig : ScriptableObject
{
    [SerializeField] private ResourceType _type;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    [Header(nameof(BoxCollider))]
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _size;

    public ResourceType Type => _type;
    public Vector3 Scale => _scale;
    public Mesh Mesh => _mesh;
    public Material Material => _material;
    public Vector3 ColliderCenter => _center;
    public Vector3 ColliderSize => _size;
}
