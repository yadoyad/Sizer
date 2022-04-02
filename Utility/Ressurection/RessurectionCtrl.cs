using System.Collections;
using UnityEngine;

public class RessurectionCtrl : MonoBehaviour
{
    public static RessurectionCtrl instance;
    public int ressurectionPrice {get; private set;} = 1;
    public bool ressurecting {get; private set;} = false;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public bool MedalsCheck() => MedalCount.instance.totalMedals >= ressurectionPrice;

    public void RessurectionForMedals()
    {
        MedalCount.instance.RemoveMedal(ressurectionPrice);
        Ressurect();
        ressurectionPrice *= 2;
    }

    private void Ressurect()
    {
        ressurecting = true;
        TimeCounter.instance.RessurectionTime();
        StartCoroutine(RessurectionRoutine()); //Необходимо отключать ressurecting чтобы правильно работали GameState'ы
    }

    public void RessurectionForAds()
    {
        Ressurect();
    }

    public void RestorePrice() => ressurectionPrice = 2;

    IEnumerator RessurectionRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        ressurecting = false;
    }
}
