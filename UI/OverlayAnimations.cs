using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverlayAnimations : MonoBehaviour
{
    public List<OverlayElement> overlayElements = new List<OverlayElement>();
    public OverlayElement medalPopup;
    [SerializeField] private float routineDelay = .1f;
    [Header("Appear settings")]
    [SerializeField] private float appearAnimTime = 1f;
    [SerializeField] private Ease appearEase = Ease.Linear;

    [Header("Hide settings")]
    [SerializeField] private float hideAnimTime = 1f;
    [SerializeField] private Ease hideEase = Ease.Linear;

    public void ShowOverlay()
    {
        StartCoroutine(ShowRoutine());
    }

    public void HideOverlay()
    {
        StartCoroutine(HideRoutine());
    }

    public void MedalPopup()
    {
        StartCoroutine(PopupRoutine());
    }

    IEnumerator ShowRoutine()
    {
        foreach(OverlayElement el in overlayElements)
        {
            el.Show(appearAnimTime, appearEase);
            yield return new WaitForSeconds(routineDelay);
        }
        OverlayManager.instance.AppearAnimationsComplete();
    }

    IEnumerator HideRoutine()
    {
        foreach(OverlayElement el in overlayElements)
        {
            el.Hide(hideAnimTime, hideEase);
            yield return new WaitForSeconds(routineDelay);
        }
        OverlayManager.instance.HideAnimationsComplete();
    }

    IEnumerator PopupRoutine()
    {
        medalPopup.Show(appearAnimTime, Ease.Linear);
        yield return new WaitForSeconds(1f);
        medalPopup.Hide(appearAnimTime, Ease.Linear);
    }
}
