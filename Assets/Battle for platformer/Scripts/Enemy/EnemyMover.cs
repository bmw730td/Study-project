using UnityEngine;

namespace BattleForPlatformer
{
    public abstract class EnemyMover : MonoBehaviour
    {
        [SerializeField, Min(0f)] protected float _speed;
        
        public Vector3 TargetPosition { get; protected set; }
    }
}
