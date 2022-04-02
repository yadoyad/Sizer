using UnityEngine;

public class MedalCount : MonoBehaviour
{
    public static MedalCount instance;
    public int totalMedals {get; private set; } = 0;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddMedal()
    {
        totalMedals++;
        OverlayManager.instance.ShowPopup();
    } 

    public void RemoveMedal(int value)
    {
        if(totalMedals >= value)
        {
            totalMedals -= value;
        }
        else
        {
            totalMedals = 0;
        }
    }

    public void ClearMedals()
    {
        totalMedals = 0;
    }
}
