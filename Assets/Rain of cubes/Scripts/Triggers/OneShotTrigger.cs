using System;
using UnityEngine;

namespace RainOfCubes
{
    public abstract class OneShotTrigger : MonoBehaviour
    {
        public event Action Activated;

        private bool _isUsed = false;

        private void OnDisable()
        {
            _isUsed = false;
        }

        protected void ActivateSelf()
        {
            if (_isUsed == false)
            {
                _isUsed = true;
                Activated?.Invoke();
            }
        }
    }
}