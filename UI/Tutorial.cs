using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;
    public GameObject fingerImage;
    private RectTransform fingerTransform;
    public TextMeshProUGUI tutorialText;
    public string holdText = "HOLD TO GROW";
    public string releaseText = "RELEASE TO STOP";
    public Vector3 hidePosition;
    public Vector3 showPosition;
    public float animationTime = 1f;
    public Ease animationEase = Ease.Linear;
    public bool tutorialActive = false;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() 
    {
        fingerTransform = fingerImage.GetComponent<RectTransform>();
    }

    public void CheckTutorial()
    {
        if(!SaveLoadCtrl.instance.userData.tutorialShown)
        {
           fingerImage.SetActive(true);
           tutorialText.gameObject.SetActive(true);
           tutorialActive = true; 
           StartCoroutine(AnimationRoutine());
        }
        else
        {
            fingerImage.SetActive(false);
            tutorialText.gameObject.SetActive(false);            
            tutorialActive = false;
        }
    }

    public void StopTutorial()
    {
        tutorialActive = false;
        StopAnimation();
        SaveLoadCtrl.instance.TutorialShown();
    }

    private void StopAnimation()
    {
        StopAllCoroutines();
        fingerImage.SetActive(false);
        tutorialText.gameObject.SetActive(false);
    }

    IEnumerator AnimationRoutine()
    {
        tutorialText.text = holdText;
        fingerTransform.DOLocalMove(hidePosition, animationTime / 1.5f).SetEase(animationEase);

        yield return new WaitForSeconds(animationTime);

        tutorialText.text = releaseText;
        fingerTransform.DOLocalMove(showPosition, animationTime / 1.5f).SetEase(animationEase);

        yield return new WaitForSeconds(animationTime);
        StartCoroutine(AnimationRoutine());
    }
}
