using System;
using UnityEngine;

public class ReturnAnnouncer : MonoBehaviour, IReturnable<ReturnAnnouncer>
{
    public event Action<ReturnAnnouncer> ShouldReturn;

    public void InvokeReturn()
    {
        ShouldReturn?.Invoke(this);
    }
}
