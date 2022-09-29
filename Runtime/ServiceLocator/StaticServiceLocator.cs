using System;
using ServiceLocator.StaticContainer;

namespace ServiceLocator
{
    public sealed class StaticServiceLocator : IServiceLocator
    {
        public T Resolve<T>()
        {
            return Keeper<T>.Resolve();
        }
        
        public void RegisterSingleton<T>(T instance)
        {
            Keeper<T>.RegisterSingleton(instance);
        }

        public void RegisterTransient<T>(Func<T> resolver)
        {
            Keeper<T>.RegisterTransient(resolver);
        }
        
        public void RegisterLazySingleton<T>(Func<T> resolver)
        {
            Keeper<T>.RegisterLazySingleton(resolver);
        }
    }
}