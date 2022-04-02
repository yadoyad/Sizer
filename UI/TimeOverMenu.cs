using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeOverMenu : MonoBehaviour
{
    public Button medalsButton;
    public Image buttonImage;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public GameObject recordText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ressurectionCostText;

    public void CheckMedals()
    {
        ressurectionCostText.text = "x" + RessurectionCtrl.instance.ressurectionPrice.ToString();
        
        if(!RessurectionCtrl.instance.MedalsCheck())
        {
            buttonImage.sprite = disabledSprite;
            medalsButton.interactable = false;
        }
        else
        {
            buttonImage.sprite = enabledSprite;
            medalsButton.interactable = true;
        }
    }

    public void CheckHighscore()
    {
        recordText.SetActive(ScoreManager.instance.CheckScore());
        scoreText.text = ScoreManager.instance.currentScore.ToString();
    }

    public void PayMedals()
    {
        RessurectionCtrl.instance.RessurectionForMedals();
        MenuManager.instance.DisableTimeOverMenu();
    }

    public void PayAds()
    {
        RessurectionCtrl.instance.RessurectionForAds();
        MenuManager.instance.DisableTimeOverMenu();
    }
}
