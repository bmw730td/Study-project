using UnityEngine;

namespace BattleForPlatformer
{
    public class SizeChanger : MonoBehaviour
    {
        [SerializeField] private AbilityVampiricYodel _ability;

        private void OnEnable()
        {
            _ability.StatsChanged += ChangeSize;

            ChangeSize();
        }

        private void OnDisable()
        {
            _ability.StatsChanged -= ChangeSize;
        }

        private void ChangeSize()
        {
            Vector3 globalMultiplier = new Vector3(transform.localScale.x / transform.lossyScale.x,
                                                   transform.localScale.y / transform.lossyScale.y,
                                                   transform.localScale.z / transform.lossyScale.z);
            
            transform.localScale = globalMultiplier * _ability.Range * 2f;
        }
    }
}
