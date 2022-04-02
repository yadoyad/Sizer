using System.Collections;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    [SerializeField] private float ressurectionSeconds = 5f;
    [SerializeField] private float refreshRate = 0.01f;
    public float maxTime {get; private set;} = 15f;
    public float currentTime {get; private set;} = 0;
    public bool timerWorking {get; private set;} = false;
    public bool timeOver {get; private  set;} = false;
    [SerializeField] private float perfectSeconds = 2f;
    [SerializeField] private float amazingSeconds = 1.5f;
    [SerializeField] private float greatSeconds = 1f;
    [SerializeField] private float goodSeconds = .5f;
    [SerializeField] private float badSeconds = .5f;
    [SerializeField] private float awfulSeconds = .5f;
    private IEnumerator routine;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start() 
    {
        currentTime = maxTime;
    }

    public void StartTimeCount()
    {
        routine = CountRoutine();
        StartCoroutine(routine);
        timerWorking = true;
    }

    public void PauseTimeCount()
    {
        StopAllCoroutines();
    }

    public void ResumeTimeCount()
    {
        routine = CountRoutine();
        StartCoroutine(routine);
    }

    public void ReduceTime(float amount)
    {
        if(currentTime - amount > 0)
        {
            currentTime -= amount;
        }
        else
        {
            currentTime = 0;
            timeOver = true;
            timerWorking = false;
        }
        OverlayManager.instance.TimerTextUpdate(currentTime);
    }

    public void TimerReset()
    {
        PauseTimeCount();
        currentTime = maxTime;
        timeOver = false;
        timerWorking = false;
        OverlayManager.instance.TimerTextUpdate(currentTime);
    }

    public void RessurectionTime()
    {
        PauseTimeCount();
        AddTime(ressurectionSeconds);
        timerWorking = false;
        timeOver = false;
        OverlayManager.instance.TimerTextUpdate(currentTime);
    }

    public float AddTimeFromResult(int value)
    {
        float result = 0;

        if(value == ScoreManager.instance.perfectScore)
        {
            result = perfectSeconds;
            AddTime(perfectSeconds);
        }
        else if(value < ScoreManager.instance.perfectScore && value >= ScoreManager.instance.amazingScore)
        {
            result = amazingSeconds;
            AddTime(amazingSeconds);
        }
        else if(value < ScoreManager.instance.amazingScore && value >= ScoreManager.instance.greatScore)
        {
            result = greatSeconds;
            AddTime(greatSeconds);
        }
        else if(value < ScoreManager.instance.greatScore && value >= ScoreManager.instance.goodScore)
        {
            result = goodSeconds;
            AddTime(goodSeconds);
        }
        else if(value < ScoreManager.instance.badScore && value >= ScoreManager.instance.awfulScore)
        {
            result = badSeconds;
            AddTime(badSeconds);
        }
        else if(value < ScoreManager.instance.awfulScore)
        {
            result = awfulSeconds;
            AddTime(awfulSeconds);
        }

        return result;
    }
    
    private void AddTime(float amount)
    {
        if(currentTime + amount < maxTime)
        {
            currentTime += amount;
        }
        else
        {
            currentTime = maxTime;
        }
        OverlayManager.instance.TimerTextUpdate(currentTime);
    }

    IEnumerator CountRoutine()
    {
        yield return new WaitForSeconds(refreshRate);
        
        ReduceTime(refreshRate);

        routine = CountRoutine();
        StartCoroutine(routine);
    }
}
