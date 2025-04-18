using DG.Tweening;
using UnityEngine;

public class TweenMover : MonoBehaviour
{
    [SerializeField] private Vector3 _endPosition;
    [SerializeField, Min(0f)] private float _duration;
    [SerializeField] private bool _snapping;

    [SerializeField, Min(-1)] private int _amountOfLoops;
    [SerializeField] private LoopType _loopType;

    private void Start()
    {
        transform.DOMove(_endPosition, _duration, _snapping).SetLoops(_amountOfLoops, _loopType);
    }
}
