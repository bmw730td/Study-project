using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private List<ResourceStorageSlot> _slots;

    public List<ResourceStorageSlot> Slots => _slots;
    public Dictionary<EnumResourceType, ResourceStorageSlot> TypeSlotPairs { get; private set; }
    public bool IsFull
    {
        get
        {
            foreach (ResourceStorageSlot slot in _slots)
            {
                if (slot.ExpectedAmount < slot.Capacity)
                    return false;
            }

            return true;
        }
    }

    private void Awake()
    {
        TypeSlotPairs = new();
    }

    private void Start()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            TypeSlotPairs.Add(_slots[i].Type, _slots[i]);
        }
    }

    public void PutResourceIn(Resource resource)
    {
        foreach (ResourceStorageSlot slot in _slots)
        {
            if (slot.Type == resource.Type)
                slot.ChangeAmount(Resource.Value);
        }
    }

    public void TryFillSelf(Func<EnumResourceType, bool> tryingFillFunc)
    {
        foreach (ResourceStorageSlot slot in _slots)
        {
            while (slot.ExpectedAmount < slot.Capacity)
            {
                if (tryingFillFunc(slot.Type) == false)
                    break;
            }
        }
    }
}
