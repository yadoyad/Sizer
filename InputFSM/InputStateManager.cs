using UnityEngine;

public class InputStateManager : MonoBehaviour
{
    public InputState State {get; private set;}
    [HideInInspector] public GameStateManager gameStateManager;
    [HideInInspector] public ObjectiveManager objectiveManager;
    public CommonObjective currentObjective {get; private set;}

    private void OnEnable() 
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    private void Start() 
    {
        SetState(new FirstTouchState(this));
    }

    private void Update() 
    {
        State.StateJob();
    }

    public void SetState(InputState state)
    {
        State = state;
    }

    public void SetObjective(CommonObjective cObj)
    {
        currentObjective = cObj;
        CreateSolutionForCurrentObjective();
    }

    private void CreateSolutionForCurrentObjective()
    {
        currentObjective.CreateSolution();
    }
}
