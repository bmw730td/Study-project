using System;
using UnityEngine;

public class SelfReturner : MonoBehaviour
{
    public event Action<SelfReturner> ShouldBeReturned;

    public void ReturnSelf()
    {
        ShouldBeReturned?.Invoke(this);
    }
}
