using UnityEngine;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(AbilityVampiricYodel))]
    
    public class ObjectSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

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
            _target.SetActive(true);
        }

        private void DeactivateObject()
        {
            _target.SetActive(false);
        }
    }
}
