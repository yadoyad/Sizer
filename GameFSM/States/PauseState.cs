public class PauseState : GameState
{
    bool workedAtStart = false;
    public PauseState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(MenuManager.instance.mainMenuLoad)
        {
            stateManager.SetState(new MenuLoadingState(stateManager));
        }
        else if(!MenuManager.instance.pauseEnabled)
        {
            // stateManager.objectiveManager.timeCounter.ResumeTimeCount();
            stateManager.SetState(new LevelLoadingState(stateManager));
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            stateManager.objectiveManager.timeCounter.PauseTimeCount();
            OverlayManager.instance.HideOverlay();
            workedAtStart = true;
        }
    }
}
