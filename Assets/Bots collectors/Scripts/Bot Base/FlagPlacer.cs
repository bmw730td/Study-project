using UnityEngine;

[RequireComponent(typeof(BotBaseBuilder))]
[RequireComponent(typeof(BotSender))]

public class FlagPlacer : MonoBehaviour
{
    [SerializeField] private Transform _flag;

    private BotBaseBuilder _builder;
    private BotSender _sender;

    private void Awake()
    {
        _builder = GetComponent<BotBaseBuilder>();
        _sender = GetComponent<BotSender>();
    }

    private void OnEnable()
    {
        _builder.PositionSet += PlaceFlag;
        _sender.SentBotBuildBase += HideFlag;
    }

    private void OnDisable()
    {
        _builder.PositionSet -= PlaceFlag;
        _sender.SentBotBuildBase -= HideFlag;
    }

    private void PlaceFlag(Vector3 newPosition)
    {
        _flag.position = newPosition;
        _flag.gameObject.SetActive(true);
    }

    private void HideFlag(Bot botToUnsubbscribe)
    {
        _flag.gameObject.SetActive(false);
    }
}
