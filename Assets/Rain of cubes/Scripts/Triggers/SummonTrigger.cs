using UnityEngine;

namespace RainOfCubes
{
    public class SummonTrigger : OneShotTrigger
    {
        [SerializeField, Min(0f)] private float _delay;
        
        private void OnEnable()
        {
            Invoke(nameof(ActivateSelf), _delay);
        }
    }
}
