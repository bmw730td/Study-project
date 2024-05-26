using UnityEngine;

public class UserInput : MonoBehaviour
{
    [HideInInspector] public static UserInput Instance;
    [HideInInspector] public Controls Controls;

    [HideInInspector] public Vector2 MoveInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Controls = new();

        Controls.Movement.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
}
