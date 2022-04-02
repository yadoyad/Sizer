using UnityEngine;

public class HoldingState : InputState
{
    public HoldingState(InputStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        if(stateManager.gameStateManager.State.GetType() == typeof(PlayState) && !stateManager.objectiveManager.solutionProvided)
        {
            if(TimeCounter.instance.timeOver)
            {
                stateManager.currentObjective.ProvideSolution();
                stateManager.SetState(new FirstTouchState(stateManager));
            }
            else
            {
                if(Input.GetMouseButton(0))
                {
                    stateManager.currentObjective.EnlargeSolution();
                }
                if(Input.GetMouseButtonUp(0))
                {
                    stateManager.currentObjective.ProvideSolution();
                    stateManager.SetState(new FirstTouchState(stateManager));
                }
            }
        }
    }
}
