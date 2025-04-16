using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class ObjectReturner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SelfReturner returner))
            returner.ReturnSelf();
    }
}
