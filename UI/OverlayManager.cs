using UnityEngine;
using TMPro;
using System;

public class OverlayManager : MonoBehaviour
{
    public static OverlayManager instance;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public OverlayAnimations animations;
    public bool overlayEnabled {get; private set;} = false;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void ShowOverlay()
    {
        animations.ShowOverlay();
    }

    public void HideOverlay()
    {
        animations.HideOverlay();
    }

    public void ShowPopup()
    {
        animations.MedalPopup();
    }

    public void TimerTextUpdate(float value)
    {
        timerText.text = Math.Round(value, 1).ToString();
    }

    public void ScoreTextUpdate(int value)
    {
        scoreText.text = value.ToString();
    }

    public void AppearAnimationsComplete() => overlayEnabled = true;
    public void HideAnimationsComplete() => overlayEnabled = false;
}
