using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int perfectScore {get; private set;} = 100;
    public int amazingScore {get; private set;} = 90;
    public int greatScore {get; private set;} = 75;
    public int goodScore {get; private set;} = 60;
    public int badScore {get; private set;} = 40;
    public int awfulScore {get; private set;} = 20;
    public int currentScore {get; private set;} = 0;
    public event Action OnAddScore;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        currentScore += value;
        currentScore = currentScore < 0 ? 0 : currentScore;
        OverlayManager.instance.ScoreTextUpdate(currentScore);
        OnAddScore?.Invoke();
    }

    public bool CheckScore()
    {
        bool result = false;
        
        var currentHighscore = SaveLoadCtrl.instance.userData.highestScore;

        if(currentScore > currentHighscore)
        {
            result = true;
            SaveLoadCtrl.instance.UpdateHighscoreData(currentScore);
        }
        
        return result;
    }

    public void ClearScore()
    {
        currentScore = 0;
        OverlayManager.instance.ScoreTextUpdate(currentScore);
    }
}
