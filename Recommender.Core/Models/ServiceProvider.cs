using System;
using System.Collections.Generic;
using IServiceProvider = Recommender.Core.Services.IServiceProvider;

namespace Recommender.Core.Models
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, object> _services;

        public ServiceProvider()
        {
            _services = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
               
            var serviceType = typeof(T);

            if(!_services.ContainsKey(serviceType))
            {
                _services.Add(serviceType, service);
            }
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
               
            if (!_services.TryGetValue(serviceType, out var service))
            {
                throw new InvalidOperationException($"Service of type '{serviceType.Name}' has not been registered.");
            }
               
            return service;
        }

        public bool IsServiceRegistered<T>()
        {
            var serviceType = typeof(T);

            return _services.ContainsKey(serviceType);
        }
    }
}
