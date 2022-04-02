using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class MenuAnimations : MonoBehaviour
{
    [SerializeField] private float animationTime = 1f;
    public List<TMP_Text> tmElements = new List<TMP_Text>();
    public Button playButton;
    public Button exitButton;
    public RawImage gameLogo;
    public Ease animationEase;

    public void HideMainMenu()
    {
        foreach(TMP_Text txt in tmElements)
        {
            TempHide(txt);
        }
        HideLogo();
        playButton.interactable = false;
        exitButton.interactable = false;
        MenuManager.instance.DisableMainMenu(animationTime);
    }

    public void ShowMainMenu()
    {
        foreach(TMP_Text txt in tmElements)
        {
            TempShow(txt);
        }
        ShowLogo();
        playButton.interactable = true;
        exitButton.interactable = true;
        MenuManager.instance.EnableMainMenu(animationTime);
    }

    public void ShowInGameMenu(InGameMenu igMenu)
    {
        igMenu.Show(animationTime / 2, Ease.OutBack);
    }

    public void HideInGameMenu(InGameMenu igMenu)
    {
        igMenu.Hide(animationTime / 2, Ease.InBack);
    }

    void HideLogo()
    {
        DOTween.ToAlpha(() => gameLogo.color, x => gameLogo.color = x, 0f, animationTime)
            .SetEase(animationEase);
    }

    void ShowLogo()
    {
        DOTween.ToAlpha(() => gameLogo.color, x => gameLogo.color = x, 1f, animationTime)
            .SetEase(animationEase);
    }

    void TempHide(TMP_Text txt)  
    {
        DOTween.ToAlpha(() => txt.color, x => txt.color = x, 0f, animationTime)
            .SetEase(animationEase);
    }

    void TempShow(TMP_Text txt)  
    {
        DOTween.ToAlpha(() => txt.color, x => txt.color = x, 1f, animationTime)
            .SetEase(animationEase);
    }
}
