using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [Header("Настройки задания")]
    public List<Sprite> objectiveSprites = new List<Sprite>();
    [Header("Настройки решения")]
    public GameObject solutionPrefab;
    public List<Sprite> solutionSprites = new List<Sprite>();
    [SerializeField] private float summaryAnimationTime = .5f;
    public bool solutionProvided {get; private set;} = false;
    public bool summaryShown {get; private set;} = false;
    public TimeCounter timeCounter {get; private set;}
    public ScoreManager scoreManager {get; private set;}
    public Field currentField {get; private set;}
    private GameStateManager stateManager;

    private void OnEnable() 
    {
        stateManager = FindObjectOfType<GameStateManager>();
        timeCounter = GetComponentInChildren<TimeCounter>();
        scoreManager = GetComponentInChildren<ScoreManager>();
    }
    public void SetField(Field f)
    {
        if(currentField)
        {
            currentField.animations.Hide();
        }
        
        currentField = f;
        f.SetupObjective();
        f.animations.RollOut();
    }

    public void ClearField()
    {
        currentField = null;
    }

    public void HideField()
    {
        currentField.animations.Hide();
    }

    public void CheckFieldReadiness()
    {
        solutionProvided = currentField.fieldComplete;
    }

    public void Summary()
    {
        summaryShown = false;
    }

    public void SummaryRout() => StartCoroutine(SummaryRoutine());

    public void FinishSummaryRout()
    {
        StopAllCoroutines();
        summaryShown = true;
        solutionProvided = false;
    }

    IEnumerator SummaryRoutine()
    {
        yield return new WaitForSeconds(summaryAnimationTime);
        summaryShown = true;
        solutionProvided = false;
    }
}
