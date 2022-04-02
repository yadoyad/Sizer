public class PlayState : GameState
{
    bool workedAtStart = false;
    public PlayState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(MenuManager.instance.pauseEnabled)
        {
            stateManager.SetState(new PauseState(stateManager));
        }
        if(stateManager.objectiveManager.solutionProvided)
        {
            stateManager.SetState(new SummaryState(stateManager));
        }
        if(TimeCounter.instance.timeOver)
        {
            stateManager.SetState(new GameOverState(stateManager));
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            OverlayManager.instance.ShowOverlay();
            Tutorial.instance.CheckTutorial();
            workedAtStart = true;
        }
    }
}
