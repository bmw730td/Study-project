using UnityEngine;

public class Division : MonoBehaviour
{
    [SerializeField] private float _chanceToDivide = 100f;
    [SerializeField] private float _chanceToDivideMultiplier = 0.5f;

    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private int _minDivisionFragments = 2;
    [SerializeField] private int _maxDivisionFragments = 6;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void OnMouseDown()
    {
        DivideSelf();
    }

    private void DivideSelf()
    {
        if (Random.Range(0f, 100f) <= _chanceToDivide)
        {
            _chanceToDivide *= _chanceToDivideMultiplier;
            transform.localScale *= _sizeMultiplier;

            int amountToInstantiate = Random.Range(_minDivisionFragments, _maxDivisionFragments + 1);

            for (int i = 0; i < amountToInstantiate; i++)
            {
                GameObject newSelf = Instantiate(gameObject);
                Rigidbody newSelfRigidbody = newSelf.GetComponent<Rigidbody>();

                newSelfRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}
