using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericPractice
{
    public interface IStorage<TKey>
    {
        T Create<T>() where T : new();
        IDictionary<TKey, T> GetWith<T>();
        T Get<T>(TKey guid);
    }

    public class GuidStorage : IStorage<Guid>
    {
        private Dictionary<Guid, object> Storage { get; } = new Dictionary<Guid, object>();

        public T Create<T>() where T : new()
        {
            var entity = new T();
            var guid = new Guid();
            Storage[guid] = entity;
            return entity;
        }

        public IDictionary<Guid, T> GetWith<T>()
        {
            return Storage
                .Where(x => x.Value is T)
                .ToDictionary(x => x.Key, x => (T) x.Value);
        }

        public T Get<T>(Guid guid)
        {
            if (!Storage.ContainsKey(guid))
                return default(T);
            var entity = Storage[guid];
            if (entity is T)
                return (T) entity;
            return default(T);
        }
    }
}