using UnityEngine;

public class UserInput : MonoBehaviour
{
    public Controls Controls { get; private set; }

    private void Awake()
    {
        Controls = new();
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
