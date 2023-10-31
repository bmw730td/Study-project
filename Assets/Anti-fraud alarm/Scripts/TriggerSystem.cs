using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;

    private void OnTriggerEnter(Collider otherCollider)
    {
        _onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        _onTriggerExit.Invoke();
    }
}
