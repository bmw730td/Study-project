using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    public event Action PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            PlayerDetected.Invoke();
        }
    }
}
