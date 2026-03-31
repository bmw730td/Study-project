using System;
using UnityEngine;

[RequireComponent(typeof(BotSender))]

public class BotBaseBuilder : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private BotSender _sender;

    private Vector3 _newBasePosition;

    public event Action<Vector3> PositionSet;

    private void Awake()
    {
        _sender = GetComponent<BotSender>();

        _newBasePosition = transform.position;
    }

    public void SetBasePosition(Vector3 position)
    {
        _newBasePosition = position + _offset;
        PositionSet?.Invoke(_newBasePosition);
    }

    public void SendBotToBuildBase()
    {
        _sender.StartBuildingBase(_newBasePosition);
    }
}
