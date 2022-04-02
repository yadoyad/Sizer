using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class OverlayElement : MonoBehaviour
{
    [SerializeField] private Vector3 showPosition = Vector3.zero;
    [SerializeField] private Vector3 hidePosition = Vector3.zero;
    private RectTransform rTrans;

    private void OnEnable() 
    {
        rTrans = GetComponent<RectTransform>();
    }

    public void Hide(float animationTime, Ease ease)
    {
        rTrans.DOLocalMove(hidePosition, animationTime).SetEase(ease);
    }
    public void Show(float animationTime, Ease ease)
    {
        rTrans.DOLocalMove(showPosition, animationTime).SetEase(ease);
    }
}
