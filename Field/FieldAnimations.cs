using UnityEngine;
using DG.Tweening;

public class FieldAnimations : MonoBehaviour
{
    [SerializeField] private Ease animationEase = Ease.Linear;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private Vector2 hidePosition = Vector2.zero;
    public bool rollOutFinished {get; private set;} = false;
    private Transform myTrans;
    private Field myField;

    private void OnEnable() 
    {
        myTrans = GetComponent<Transform>();
        myField = GetComponent<Field>();
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
            .SetEase(animationEase)
            .OnComplete(() => myField.ClearField());
    }
}
