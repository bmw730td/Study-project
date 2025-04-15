using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(SelfReturner))]

public class ProbeCollisionProcessor : MonoBehaviour
{
    private CollisionHandler _handler;
    private SelfReturner _returner;
    private ScoreCounter _counter;

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
        if (_counter == null)
            _returner.Spawner.TryGetComponent(out _counter);
        
        switch (interactable)
        {
            case BirdLaser:
                _returner.ReturnSelf();
                _counter.IncreaseScore();
                break;
        }
    }
}
