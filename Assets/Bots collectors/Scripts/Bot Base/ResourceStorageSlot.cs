using System;
using UnityEngine;

[Serializable]
public class ResourceStorageSlot
{
    private readonly int MinAmount = 0;

    [SerializeField] private EnumResourceType _type;
    [SerializeField, Min(0)] private int _capacity;

    public event Action AmountChanged;

    public EnumResourceType Type => _type;
    public int Capacity => _capacity;
    public int Amount { get; private set; }
    public int ExpectedAmount { get; private set; }

    public void ChangeAmount(int value)
    {
        Amount = Mathf.Clamp(Amount + value, MinAmount, _capacity);
        ExpectedAmount = Mathf.Clamp(ExpectedAmount, Amount, _capacity);
        AmountChanged?.Invoke();
    }

    public void ChangeExpectedAmount(int value)
    {
        ExpectedAmount = Mathf.Clamp(ExpectedAmount + value, Amount, _capacity);
    }
}
