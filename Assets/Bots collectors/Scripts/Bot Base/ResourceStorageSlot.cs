using System;
using UnityEngine;

[Serializable]

public class ResourceStorageSlot
{
    private readonly int MinAmount = 0;

    [SerializeField] private ResourceType _type;
    [SerializeField, Min(0)] private int _capacity;

    public event Action<ResourceStorageSlot, int> AmountChanged;

    public ResourceType Type => _type;
    public int Capacity => _capacity;
    public int Amount { get; private set; }

    public void ChangeAmount(int value)
    {
        value = Mathf.Clamp(value, MinAmount - Amount, _capacity - Amount);
        Amount += value;
        AmountChanged?.Invoke(this, value);
    }
}
