using UnityEngine;

namespace BattleForPlatformer
{
    public class PlayerSensor : MonoBehaviour
    {
        [SerializeField] private float _range;

        public Transform PlayerTransform { get; private set; }

        public bool IsPlayerInRange()
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, _range);

            foreach (Collider2D collider2D in collider2Ds)
            {
                if (collider2D.TryGetComponent(out PlayerMover _))
                {
                    PlayerTransform = collider2D.transform;

                    return true;
                }
            }

            PlayerTransform = null;

            return false;
        }
    }
}
