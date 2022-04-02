public class GameOverState : GameState
{
    bool workedAtStart = false; 
    public GameOverState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(!MenuManager.instance.timeOverEnabled)
        {
            if(MenuManager.instance.mainMenuLoad)
            {
                stateManager.SetState(new MenuLoadingState(stateManager));
            }
            else
            {
                if(RessurectionCtrl.instance.ressurecting)
                {
                    stateManager.SetState(new RessurectionState(stateManager));
                }
            }
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            OverlayManager.instance.HideOverlay();
            MenuManager.instance.EnableTimeOverMenu();
            workedAtStart = true;
        }
    }
}
