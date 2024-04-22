using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField, Range(0f, 1f)] private float _colorRedMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _colorRedMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _colorGreenMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _colorGreenMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _colorBlueMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _colorBlueMax = 1f;

    private Material _material => _meshRenderer.material;

    private void OnValidate()
    {
        if(_colorRedMin > _colorRedMax)
            _colorRedMin = _colorRedMax;

        if (_colorGreenMin > _colorGreenMax)
            _colorGreenMin = _colorGreenMax;

        if (_colorBlueMin > _colorBlueMax)
            _colorBlueMin = _colorBlueMax;
    }

    private void Start()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        _material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
