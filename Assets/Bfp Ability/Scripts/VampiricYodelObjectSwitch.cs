using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(AbilityVampiricYodel))]
    
    public class VampiricYodelObjectSwitch : MonoBehaviour
    {
        [SerializeField] private SizeChanger _target;

        private AbilityVampiricYodel _ability;
        
        private void Awake()
        {
            _ability = GetComponent<AbilityVampiricYodel>();
        }

        private void OnEnable()
        {
            _ability.Activated += ActivateObject;
            _ability.Deactivated += DeactivateObject;
        }

        private void OnDisable()
        {
            _ability.Activated -= ActivateObject;
            _ability.Deactivated -= DeactivateObject;
        }

        private void ActivateObject()
        {
            _target.gameObject.SetActive(true);
        }

        private void DeactivateObject()
        {
            _target.gameObject.SetActive(false);
        }
    }
}
