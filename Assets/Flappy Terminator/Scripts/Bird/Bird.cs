using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(BirdRotator))]
[RequireComponent(typeof(ObjectSpawner))]

public class Bird : MonoBehaviour
{
    private PlayerInput _input;

    private BirdMover _mover;
    private BirdRotator _rotator;
    private ObjectSpawner _spawner;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();

        _mover = GetComponent<BirdMover>();
        _rotator = GetComponent<BirdRotator>();
        _spawner = GetComponent<ObjectSpawner>();
    }

    private void Start()
    {
        _mover.Jumped += _rotator.RotateBirdToMax;
    }

    private void OnEnable()
    {
        _input.Controls.Player.Jump.started += ctx => _mover.Jump();
        _input.Controls.Player.Fire.started += ctx => _spawner.SpawnObject();
    }

    private void OnDisable()
    {
        _input.Controls.Player.Jump.started -= ctx => _mover.Jump();
        _input.Controls.Player.Fire.started -= ctx => _spawner.SpawnObject();
    }

    private void FixedUpdate()
    {
        _mover.Move();
        _rotator.UpdateBirdRotation();
    }

    public void Reset()
    {
        _mover.Reset();
        _spawner.Reset();
    }
}
