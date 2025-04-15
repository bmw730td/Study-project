using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class ObjectReturner : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject collision)
    {
        if (collision.gameObject.TryGetComponent(out SelfReturner returner))
            returner.ReturnSelf();
    }
}
