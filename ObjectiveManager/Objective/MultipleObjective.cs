using System.Collections.Generic;
using UnityEngine;

public class MultipleObjective : Field
{
    [SerializeField] private float maxSize = 1.5f; //Max size of the objective. Adjust value to fit in screenspace
    [SerializeField] private  float minSize = 0.7f;

    public List<CommonObjective> objectives = new List<CommonObjective>();
    
    private ObjectiveManager objectiveManager;

    private void OnEnable() 
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
        animations = GetComponent<FieldAnimations>();
        fieldComplete = true;
    }

    public override void ObjectiveReady(CommonObjective objective)
    {
        var fieldReadyCheck = true;
        for(int i = 0; i < objectives.Count; i++)
        {
            if(!objectives[i].solutionProvided)
            {
                fieldReadyCheck = false;
            }
        }
        fieldComplete = fieldReadyCheck;
        objectiveManager.CheckFieldReadiness();
    }

    public override void SetupObjective()
    {
        foreach(CommonObjective objective in objectives)
        {
            var objSize = Random.Range(minSize, maxSize);
            objective.SetUp(objSize);
        }
        fieldComplete = false;
    }

    public override void ClearField()
    {
        foreach(CommonObjective objective in objectives)
        {
            objective.ClearSolution();
        }
    }
}
