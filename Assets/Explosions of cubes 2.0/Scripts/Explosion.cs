using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Division))]

public class Explosion : MonoBehaviour
{
    [SerializeField] private Division _division;

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void OnEnable()
    {
        _division.ObjectNotDivided += Explode;
    }

    private void OnDisable()
    {
        _division.ObjectNotDivided -= Explode;
    }

    private void Explode()
    {
        foreach (Rigidbody hitRigidbody in GetHitRigidbodies())
        {
            hitRigidbody.AddExplosionForce(_explosionForce / _division.CurrentSizeMultiplier, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetHitRigidbodies()
    {
        List<Rigidbody> hitRigidbodies = new();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            if(collider.attachedRigidbody != null)
            {
                hitRigidbodies.Add(collider.attachedRigidbody);
            }
        }

        return hitRigidbodies;
    }
}
