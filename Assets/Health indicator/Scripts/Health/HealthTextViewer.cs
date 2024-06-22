using TMPro;
using UnityEngine;

namespace HealthBar
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    
    public class HealthTextViewer : HealthViewer
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void UpdateValues()
        {
            _text.text = $"{Health.Value}/{Health.MaxValue}";
        }
    }
}
