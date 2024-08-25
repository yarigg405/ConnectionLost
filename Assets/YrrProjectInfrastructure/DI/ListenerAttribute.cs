using System;


namespace Infrastructure.DI
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class ListenerAttribute : Attribute { }
}