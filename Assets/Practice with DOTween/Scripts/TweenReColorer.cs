using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class TweenReColorer : MonoBehaviour
{
    [SerializeField] private Color _endColor;
    [SerializeField, Min(0f)] private float _duration;

    [SerializeField, Min(-1)] private int _amountOfLoops;
    [SerializeField] private LoopType _loopType;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _renderer.material.DOColor(_endColor, _duration).SetLoops(_amountOfLoops, _loopType);
    }
}
