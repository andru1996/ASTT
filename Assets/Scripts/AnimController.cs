using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimController : MonoBehaviour
{
    private Vector2 startAnimSize = new Vector2(0f, 0f);
    [SerializeField] private float _animBounceFrameDuration;

    [SerializeField] private float _animCorrectAnswerDuration;

    [SerializeField] private Vector3 _maxOffsetAnimIncorrectAnswer;
    [SerializeField] private float _animIncorrectAnswerDuration;

    [SerializeField] private float _animFadeInTextDuration;
    [SerializeField] private float _animFadeInImageDuration;

    public float AnimCorrectAnswerDuration { get => _animCorrectAnswerDuration; }

    public void AnimBounceFrame(Frame frame)
    {
        Vector2 baseSizeDelta = frame.FrameTransform.sizeDelta;
        frame.FrameTransform.sizeDelta = startAnimSize;

        Sequence animFrameSequence = DOTween.Sequence();

        animFrameSequence.Append(frame.FrameTransform.DOSizeDelta(baseSizeDelta * 1.02f, _animBounceFrameDuration * 0.9f, false));
        animFrameSequence.Append(frame.FrameTransform.DOSizeDelta(baseSizeDelta, _animBounceFrameDuration * 0.1f, false));
    }

    public void AnimCorrectAnswer(Frame frame)
    {
        Vector2 baseSizeDelta = frame.IconTransform.sizeDelta;

        Sequence animFrameSequence = DOTween.Sequence();

        animFrameSequence.Append(frame.IconTransform.DOSizeDelta(baseSizeDelta * 0.9f, _animCorrectAnswerDuration * 0.5f, false));
        animFrameSequence.Append(frame.IconTransform.DOSizeDelta(baseSizeDelta, _animCorrectAnswerDuration * 0.5f, false));

        foreach (ParticleSystem particle in frame.Particles)
        {
            particle.Play();
        }
    }

    public void AnimIncorrectAnswer(Frame frame)
    {
        Sequence animFrameSequence = DOTween.Sequence();

        animFrameSequence.Append(frame.IconTransform.DOShakePosition(_animIncorrectAnswerDuration, _maxOffsetAnimIncorrectAnswer, 5, 0, false, true));
    }

    public void AnimFadeInText(Text text)
    {
        float baseAlpha = text.color.a;

        Sequence animTextSequence = DOTween.Sequence();

        animTextSequence.Append(text.DOFade(0f, 0f));
        animTextSequence.Append(text.DOFade(baseAlpha, _animFadeInTextDuration));
    }

    public void AnimFadeInImage(Image image)
    {
        float baseAlpha = image.color.a;

        Sequence animTextSequence = DOTween.Sequence();

        animTextSequence.Append(image.DOFade(0f, 0f));
        animTextSequence.Append(image.DOFade(baseAlpha, _animFadeInImageDuration));
    }
}
