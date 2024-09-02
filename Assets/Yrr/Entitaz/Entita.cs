using System;
using System.Collections.Generic;
using UnityEngine;


namespace Yrr.Entitaz
{
    public class Entita : MonoBehaviour, IEntita
    {
        private readonly Dictionary<Type, object> _components = new();

        public virtual void SetupEntita()
        {
            var childrenComponents = GetComponentsInChildren<IEntitazComponent>(true);

            for (int i = 0; i < childrenComponents.Length; i++)
            {
                var child = childrenComponents[i];
                AddEntitaComponent(child);
            }
        }

        public T GetEntitaComponent<T>()
        {
            return (T)_components[typeof(T)];
        }

        public IEnumerable<T> GetEntitaComponents<T>()
        {
            foreach (var pair in _components)
                if (pair.Key is T)
                    yield return (T)pair.Value;
        }

        public bool TryGetEntitaComponent<T>(out T element)
        {
            if (_components.TryGetValue(typeof(T), out var result))
            {
                element = (T)result;
                return true;
            }

            element = default;
            return false;
        }

        public void AddEntitaComponent(object component)
        {
            _components.Add(component.GetType(), component);
        }

        public void AddEntitaComponent(object component, Type componentType)
        {
            _components.Add(componentType, component);
        }
    }
}
