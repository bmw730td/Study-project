using UnityEngine;

[RequireComponent(typeof(ObjectSpawner))]

public class RotationRandomizer : MonoBehaviour
{
    [SerializeField] private float _minYRotation;
    [SerializeField] private float _maxYRotation;

    private ObjectSpawner _spawner;

    private void OnValidate()
    {
        if (_maxYRotation < _minYRotation)
            _maxYRotation = _minYRotation;
    }

    private void Awake()
    {
        _spawner = GetComponent<ObjectSpawner>();
    }

    private void OnEnable()
    {
        _spawner.WillSpawnObject += RandomizeRotation;
    }

    private void OnDisable()
    {
        _spawner.WillSpawnObject -= RandomizeRotation;
    }

    private void RandomizeRotation(ReturnAnnouncer obj)
    {
        obj.transform.rotation = Quaternion.Euler(new Vector3(
            obj.transform.rotation.x,
            Random.Range(_minYRotation, _maxYRotation),
            obj.transform.rotation.z));
    }
}
