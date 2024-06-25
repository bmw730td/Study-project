using UnityEngine;
using HealthBar;

namespace BattleForPlatformer
{
    public class MedKitCollectable : CollectableItem
    {
        [SerializeField, Min(0f)] private float _healAmount;
        
        public override void Collect(GameObject collector)
        {
            if (collector.TryGetComponent(out Health collectorsHealth))
                collectorsHealth.TakeHeal(_healAmount);

            Destroy(gameObject);
        }
    }
}
