using UnityEngine;
using TMPro;

public class StorageUnitViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitValues;

    private ResourceStorageSlot _storageUnit;

    private void OnDestroy()
    {
        if (_storageUnit != null)
            _storageUnit.AmountChanged -= UpdateText;
    }

    public void SetStorageUnit(ResourceStorageSlot unit)
    {
        _storageUnit = unit;

        if (enabled && _storageUnit != null)
        {
            UpdateText();
            _storageUnit.AmountChanged += UpdateText;
        }
    }

    private void UpdateText()
    {
        _unitValues.text = $"{_storageUnit.Type}: {_storageUnit.Amount}/{_storageUnit.Capacity}";
    }
}
