using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Collider2D))]

    public abstract class CollectableItem : MonoBehaviour
    {
        protected Collider2D _collider2D;

        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _collider2D.isTrigger = true;
        }

        public virtual void Collect(GameObject collector)
        {
            Destroy(gameObject);
        }
    }
}
