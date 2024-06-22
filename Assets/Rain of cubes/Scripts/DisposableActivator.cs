using System;
using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(Collider))]
    
    public class DisposableActivator : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _targetLayerNumber;

        private bool _isUsed;

        public event Action TargetReached;

        private void OnEnable()
        {
            _isUsed = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isUsed == false && collision.gameObject.layer == _targetLayerNumber)
            {
                _isUsed = true;

                TargetReached?.Invoke();
            }
        }
    }
}
