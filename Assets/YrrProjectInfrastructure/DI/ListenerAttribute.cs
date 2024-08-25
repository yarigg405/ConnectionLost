using System;


namespace Infrastructure.DI
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ListenerAttribute : Attribute { }
}