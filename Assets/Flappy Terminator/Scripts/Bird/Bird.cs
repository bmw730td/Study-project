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

    private void OnEnable()
    {
        _input.Controls.Player.Jump.started += ctx => _mover.Jump();
        _input.Controls.Player.Fire.started += ctx => _spawner.SpawnObject();

        _rotator.Enabled += () => _mover.Jumped += _rotator.RotateBirdToMax;
        _rotator.Disabled += () => _mover.Jumped -= _rotator.RotateBirdToMax;
    }

    private void OnDisable()
    {
        _input.Controls.Player.Jump.started -= ctx => _mover.Jump();
        _input.Controls.Player.Fire.started -= ctx => _spawner.SpawnObject();

        _rotator.Enabled -= () => _mover.Jumped += _rotator.RotateBirdToMax;
        _rotator.Disabled -= () => _mover.Jumped -= _rotator.RotateBirdToMax;
        _mover.Jumped -= _rotator.RotateBirdToMax;
    }

    private void Start()
    {
        if (_rotator.enabled)
        {
            _mover.Jumped += _rotator.RotateBirdToMax;
        }
        else
        {
            _mover.Jumped -= _rotator.RotateBirdToMax;
        }
    }

    private void FixedUpdate()
    {
        if (_mover.enabled)
            _mover.Move();

        if (_rotator.enabled)
            _rotator.UpdateBirdRotation();
    }

    public void Reset()
    {
        if (_mover.enabled)
            _mover.Reset();

        if (_spawner.enabled)
            _spawner.Reset();
    }
}
