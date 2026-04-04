using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _useTargetRange;
    
    private PlayerInput _playerControls;
    private Action<InputAction.CallbackContext> _useTargetSubscription;

    public event Action<RaycastHit> UsableHit;

    private void Awake()
    {
        _playerControls = new();
        _useTargetSubscription = ctx => UseTarget();
    }
    private void OnEnable()
    {
        _playerControls.Player.Use.started += _useTargetSubscription;
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Player.Use.started -= _useTargetSubscription;
        _playerControls.Disable();
    }

    private void UseTarget()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IUsable usable))
            {
                UsableHit?.Invoke(hit);
                usable.OnUse(this);
            }
        }
    }
}
