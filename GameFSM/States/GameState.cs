public abstract class GameState
{
    protected GameStateManager stateManager;

    public GameState(GameStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public abstract void StateJob();
}
