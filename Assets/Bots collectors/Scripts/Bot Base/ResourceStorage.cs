using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private List<ResourceStorageUnit> _units;// wtf

    public List<ResourceStorageUnit> Units => _units;

    private void OnValidate()
    {
        if (Units.Count > 0)
        {
            foreach (ResourceStorageUnit unit in _units)
            {
                unit.CheckAmount();
            }
        }
    }

    public void PutResourceIn(Resource resource)
    {
        foreach(ResourceStorageUnit unit in _units)
        {
            if (unit.Type == resource.Type)
                unit.ChangeAmount(Resource.Value);
        }
    }
}
