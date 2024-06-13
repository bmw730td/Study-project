using TMPro;
using UnityEngine;

namespace HealthBar
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Health _health;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _health.HealthChanged += UpdateText;

            UpdateText();
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateText;
        }

        private void UpdateText()
        {
            _text.text = $"{_health.Value}/{_health.MaxValue}";
        }
    }
}
