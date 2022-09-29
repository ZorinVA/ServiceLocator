using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    public sealed class DictionaryServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _instanceStorage;
        private readonly Dictionary<Type, Func<object>> _transientStorage;
        private readonly Dictionary<Type, Func<object>> _lazyStorage;

        public DictionaryServiceLocator(int capacity)
        {
            _instanceStorage = new Dictionary<Type, object>(capacity);
            _transientStorage = new Dictionary<Type, Func<object>>(capacity);
            _lazyStorage = new Dictionary<Type, Func<object>>(capacity);
        }

        public T Resolve<T>()
        {
            var type = typeof(T);

            if (_instanceStorage.ContainsKey(type))
            {
                return (T)_instanceStorage[type];
            }

            if (_transientStorage.ContainsKey(type))
            {
                return (T)_transientStorage[type].Invoke();
            }
            
            if (_lazyStorage.ContainsKey(type))
            {
                _instanceStorage[type] = _lazyStorage[type].Invoke();
                _lazyStorage.Remove(type);
                
                return (T)_instanceStorage[type];
            }
            
            throw new Exception($"[{nameof(DictionaryServiceLocator)}] Type [{type}] not exist.");
        }

        public void RegisterSingleton<T>(T instance)
        {
            var type = typeof(T);

            ContainsAssert(type);

            _instanceStorage.Add(type, instance);
        }

        public void RegisterTransient<TKey>(Func<TKey> resolver)
        {
            var type = typeof(TKey);

            ContainsAssert(type);

            _transientStorage[type] = () => resolver.Invoke();
        }

        public void RegisterLazySingleton<TKey>(Func<TKey> resolver)
        {
            var type = typeof(TKey);

            ContainsAssert(type);
            
            _lazyStorage[type] = () => resolver.Invoke();
        }

        private void ContainsAssert(Type type)
        {
            if (_instanceStorage.ContainsKey(type))
            {
                throw new Exception($"[{nameof(DictionaryServiceLocator)}] Type [{type}] already register as instance.");
            }

            if (_transientStorage.ContainsKey(type))
            {
                throw new Exception($"[{nameof(DictionaryServiceLocator)}] Type [{type}] already register as transient.");
            }
            
            if (_lazyStorage.ContainsKey(type))
            {
                throw new Exception($"[{nameof(DictionaryServiceLocator)}] Type [{type}] already register as lazy.");
            }
        }
    }
}