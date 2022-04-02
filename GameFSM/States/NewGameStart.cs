public class NewGameStart : GameState
{
    bool workedAtStart = false;
    public NewGameStart(GameStateManager stateManager) : base(stateManager)
    {
    }

    public override void StateJob()
    {
        DoAtStart();
    }

    private void DoAtStart()
    {
        if(!workedAtStart)
        {
            workedAtStart = true;
            TotalReset();
            stateManager.objectiveManager.SetField(FieldManager.instance.ChooseNextField());
            stateManager.SetState(new LevelLoadingState(stateManager));
        }
    }

    private void TotalReset()
    {
        FieldManager.instance.HideMenuField();
        FieldManager.instance.ClearFields();
        stateManager.objectiveManager.ClearField();
        ScoreManager.instance.ClearScore();
        TimeCounter.instance.TimerReset();
        MedalCount.instance.ClearMedals();
    }
}
