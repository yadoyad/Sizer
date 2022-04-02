[System.Serializable]
public class UserData
{
    public bool soundsEnabled;
    public int highestScore;
    public bool tutorialShown;
    public UserData()
    {
        soundsEnabled = true;
        tutorialShown = false;
        highestScore = 0;
    }
}
