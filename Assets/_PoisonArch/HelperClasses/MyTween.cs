using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using PoisonArch;

public class MyTween : AbstractSingleton<MyTween>
{
    public void ButtonShineEffect(Transform _shine, float _offset, float _speed, float _minDelay, float _maxDelay)
    {
        _shine.DOLocalMoveX(_offset, _speed).SetEase(Ease.Linear).SetDelay(Random.Range(_minDelay, _maxDelay)).OnComplete(() =>
        {
            _shine.DOLocalMoveX(-_offset, 0);
        }).SetLoops(-1, LoopType.Restart);
    }
    public void DoTween_TryAgainTransform(Transform _TryAgainTransform)
    {
        Sequence DCSeq = DOTween.Sequence().SetAutoKill(false);

        _TryAgainTransform.GetChild(0).GetComponent<RectTransform>().DORotate(new Vector3(0, 0, 50f), .8f, RotateMode.Fast)
            .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        Color noAlpha = Color.white;
        Color fullAlpha = Color.white;
        noAlpha.a = 0;
        fullAlpha.a = 255;

        Transform TryAgainNumbers = _TryAgainTransform.GetChild(2);

        DCSeq.Append(TryAgainNumbers.GetChild(2).GetComponent<Image>().DOColor(fullAlpha, 0f));
        DCSeq.Append(TryAgainNumbers.GetChild(2).GetComponent<Image>().DOColor(noAlpha, 1f));
        DCSeq.Append(TryAgainNumbers.GetChild(1).GetComponent<Image>().DOColor(fullAlpha, 0f));
        DCSeq.Append(TryAgainNumbers.GetChild(1).GetComponent<Image>().DOColor(noAlpha, 1f));
        DCSeq.Append(TryAgainNumbers.GetChild(0).GetComponent<Image>().DOColor(fullAlpha, 0f));
        DCSeq.Append(TryAgainNumbers.GetChild(0).GetComponent<Image>().DOColor(noAlpha, 1f));
    }
    public void CollactableRotate(Transform _transform, float _delay)
    {
        _transform.DORotate(new Vector3(0, 80f, 0), _delay, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
