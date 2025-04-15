using UnityEngine;

public class SelfReturner : MonoBehaviour
{
    public ObjectSpawner Spawner;

    public void ReturnSelf()
    {
        if (Spawner != null)
        {
            Spawner.PutObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
