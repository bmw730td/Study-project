using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputControls _controls;

    public PlayerInputControls Controls
    {
        get
        {
            if (_controls == null)
            {
                _controls = new();
                _controls.Enable();
            }

            return _controls;
        }
    
    }

    private void Awake()
    {
        _controls = new();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
