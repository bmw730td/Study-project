using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Button))]

    public class TargetDamager : MonoBehaviour
    {
        [SerializeField] private Health _target;
        [SerializeField] private float _power;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(DamageTarget);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DamageTarget);
        }

        private void DamageTarget()
        {
            _target.TakeDamage(_power);
        }
    }
}
