using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public static readonly int Value = 1;
    
    [SerializeField] private EnumResourceType _type;

    public event Action<Resource> Disabled;

    public EnumResourceType Type => _type;
    public bool IsGrabbable => gameObject.transform.parent == null;
    public bool IsReserved { get; private set; }

    private void OnEnable()
    {
        UnReserve();
    }

    private void OnDisable()
    {
        UnReserve();
        Disabled?.Invoke(this);
    }

    public void Reserve()
    {
        IsReserved = true;
    }

    public void UnReserve()
    {
        IsReserved = false;
    }
}
