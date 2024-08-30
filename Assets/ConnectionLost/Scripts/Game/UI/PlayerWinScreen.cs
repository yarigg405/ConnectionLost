using VContainer;
using Yrr.UI;


namespace ConnectionLost
{
    internal sealed class PlayerWinScreen : UIScreen
    {
        [Inject] private readonly GridStarter _gridStarter;

        public void ClickOnButton()
        {
            _gridStarter.NextGrid();
            Hide();
        }
    }
}