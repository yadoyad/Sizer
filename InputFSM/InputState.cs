public abstract class InputState
{
    protected InputStateManager stateManager;

    public InputState(InputStateManager stateManager)
    {
        this.stateManager = stateManager;
    }
    public abstract void StateJob();
}
