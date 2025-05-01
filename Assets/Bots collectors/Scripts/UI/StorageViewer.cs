using UnityEngine;

public class StorageViewer : MonoBehaviour
{
    [SerializeField] private StorageUnitViewer _unitViewerPrefab;
    [SerializeField] private ResourceStorage _storage;

    private void Start()
    {
        StorageUnitViewer newViewer;

        foreach (ResourceStorageUnit unit in _storage.Units)
        {
            newViewer = Instantiate(_unitViewerPrefab, parent: transform);
            newViewer.SetStorageUnit(unit);
        }
    }
}
