using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopupText : MonoBehaviour
{
    public TextMeshPro popupText;
    public TextMeshPro scoreText;
    public float animationTime = 1f;
    public float positionOffset = 10f;
    private RectTransform rectTransform;

    private void OnEnable() 
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetNewText(string s)
    {
        popupText.text = s + "%";
    }

    public void SetScoreText(float f)
    {
        string result = "";

        if(f != 0)
        {
            result = f.ToString();
            var sign = f >= 0 ? "+" : "";
            scoreText.text = sign + result + " sec";
        }
        else
        {
            scoreText.text = "";
        }
    }
    public void SpawnAnimation()
    {
        rectTransform.DOMoveY(rectTransform.position.y + positionOffset, animationTime)
            .OnComplete(() => ReduceTransparency());
    }

    private void ReduceTransparency()
    {
        DOTween.ToAlpha(() => scoreText.color, x => scoreText.color = x, 0f, animationTime / 3);
        DOTween.ToAlpha(() => popupText.color, x => popupText.color = x, 0f, animationTime / 3)
            .OnComplete(()=>Destroy(gameObject));
    } 
}
