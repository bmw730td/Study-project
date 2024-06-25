using UnityEngine;

namespace BattleForPlatformer
{
    public class MultiDestroyer : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void OnDestroy()
        {
            Destroy(_target);
        }
    }
}
