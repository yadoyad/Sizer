public class RessurectionState : GameState
{
    bool workedAtStart = false;
    public RessurectionState(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
        if(!TimeCounter.instance.timeOver)
        {
            stateManager.SetState(new PlayState(stateManager));
        }
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            // TimeCounter.instance.RessurectionTime();
            workedAtStart = true;
        }
    }
}
