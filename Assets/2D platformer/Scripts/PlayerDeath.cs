using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    
    public void Die()
    {
        transform.position = _respawnPoint.position;
    }
}
