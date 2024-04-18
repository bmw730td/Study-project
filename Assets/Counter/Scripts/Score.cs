using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public event Action ValueChanged;

    private int _value = 0;
    public int Value => _value;


    public void ChangeValue(int valueToAdd)
    {
        _value += valueToAdd;
        ValueChanged.Invoke();
    }
}
