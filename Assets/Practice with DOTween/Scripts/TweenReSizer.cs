using DG.Tweening;
using UnityEngine;

public class TweenReSizer : MonoBehaviour
{
    [SerializeField] Vector3 _endSize;
    [SerializeField, Min(0f)] private float _duration;

    [SerializeField, Min(-1)] private int _amountOfLoops;
    [SerializeField] private LoopType _loopType;

    private void Start()
    {
        transform.DOScale(_endSize, _duration).SetLoops(_amountOfLoops, _loopType);
    }
}
