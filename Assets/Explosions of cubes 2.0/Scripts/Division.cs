using System;
using UnityEngine;

public class Division : MonoBehaviour
{
    [SerializeField] private float _chanceToDivide = 100f;
    [SerializeField] private float _chanceToDivideMultiplier = 0.5f;

    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private int _minDivisionFragments = 2;
    [SerializeField] private int _maxDivisionFragments = 6;

    private float _currentSizeMultiplier = 1;

    public event Action ObjectNotDivided;

    public float CurrentSizeMultiplier => _currentSizeMultiplier;

    private void OnMouseDown()
    {
        DivideSelf();
    }

    private void DivideSelf()
    {
        if (UnityEngine.Random.Range(0f, 100f) <= _chanceToDivide)
        {
            _chanceToDivide *= _chanceToDivideMultiplier;
            transform.localScale *= _sizeMultiplier;
            _currentSizeMultiplier *= _sizeMultiplier;

            int amountToInstantiate = UnityEngine.Random.Range(_minDivisionFragments, _maxDivisionFragments + 1);

            for (int i = 0; i < amountToInstantiate; i++)
            {
                Instantiate(this);
            }
        }
        else
        {
            ObjectNotDivided.Invoke();
        }

        Destroy(gameObject);
    }
}
