
public class StartBetCommand : ICommand
{
    private BettingController _bettingCtrl;

    public StartBetCommand(BettingController bettingCtrl)
    {
        _bettingCtrl = bettingCtrl;
    }

    public void Execuate()
    {
        _bettingCtrl.SetBetStatus(true);
    }
}