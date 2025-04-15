using UnityEngine;

public class KeySpawner : ObjectSpawner
{
    [SerializeField] private KeyCode _key;

    private void Update()
    {
        if (Input.GetKeyDown(_key))
            SpawnObject();
    }
}
