using System.Collections;
using UnityEngine;

public class CommonObjective : MonoBehaviour
{
    public bool solutionProvided {get; private set;} = false;
    public float currentSize {get; private set;} = 0;
    private int currentRotation = 1; //-1 0 1
    public int currentSprite {get; private set;} = 0;
    public float maxRotationSpeed = .1f;
    [SerializeField] private int maxRotationAmount = 1;
    [SerializeField] private float rotationSpeedModifier = 5;
    public int spriteNumber {get; private set;} = 0;
    private SpriteRenderer spriteRenderer;
    private ObjectiveManager manager;
    private ObjectiveAnimations animations;
    public Solution mySolution {get; private set;}
    private Field myField;
    private float nextZ = 0;

    private void OnEnable() 
    {
        manager = FindObjectOfType<ObjectiveManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myField = GetComponentInParent<Field>();
        animations = GetComponent<ObjectiveAnimations>();
    }
    
    public void CreateSolution()
    {
        if(!mySolution)
        {
            var tmpSolution = Instantiate(manager.solutionPrefab, transform.position, Quaternion.identity);
            mySolution = tmpSolution.GetComponent<Solution>();
            mySolution.transform.SetParent(myField.gameObject.GetComponent<Transform>());
            mySolution.SetSprite(manager.solutionSprites[currentSprite]);
            mySolution.SetSize(this);

            AudioManager.instance.Play("Enlarge");
        }
    }

    public void EnlargeSolution()
    {
        if(mySolution && !solutionProvided)
        {
            mySolution.Enlarge();
        }
    }

    public void ProvideSolution()
    {
        if(solutionProvided)
            return;
            
        StopRotation();
        AudioManager.instance.FadeOut("Enlarge");

        var rawResult = Mathf.CeilToInt((mySolution.solutionSize * 100) / currentSize);
        var correctResult = rawResult > 100 ? 200 - rawResult : rawResult;

        mySolution.JellyEffect();

        ScoreManager.instance.AddScore(correctResult);
        float addedSeconds = TimeCounter.instance.AddTimeFromResult(correctResult);
        
        animations.SpawnPopup(rawResult.ToString(), addedSeconds);
        manager.Summary();

        if(correctResult == ScoreManager.instance.perfectScore)
        {
            AudioManager.instance.Play("Medal");
            // animations.Poof(); Не готово, particle effect надо доработать
            MedalCount.instance.AddMedal();
        }

        solutionProvided = true;
        myField.ObjectiveReady(this);
        AudioManager.instance.Play("Ding");
    }


    #region SETUP
    public void SetUp(float someSize)
    {
        SetSize(someSize);
        SetSprite();
        SetRotation();
        solutionProvided = false;
    }

    private void SetSize(float someSize)
    {
        currentSize = someSize;
        transform.localScale = new Vector3(currentSize,currentSize,0);
    }

    private void SetSprite()
    {
        spriteNumber = UnityEngine.Random.Range(0, manager.objectiveSprites.Count);
        currentSprite = spriteNumber;
        spriteRenderer.sprite = manager.objectiveSprites[spriteNumber];

    }

    private void SetRotation()
    {
        StopRotation();
        maxRotationAmount = UnityEngine.Random.Range(-1,2);
        currentRotation = maxRotationAmount;
        nextZ = gameObject.transform.rotation.z;
        StartCoroutine(RotationRoutine());
    }

    #endregion

    public void ClearSolution()
    {
        if(mySolution)
        {
            Destroy(mySolution.gameObject);
        }
    }
    public void StopRotation() => StopAllCoroutines();

    IEnumerator RotationRoutine()
    {
        // yield return new WaitForSeconds(maxRotationSpeed);

        if(nextZ >= 360f || nextZ <= -360f)
        {
            nextZ = 0;
        }
        
        float timeElapsed = 0;
        nextZ += maxRotationAmount * rotationSpeedModifier;
        Transform tempTransform = gameObject.transform;
        Vector3 tVector = new Vector3(0,0, nextZ);
        tempTransform.rotation = Quaternion.Euler(tVector);


        while(timeElapsed < maxRotationSpeed)
        {
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, tempTransform.rotation, timeElapsed / maxRotationSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.rotation = tempTransform.rotation;
        StartCoroutine(RotationRoutine());
    }
}
