using UnityEngine;

namespace RainOfCubes
{
    [RequireComponent(typeof(SelfReleaser))]
    
    public class Exploder : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float _power;
        [SerializeField, Min(0f)] private float _range;
        
        private SelfReleaser _selfReleaser;

        private void Awake()
        {
            _selfReleaser = GetComponent<SelfReleaser>();
        }

        private void OnEnable()
        {
            _selfReleaser.AboutToRealease += Explode;
        }

        private void OnDisable()
        {
            _selfReleaser.AboutToRealease -= Explode;
        }

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _range);

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out Rigidbody hitRigidbody))
                    hitRigidbody.AddExplosionForce(_power, transform.position, _range);
            }
        }
    }
}
