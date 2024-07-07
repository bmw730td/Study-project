using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(Collider))]
    
    public class TargetReachTrigger : OneShotTrigger
    {
        [SerializeField, Min(0)] private int _targetLayerNumber;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == _targetLayerNumber)
            {
                ActivateSelf();
            }
        }
    }
}
