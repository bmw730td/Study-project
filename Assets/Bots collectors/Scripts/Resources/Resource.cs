using UnityEngine;

public class Resource : MonoBehaviour
{
    public static readonly int Value = 1;
    
    [SerializeField] private EnumResourceType _type;

    public EnumResourceType Type => _type;
    public bool IsGrabbable => gameObject.transform.parent == null;
}
