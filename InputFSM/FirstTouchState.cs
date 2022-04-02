using UnityEngine;

public class FirstTouchState : InputState
{
    public FirstTouchState(InputStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        if(stateManager.gameStateManager.State.GetType() == typeof(PlayState) && !stateManager.objectiveManager.solutionProvided)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                int lm = LayerMask.GetMask("InputBox");

                if(Physics.Raycast(ray, out hit, lm))
                {
                    if(hit.collider.gameObject.tag == "CommonObjective")
                    {
                        var someObj = hit.collider.gameObject.GetComponent<CommonObjective>();
                        stateManager.SetObjective(someObj);
                        
                        if(!TimeCounter.instance.timerWorking)
                        {
                            TimeCounter.instance.StartTimeCount();
                        }

                        if(Tutorial.instance.tutorialActive)
                        {
                            Tutorial.instance.StopTutorial();
                        }

                        stateManager.SetState(new HoldingState(stateManager));
                    }
                }
            }
        }
    }
}
