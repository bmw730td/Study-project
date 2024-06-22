using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(DisposableActivator))]

    public class ColorChanger : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _minRed = 0f;
        [SerializeField, Range(0f, 1f)] private float _maxRed = 1f;

        [SerializeField, Range(0f, 1f)] private float _minGreen = 0f;
        [SerializeField, Range(0f, 1f)] private float _maxGreen = 1f;

        [SerializeField, Range(0f, 1f)] private float _minBlue = 0f;
        [SerializeField, Range(0f, 1f)] private float _maxBlue = 1f;

        private MeshRenderer _meshRenderer;
        private DisposableActivator _disposableActivator;
        private Color _defaultColor;

        private void OnValidate()
        {
            if (_minRed > _maxRed)
                _minRed = _maxRed;

            if (_minGreen > _maxGreen)
                _minGreen = _maxGreen;

            if (_minBlue > _maxBlue)
                _minBlue = _maxBlue;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _disposableActivator = GetComponent<DisposableActivator>();
        }

        private void OnEnable()
        {
            _disposableActivator.TargetReached += ChangeColor;
            _defaultColor = _meshRenderer.material.color;
        }

        private void OnDisable()
        {
            _disposableActivator.TargetReached -= ChangeColor;
            _meshRenderer.material.color = _defaultColor;
        }

        private void ChangeColor()
        {
            _meshRenderer.material.color = new Color(Random.Range(_minRed, _maxRed),
                                                     Random.Range(_minGreen, _maxGreen),
                                                     Random.Range(_minBlue, _maxBlue));
        }
    }
}
