using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

public class SaveLoadCtrl : MonoBehaviour
{
    public static SaveLoadCtrl instance;
    public UserData userData {get; private set;}
    private ISaveGameSerializer mySerializer = new SaveGameBinarySerializer();
    private const string identifier = "c308_lkx4";
    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable() 
    {
        LoadUD();
    }
    public void UpdateHighscoreData(int score)
    {
        userData.highestScore = score;
        SaveUD();
    }

    public void UpdateSoundsData(bool soundsOn)
    {
        userData.soundsEnabled = soundsOn;
        SaveUD();
    }

    public void TutorialShown()
    {
        userData.tutorialShown = true;
        SaveUD();
    }

    //Not implemented yet
    // public void FlushUD()
    // {
    //     userData = new UserData();
    //     SaveUD();
    // }
    public void SaveUD()
    {
        // SaveGame.Save<UserData>(identifier, userData, mySerializer);
    }

    public void LoadUD()
    {
        if(SaveGame.Exists(identifier))
        {
            userData = SaveGame.Load<UserData>(identifier, new UserData(), mySerializer);
        }
        else
        {
            userData = new UserData();
            SaveUD();
        }
    }
}
