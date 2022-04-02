using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Solution : MonoBehaviour
{
    [SerializeField] private float increaseAmount = 0.05f; //Увеличение scale
    [SerializeField] private float increaseRate = 0.01f; //Частота обновления в секундах
    public float solutionSize {get; private set;} = 0.05f;
    public float jellyAnimationTime = .2f;
    public Ease jellyEase = Ease.Linear;
    private const float maxSize = 2f;
    private float previousTime = 0;
    private SpriteRenderer spriteRenderer;
    private ObjectiveManager objectiveManager;
    private CommonObjective myComObj;
    private bool reachedMaxSize = false;

    private void OnEnable() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    public void Enlarge()
    {
        if(solutionSize < myComObj.currentSize * maxSize)
        {
            if(Time.time - previousTime >= increaseRate)
            {
                solutionSize += increaseAmount;
                transform.localScale = new Vector3(solutionSize, solutionSize, 0);
                previousTime = Time.time;
            }
        }
        else
        {
            if(!reachedMaxSize)
            {
                solutionSize = myComObj.currentSize * maxSize;
                transform.localScale = new Vector3(solutionSize, solutionSize, 0);
                previousTime = Time.time;
                reachedMaxSize = true;
            }
        }
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetSize(CommonObjective myObj)
    {
        myComObj = myObj;
        transform.localScale = new Vector3(solutionSize, solutionSize, 0);
        previousTime = Time.time;
        reachedMaxSize = false;
        StartCoroutine(RotationRout());
    }

    public void JellyEffect()
    {
        var twoPercent = solutionSize * .025f;
        var jellySize = new Vector3(solutionSize + twoPercent, solutionSize + twoPercent, 0);
        var solSize = new Vector3(solutionSize, solutionSize, 0);
        transform.DOScale(jellySize, jellyAnimationTime)
            .SetEase(jellyEase)
            .OnComplete(() => transform.DOScale(solSize, jellyAnimationTime).SetEase(jellyEase));
    }

    IEnumerator RotationRout()
    {
        yield return new WaitForSeconds(myComObj.maxRotationSpeed / 1.5f);
        gameObject.transform.rotation = myComObj.gameObject.transform.rotation;
        StartCoroutine(RotationRout());
    }
}
