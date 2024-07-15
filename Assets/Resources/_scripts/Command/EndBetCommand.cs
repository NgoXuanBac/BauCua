
public class EndBetCommand : ICommand
{
    private BettingController _bettingCtrl;

    public EndBetCommand(BettingController bettingCtrl)
    {
        _bettingCtrl = bettingCtrl;
    }

    public void Execuate()
    {
        _bettingCtrl.SetBetStatus(false);
    }
}