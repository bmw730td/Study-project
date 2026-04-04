using System;
using UnityEngine;

[Serializable]

public class ResourceStorageSlot
{
    private readonly int MinCapacity = 0;
    private readonly int MinAddAmount = 0;
    private readonly int MaxRemoveAmount = 0;

    [SerializeField] private ResourceType _type;
    [SerializeField, Min(0)] private int _capacity;

    public event Action<ResourceStorageSlot, int> AmountChanged;

    public ResourceType Type => _type;
    public int Capacity => _capacity;
    public int Amount { get; private set; }

    public void AddAmount(int value)
    {
        value = Mathf.Clamp(value, MinAddAmount, _capacity - Amount);
        Amount += value;
        AmountChanged?.Invoke(this, value);
    }

    public void RemoveAmount(int value)
    {
        value *= -1;
        value = Mathf.Clamp(value, MinCapacity - Amount, MaxRemoveAmount);
        Amount += value;
        AmountChanged?.Invoke(this, value);
    }
}
