using UnityEngine;

namespace BattleForPlatformer
{
    public static class PlayerAnimatorData
    {
        public static class Parameters
        {
            public static readonly int IsWalking = Animator.StringToHash(nameof(IsWalking));
            public static readonly int IsInAir = Animator.StringToHash(nameof(IsInAir));
            public static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
            public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        }
    }
}
