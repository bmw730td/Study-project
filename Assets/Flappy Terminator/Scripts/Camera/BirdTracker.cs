using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetX;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x + _offsetX, transform.position.y, transform.position.z);
    }
}
