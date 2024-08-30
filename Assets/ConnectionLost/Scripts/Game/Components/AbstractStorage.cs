using System.Collections.Generic;


namespace ConnectionLost
{
    internal class AbstractStorage<T>
    {
        private readonly List<T> _storage = new();
        internal IEnumerable<T> GetValues()
        {
            return _storage;
        }

        internal void Add(T value)
        {
            _storage.Add(value);
        }

        internal void Remove(T value)
        {
            _storage.Remove(value);
        }

        internal void Clear()
        {
            _storage.Clear();
        }
    }
}