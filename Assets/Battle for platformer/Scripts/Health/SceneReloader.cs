using UnityEngine;
using UnityEngine.SceneManagement;
using HealthBar;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Health))]
    
    public class SceneReloader : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.ValueReachedMin += ReloadScene;
        }

        private void OnDisable()
        {
            _health.ValueReachedMin -= ReloadScene;
        }

        private void ReloadScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentSceneName);
        }
    }
}
