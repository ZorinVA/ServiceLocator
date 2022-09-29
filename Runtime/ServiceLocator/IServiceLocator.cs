using System;

namespace ServiceLocator
{
    public interface IServiceLocator
    {
        T Resolve<T>();
        void RegisterSingleton<T>(T instance);
        void RegisterTransient<T>(Func<T> resolver);
        void RegisterLazySingleton<T>(Func<T> resolver);
    }
}