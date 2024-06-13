using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TaskAudioMixer
{
    public class VolumeSlider : MonoBehaviour
    {
        private const float MinValue = 0.0001f;
        private const float MaxValue = 1.0f;
        private const float ValueMultiplier = 20f;
        
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _volumeParameterName;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
        }

        private void ChangeVolume(float value)
        {
            value = Mathf.Clamp(value, MinValue, MaxValue);
            value = Mathf.Log10(value) * ValueMultiplier;
            
            _audioMixer.SetFloat(_volumeParameterName, value);
        }
    }
}
