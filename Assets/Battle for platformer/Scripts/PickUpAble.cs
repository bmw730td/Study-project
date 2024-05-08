using UnityEngine;

namespace BattleForPlatformer
{
    public class PickUpAble : MonoBehaviour
    {
        [SerializeField] private PickUpAbles _type;

        public PickUpAbles Type => _type;
    }
}
