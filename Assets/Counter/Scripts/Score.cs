using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _value = 0;

    public event Action ValueChanged;

    public int Value => _value;

    public void AddValue(int valueToAdd)
    {
        _value += valueToAdd;
        ValueChanged.Invoke();
    }
}
