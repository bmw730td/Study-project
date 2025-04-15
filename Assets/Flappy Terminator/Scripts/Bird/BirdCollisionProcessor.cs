using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class BirdCollisionProcessor : MonoBehaviour
{
    private CollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _handler = GetComponent<CollisionHandler>();
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
        switch (interactable)
        {
            case Ground:
                GameOver?.Invoke();
                break;

            case Probe:
                GameOver?.Invoke();
                break;

            case ProbeLaser:
                GameOver?.Invoke();
                break;
        }
    }
}
