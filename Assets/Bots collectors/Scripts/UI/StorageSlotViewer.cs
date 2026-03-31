using UnityEngine;
using TMPro;

public class StorageSlotViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _slotValues;

    private ResourceStorageSlot _storageSlot;

    private void OnDestroy()
    {
        if (_storageSlot != null)
            _storageSlot.AmountChanged -= (ctx1, ctx2) => UpdateText();
    }

    public void SetStorageSlot(ResourceStorageSlot slot)
    {
        _storageSlot = slot;

        if (enabled && _storageSlot != null)
        {
            UpdateText();
            _storageSlot.AmountChanged += (ctx1, ctx2) => UpdateText();
        }
    }

    private void UpdateText()
    {
        _slotValues.text = $"{_storageSlot.Type}: {_storageSlot.Amount}/{_storageSlot.Capacity}";
    }
}
