using VContainer;
using Yrr.UI;


namespace ConnectionLost
{
    internal sealed class PlayerLoseScreen : UIScreen
    {
        [Inject] private readonly GridStarter _gridStarter;

        public void ClickOnButton()
        {
            _gridStarter.StartGrid();
            Hide();
        }
    }
}