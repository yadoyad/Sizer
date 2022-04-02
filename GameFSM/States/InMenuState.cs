public class InMenuState : GameState
{
    public InMenuState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        if(MenuManager.instance.newGameStarted)
            stateManager.SetState(new NewGameStart(stateManager));
    }
}
