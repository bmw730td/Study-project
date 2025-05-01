using UnityEngine;
using TMPro;

public class StorageUnitViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitValues;

    private ResourceStorageUnit _storageUnit;

    private void OnEnable()
    {
        if (_storageUnit != null)
            _storageUnit.AmountChanged += UpdateUnitValuesText;
    }

    private void OnDisable()
    {
        if (_storageUnit != null)
            _storageUnit.AmountChanged += UpdateUnitValuesText;
    }

    public void SetStorageUnit(ResourceStorageUnit unit)
    {
        _storageUnit = unit;

        if (enabled && _storageUnit != null)
        {
            UpdateUnitValuesText();
            _storageUnit.AmountChanged += UpdateUnitValuesText;
        }
    }

    private void UpdateUnitValuesText()
    {
        _unitValues.text = $"{_storageUnit.Type}: {_storageUnit.Amount}/{_storageUnit.Capacity}";
    }
}
