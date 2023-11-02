using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent(typeof(PlayerMovement)))
        {
            _onTriggerEnter2D.Invoke();
        }
    }
}
