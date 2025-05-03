using UnityEngine;

public class StorageViewer : MonoBehaviour
{
    [SerializeField] private StorageUnitViewer _unitViewerPrefab;
    [SerializeField] private ResourceStorage _storage;

    private void Start()
    {
        StorageUnitViewer newViewer;

        foreach (ResourceStorageSlot unit in _storage.Slots)
        {
            newViewer = Instantiate(_unitViewerPrefab, parent: transform);
            newViewer.SetStorageUnit(unit);
        }
    }
}
