using UnityEngine;

[RequireComponent(typeof(PlayerSensor))]

public class PlayerKiller : MonoBehaviour
{
    [SerializeField] private PlayerSensor _playerSensor;
    [SerializeField] private PlayerDeath _player;
    
    private void OnEnable()
    {
        _playerSensor.PlayerDetected += KillPlayer;
    }

    private void OnDisable()
    {
        _playerSensor.PlayerDetected -= KillPlayer;
    }

    private void KillPlayer()
    {
        _player.Die();
    }
}
