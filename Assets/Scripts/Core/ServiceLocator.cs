using System;
using System.Collections.Generic;

namespace Core
{
    public static class ServiceLocator
    {
        static Dictionary<Type, object> services = new();

        public static void RegisterService<T>(T service)
        {
            var type = typeof(T);
            services.TryAdd(type, service);
        }

        public static T GetService<T>()
        {
            var type = typeof(T);

            if (services.TryGetValue(type, out var service))
                return (T)service;

            throw new Exception($"Service {type} not found");
        }
    }
}