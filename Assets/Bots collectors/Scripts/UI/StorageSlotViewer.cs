using UnityEngine;
using TMPro;
using System;

public class StorageSlotViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _slotValues;

    private ResourceStorageSlot _storageSlot;
    private Action<ResourceStorageSlot, int> _updateTextSubscription;

    private void Awake()
    {
        _updateTextSubscription = (ctx1, ctx2) => UpdateText();
    }

    private void OnDestroy()
    {
        if (_storageSlot != null)
            _storageSlot.AmountChanged -= _updateTextSubscription;
    }

    public void SetStorageSlot(ResourceStorageSlot slot)
    {
        _storageSlot = slot;

        if (enabled && _storageSlot != null)
        {
            UpdateText();
            _storageSlot.AmountChanged += _updateTextSubscription;
        }
    }

    private void UpdateText()
    {
        _slotValues.text = $"{_storageSlot.Type}: {_storageSlot.Amount}/{_storageSlot.Capacity}";
    }
}
