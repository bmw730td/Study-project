using DG.Tweening;
using UnityEngine;

public class TweenRotator : MonoBehaviour
{
    [SerializeField] private Vector3 _endRotation;
    [SerializeField, Min(0f)] private float _duration;
    [SerializeField] private RotateMode _rotateMode;

    [SerializeField, Min(-1)] private int _amountOfLoops;
    [SerializeField] private LoopType _loopType;

    private void Start()
    {
        transform.DORotate(_endRotation, _duration).SetLoops(_amountOfLoops, _loopType);
    }
}
