using System;
using UnityEngine;

public class ReturnAnnouncer : MonoBehaviour
{
    public event Action<ReturnAnnouncer> ShouldBeReturned;

    public void AnnounceReturn()
    {
        ShouldBeReturned?.Invoke(this);
    }
}
