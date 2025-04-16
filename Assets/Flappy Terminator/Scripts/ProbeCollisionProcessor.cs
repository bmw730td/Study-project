using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(SelfReturner))]

public class ProbeCollisionProcessor : MonoBehaviour
{
    private CollisionHandler _handler;
    private SelfReturner _returner;

    public event Action HitByPlayer;

    private void Awake()
    {
        _handler = GetComponent<CollisionHandler>();
        _returner = GetComponent<SelfReturner>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is BirdLaser)
        {
            HitByPlayer?.Invoke();
            _returner.ReturnSelf();
        }
    }
}
