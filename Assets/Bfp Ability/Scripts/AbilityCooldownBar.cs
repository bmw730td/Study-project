using UnityEngine;
using UnityEngine.UI;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Slider))]
    
    public class AbilityCooldownBar : MonoBehaviour
    {
        [SerializeField] private AbilityBase _ability;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _ability.CooldownTimerChanged += ChangeSliderValue;
        }

        private void OnDisable()
        {
            _ability.CooldownTimerChanged -= ChangeSliderValue;
        }

        private void ChangeSliderValue()
        {
            float cooldownTimerFraction = _ability.CooldownTimer / _ability.CooldownDuration;

            _slider.value = _slider.minValue + (_slider.maxValue - _slider.minValue) * cooldownTimerFraction;
        }
    }
}
