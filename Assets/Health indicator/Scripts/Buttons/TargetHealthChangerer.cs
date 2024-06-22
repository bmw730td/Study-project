using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Button))]

    public class TargetHealthChanger : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private bool _isDamagingTarget;
        [SerializeField, Min(0f)] private float _power;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeHealthValue);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeHealthValue);
        }

        private void ChangeHealthValue()
        {
            if (_isDamagingTarget)
            {
                _target.TakeDamage(_power);
            }
            else
            {
                _target.TakeHeal(_power);
            }
        }
    }
}
