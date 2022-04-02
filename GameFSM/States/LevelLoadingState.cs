public class LevelLoadingState : GameState
{
    bool workedAtStart = false;
    public LevelLoadingState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(stateManager.objectiveManager.currentField.animations.rollOutFinished)
        {
            if(MenuManager.instance.mainMenuLoad)
            {
                stateManager.SetState(new MenuLoadingState(stateManager));
            }
            else if(MenuManager.instance.pauseEnabled)
            {
                stateManager.SetState(new PauseState(stateManager));
            }
            else
            {
                if(TimeCounter.instance.timerWorking)
                {
                    TimeCounter.instance.ResumeTimeCount();
                }
                stateManager.SetState(new PlayState(stateManager));
            }
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            if(stateManager.objectiveManager.currentField)
            {
                if(stateManager.objectiveManager.currentField.fieldComplete)
                    stateManager.objectiveManager.SetField(FieldManager.instance.ChooseNextField());   
            }
            workedAtStart = true;
        }
    }
}
