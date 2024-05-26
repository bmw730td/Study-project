using UnityEngine;
using UnityEngine.SceneManagement;

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
            _health.OnDeath += ReloadScene;
        }

        private void OnDisable()
        {
            _health.OnDeath -= ReloadScene;
        }

        private void ReloadScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentSceneName);
        }
    }
}
