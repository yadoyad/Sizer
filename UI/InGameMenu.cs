using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameMenu : MonoBehaviour
{
    public Image background;
    public RectTransform rTrans;
    [SerializeField] private Vector3 showPosition = Vector3.zero;
    [SerializeField] private Vector3 hidePosition = Vector3.zero;

    public void Hide(float animationTime, Ease ease)
    {
        DOTween.ToAlpha(() => background.color, x => background.color = x, 0f, animationTime);
        rTrans.DOLocalMove(hidePosition, animationTime).SetEase(ease);
    }
    public void Show(float animationTime, Ease ease)
    {
        DOTween.ToAlpha(() => background.color, x => background.color = x, .5f, animationTime);
        rTrans.DOLocalMove(showPosition, animationTime).SetEase(ease);
    }
}
