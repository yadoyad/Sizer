public class MenuLoadingState : GameState
{
    bool workedAtStart = false;
    public MenuLoadingState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(MenuManager.instance.menuEnabled)
            stateManager.SetState(new InMenuState(stateManager));
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            stateManager.objectiveManager.timeCounter.PauseTimeCount();
            OverlayManager.instance.HideOverlay();
            FieldManager.instance.ShowMenuField();
            stateManager.objectiveManager.HideField();
            workedAtStart = true;
        }
    }
}
