using System;


namespace Yrr.Entitaz
{
    public interface IEntita
    {
        T GetEntitaComponent<T>();

        bool TryGetEntitaComponent<T>(out T element);

        void AddEntitaComponent(object component, Type componentType);
    }
}
