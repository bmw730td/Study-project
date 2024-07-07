using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(SelfReleaser))]
    [RequireComponent(typeof(MeshRenderer))]

    public class AlfaColorChanger : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _targetAlfaColor;

        private SelfReleaser _selfReleaser;
        private MeshRenderer _meshRenderer;

        private Color _defaultColor;

        private void Awake()
        {
            _selfReleaser = GetComponent<SelfReleaser>();
            _meshRenderer = GetComponent<MeshRenderer>();

            _defaultColor = _meshRenderer.material.color;
        }

        private void OnEnable()
        {
            _selfReleaser.DelayCounterChanged += ChangeAlfaColor;
        }

        private void OnDisable()
        {
            _selfReleaser.DelayCounterChanged -= ChangeAlfaColor;

            _meshRenderer.material.color = _defaultColor;
        }

        private void ChangeAlfaColor()
        {
            float newAlfaColor = Mathf.Lerp(_defaultColor.a, _targetAlfaColor, 1f - _selfReleaser.DelayCounter / _selfReleaser.BaseDelay);

            _meshRenderer.material.color = new Color(_meshRenderer.material.color.r,
                                                     _meshRenderer.material.color.g,
                                                     _meshRenderer.material.color.b,
                                                     newAlfaColor);
        }
    }
}
