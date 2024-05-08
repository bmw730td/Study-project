using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleForPlatformer
{
    [RequireComponent(typeof(Health))]
    
    public class ReloadSceneOnDeath : MonoBehaviour
    {
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Death += ReloadScene;
        }

        private void OnDisable()
        {
            _health.Death -= ReloadScene;
        }

        private void ReloadScene()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(currentSceneName);
        }
    }
}
