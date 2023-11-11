using System;


namespace Game.ECS.Views
{
    [Serializable]
    public enum NodeCondition
    {
        Closed,
        Opened,
        HaveContent,
        Blocked,
        Empty,
    }
}
