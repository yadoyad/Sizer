using UnityEngine;

public class ObjectiveAnimations : MonoBehaviour
{
    public GameObject popupTextPrefab;
    public GameObject poofParticle;

    public void SpawnPopup(string text, float seconds)
    {
        var popup = Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
        var popupSettings = popup.GetComponent<PopupText>();
        popupSettings.SetNewText(text);
        popupSettings.SetScoreText(seconds);
        popupSettings.SpawnAnimation();
        popup.transform.SetParent(gameObject.transform);
    }

    public void Poof()
    {
        var poof = Instantiate(poofParticle, transform.position, Quaternion.identity);
        Destroy(poof, 1f);
    }
}
