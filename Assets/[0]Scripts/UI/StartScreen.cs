using VContainer;
using Yrr.UI;


namespace Game.UI
{
    internal sealed class StartScreen : UIScreen
    {
        [Inject] private readonly GameStarter gameStarter;


        public void ClickOnPlay()
        {
            gameStarter.StartGrid();
            Hide();
        }
    }
}
