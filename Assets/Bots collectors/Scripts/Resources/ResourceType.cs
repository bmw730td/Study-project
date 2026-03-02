using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Type", menuName = "Resource Type", order = 51)]

public class ResourceType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    [Header(nameof(BoxCollider))]
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _size;

    public string Name => _name;
    public Vector3 Scale => _scale;
    public Mesh Mesh => _mesh;
    public Material Material => _material;
    public Vector3 ColliderCenter => _center;
    public Vector3 ColliderSize => _size;
}
