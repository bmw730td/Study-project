using UnityEngine;

namespace BattleForPlatformer
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private PlayerMover _player;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            transform.position = _player.transform.position + _offset;
        }
    }
}
