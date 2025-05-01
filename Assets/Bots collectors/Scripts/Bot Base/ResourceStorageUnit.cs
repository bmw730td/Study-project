using System;
using UnityEngine;

[Serializable]
public class ResourceStorageUnit
{
    private readonly int MinAmount = 0;

    [SerializeField] private EnumResourceType _type;
    [SerializeField, Min(0)] private int _amount;
    [SerializeField] private int _capacity;

    public event Action AmountChanged;

    public EnumResourceType Type => _type;
    public int Amount => _amount;
    public int Capacity => _capacity;

    public void ChangeAmount(int value)
    {
        _amount += value;
        CheckAmount();
        AmountChanged?.Invoke();
    }

    public void CheckAmount()
    {
        _amount = Mathf.Clamp(_amount, MinAmount, _capacity);
    }
}
