using System;
using UnityEngine;

namespace RainOfCubes
{
    public abstract class OneShotTrigger : MonoBehaviour
    {
        private bool _isUsed = false;

        public event Action Activated;

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
