public class SummaryState : GameState
{
    private bool workedAtStart = false;
    public SummaryState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(MenuManager.instance.pauseEnabled)
        {
            stateManager.objectiveManager.FinishSummaryRout();
            stateManager.SetState(new PauseState(stateManager));
        }
        else if(stateManager.objectiveManager.summaryShown)
        {
            stateManager.SetState(new LevelLoadingState(stateManager));
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            stateManager.objectiveManager.SummaryRout();
            stateManager.objectiveManager.timeCounter.PauseTimeCount();
            workedAtStart = true;
        }
    }
}
