using Yrr.Utils;


namespace Game.Player
{
    internal sealed class PlayerProgress
    {
        public ReactiveInt LastCompletedLevel = new ReactiveInt(0);
        public ReactiveInt CurrentLevel = new ReactiveInt(0); 
    }
}
