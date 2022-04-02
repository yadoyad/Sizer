using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameState State {get; private set;}
    [HideInInspector] public ObjectiveManager objectiveManager;

    private void OnEnable() 
    {
        objectiveManager = FindObjectOfType<ObjectiveManager>();
    }

    void Start()
    {
        SetState(new InMenuState(this));
    }

    private void Update() 
    {
        State.StateJob();
    }

    public void SetState(GameState state)
    {
        State = state;
    }
}
