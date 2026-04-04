using System;

public interface IReturnable<T>
{
    public event Action<T> ShouldReturn;

    public void InvokeReturn();
}
