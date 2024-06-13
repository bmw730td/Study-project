using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    [RequireComponent(typeof(Button))]
    
    public class TargetHealer : MonoBehaviour
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
            _button.onClick.AddListener(HealTarget);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HealTarget);
        }

        private void HealTarget()
        {
            _target.TakeHeal(_power);
        }
    }
}
