using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TweenTextChanger : MonoBehaviour
{
    [SerializeField] private string _textToReWrite;
    [SerializeField, Min(0f)] private float _reWritingDuration;

    [SerializeField] private string _textToAdd;
    [SerializeField, Min(0f)] private float _addingDuration;

    [SerializeField, Min(0f)] private float _scramblingDuration;
    [SerializeField] private ScrambleMode _scrambleMode;

    [SerializeField, Min(-1)] private int _amountOfLoops;
    [SerializeField] private LoopType _loopType;

    private Text _text;

    private Sequence _changingSequence;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        _changingSequence = DOTween.Sequence()
            .Append(_text.DOText(_textToReWrite, _reWritingDuration))
            .Append(_text.DOText(_textToAdd, _addingDuration).SetRelative())
            .Append(_text.DOText(_text.text, _scramblingDuration, scrambleMode: _scrambleMode))
            .SetLoops(_amountOfLoops, _loopType);
    }
}
