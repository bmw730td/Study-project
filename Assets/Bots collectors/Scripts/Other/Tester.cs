using System;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private int _subscribtionCount;
    private Action<int> _logTextSubscribtion;

    private event Action<int> MyAction;

    private void Awake()
    {
        _subscribtionCount = 0;
        _logTextSubscribtion = ctx => LogText();
    }

    private void LogText()
    {
        Debug.Log("text");
    }

    [ContextMenu(nameof(SubscibeToAction))]
    private void SubscibeToAction()
    {
        _subscribtionCount++;
        MyAction += _logTextSubscribtion;
    }

    [ContextMenu(nameof(UnsubscibeFromAction))]
    private void UnsubscibeFromAction()
    {
        _subscribtionCount--;
        MyAction -= _logTextSubscribtion;
    }

    [ContextMenu(nameof(InvokeAction))]
    private void InvokeAction()
    {
        Debug.Log($"should log {_subscribtionCount} times. {MyAction == null} {_logTextSubscribtion == null}");
        MyAction?.Invoke(_subscribtionCount);
    }

    [ContextMenu(nameof(DestroyThis))]
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
