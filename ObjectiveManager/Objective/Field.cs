using UnityEngine;

public abstract class Field : MonoBehaviour
{
    public abstract void ObjectiveReady(CommonObjective objective);
    public abstract void SetupObjective();
    public abstract void ClearField();
    public bool fieldComplete {get; protected set;}
    public FieldAnimations animations {get; protected set;}
}
