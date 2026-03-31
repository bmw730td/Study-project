using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    private readonly int AmountOnNoType = 0;

    [SerializeField] private List<ResourceStorageSlot> _slots;

    public void PutResourceIn(Resource resource)
    {
        GetSlot(resource.Type).ChangeAmount(resource.Value);

        if (resource.TryGetComponent(out ReturnAnnouncer announcer))
        {
            announcer.AnnounceReturn();
        }
        else
        {
            Destroy(resource);
        }
    }

    public void RemoveResource(ResourceType type, int amount)
    {
        GetSlot(type).ChangeAmount(amount * -1);
    }

    public ResourceStorageSlot GetSlot(ResourceType type)
    {
        return _slots.FirstOrDefault(slot => slot.Type == type);
    }

    public int GetAmount(ResourceType type)
    {
        ResourceStorageSlot slot = GetSlot(type);

        if (slot == null)
            return AmountOnNoType;

        return slot.Amount;
    }

    public List<ResourceStorageSlot> GetAllSlots()
    {
        return _slots.ToList();
    }
}
