using UnityEngine;

namespace BattleForPlatformer
{
    public static class PlayerAnimatorData
    {
        public static class Parameters
        {
            public static readonly int HorizontalMovement = Animator.StringToHash(nameof(HorizontalMovement));
            public static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        }
    }
}
