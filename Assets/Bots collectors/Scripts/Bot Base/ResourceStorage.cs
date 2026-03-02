using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
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

    public ResourceStorageSlot GetSlot(ResourceType type)
    {
        return _slots.FirstOrDefault(slot => slot.Type == type);
    }

    public List<ResourceStorageSlot> GetAllSlots()
    {
        return _slots.ToList();
    }
}
