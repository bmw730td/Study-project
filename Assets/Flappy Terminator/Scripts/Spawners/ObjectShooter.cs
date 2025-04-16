using UnityEngine;

[RequireComponent(typeof(PlayerInput))]

public class ObjectShooter : ObjectSpawner
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _playerInput.Controls.Player.Fire.started += ctx => SpawnObject();
    }

    private void OnDisable()
    {
        _playerInput.Controls.Player.Fire.started -= ctx => SpawnObject();
    }
}
