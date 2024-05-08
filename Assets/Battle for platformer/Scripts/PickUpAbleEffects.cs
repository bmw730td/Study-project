using UnityEngine;

namespace BattleForPlatformer
{
    public class PickUpAbleEffects : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;

        [SerializeField] private float _medKitHealAmount;

        public void ApplyEffect(PickUpAbles pickUpAbleType)
        {
            switch (pickUpAbleType)
            {
                case PickUpAbles.Coin:
                    break;

                case PickUpAbles.MedKit:
                    _playerHealth.Heal(_medKitHealAmount);
                    break;

                default:
                    Debug.Log($"Error: Unknown PickUpAble type {pickUpAbleType}");
                    break;
            }
        }
    }
}
