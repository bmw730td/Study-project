using System.Collections.Generic;
using UnityEngine;

public class StorageViewer : MonoBehaviour
{
    [SerializeField] private StorageSlotViewer _slotViewerPrefab;
    [SerializeField] private ResourceStorage _storage;

    private void Start()
    {
        StorageSlotViewer newViewer;

        List<ResourceStorageSlot> slots = _storage.GetAllSlots();

        foreach (ResourceStorageSlot slot in slots)
        {
            newViewer = Instantiate(_slotViewerPrefab, parent: transform);
            newViewer.SetStorageSlot(slot);
        }
    }
}
