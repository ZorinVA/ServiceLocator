
using System;

namespace ServiceLocator.StaticContainer
{
    internal static class Keeper<T>
        {
            private static bool _isRegistered;
            private static bool _isLazy;
            private static bool _isSingle;
            private static T _instance;
            private static Func<T> _resolver;
            
            public static T Resolve()
            {
                if (!_isRegistered)
                {
                    throw new Exception($"[{nameof(StaticServiceLocator)}] Type [{typeof(T)}] not exist.");
                }

                if (_isLazy)
                {
                    _isLazy = false;
                    _instance = _resolver.Invoke();
                }

                return _isSingle ? _instance : _resolver.Invoke();
            }
            
            public static void RegisterSingleton(T instance)
            {
                if (_isRegistered)
                {
                    throw new Exception($"[{nameof(StaticServiceLocator)}] Type [{typeof(T)}] already exist.");
                }
                
                _isRegistered = true;
                _isSingle = true;
                _instance = instance;
            }

            public static void RegisterTransient(Func<T> resolver)
            {
                if (_isRegistered)
                {
                    throw new Exception($"[{nameof(StaticServiceLocator)}] Type [{typeof(T)}] already exist.");
                }
                
                _isRegistered = true;
                _isSingle = false;
                _resolver = resolver;
            }
            
            public static void RegisterLazySingleton(Func<T> resolver)
            {
                if (_isRegistered)
                {
                    throw new Exception($"[{nameof(StaticServiceLocator)}] Type [{typeof(T)}] already exist.");
                }
                
                _isRegistered = true;
                _isSingle = true;
                _isLazy = true;
                _resolver = resolver;
            }
        }
}