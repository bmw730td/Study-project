using UnityEngine;

namespace RainOfCubes
{
    public class TargetableSpawner : ObjectSpawner
    {
        public void SpawnAtTargetPos(Vector3 targetPos)
        {
            SpawnObj(targetPos);
        }
    }
}
