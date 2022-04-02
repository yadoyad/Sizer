using UnityEngine;

public class SingleObjective : Field
{
    private CommonObjective myObjective;
    [SerializeField] private float maxSize = 1.5f;
    [SerializeField] private  float minSize = 0.7f;
    private ObjectiveManager objectiveManager;

    private void OnEnable() 
    {
        myObjective = GetComponentInChildren<CommonObjective>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        animations = GetComponent<FieldAnimations>();
        fieldComplete = true;
    }

    public override void SetupObjective()
    {
        var objSize = Random.Range(minSize, maxSize);
        myObjective.SetUp(objSize);
        fieldComplete = false;
    }

    public bool SolutionProvided() => myObjective.solutionProvided;

    public override void ObjectiveReady(CommonObjective objective)
    {
        if(objective.solutionProvided)
        {
            fieldComplete = true;
            objectiveManager.CheckFieldReadiness();
        }
    }

    public override void ClearField()
    {
        myObjective.ClearSolution();
    }
}
