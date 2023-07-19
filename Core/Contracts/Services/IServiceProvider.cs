using System;

namespace Recommender.Core.Services
{
    public interface IServiceProvider
    {
        void RegisterService<T>(T service);

        T GetService<T>();

        object GetService(Type serviceType);

        bool IsServiceRegistered<T>();
    }
}
