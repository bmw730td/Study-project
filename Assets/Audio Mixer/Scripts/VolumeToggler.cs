using UnityEngine;
using UnityEngine.UI;

namespace TaskAudioMixer
{
    [RequireComponent(typeof(Toggle))]
    
    public class VolumeToggler : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(ToggleVolume);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ToggleVolume);
        }

        private void ToggleVolume(bool isTurnedOn)
        {
            if (isTurnedOn)
            {
                AudioListener.volume = 0f;
            }
            else
            {
                AudioListener.volume = 1f;
            }
        }
    }
}
