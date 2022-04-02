using UnityEngine;
using DG.Tweening;

public class MenuField : MonoBehaviour
{
    [SerializeField] private Ease animationEase = Ease.Linear;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private Vector2 hidePosition = Vector2.zero;
    public bool rollOutFinished {get; private set;} = false;
    private Transform myTrans;

    private void OnEnable() 
    {
        myTrans = GetComponent<Transform>();
    }

    public void RollOut()
    {
        rollOutFinished = false;
        myTrans.position = hidePosition;
        myTrans.DOMoveX(0, animationTime)
            .SetEase(animationEase)
            .OnComplete(() => rollOutFinished = true);
    }

    public void Hide()
    {
        myTrans.DOMoveX(-hidePosition.x, animationTime)
            .SetEase(animationEase);
    }
}
