using System;
using Microsoft.Practices.ServiceLocation;

namespace Common
{
    public static class Ioc
    {
        public static IServiceLocator CurrentLocator { get; private set; }

        public static IServiceLocator Initialize(IServiceLocator serviceLocator)
        {
            CurrentLocator = serviceLocator;
            ServiceLocator.SetLocatorProvider(() => CurrentLocator);
            return serviceLocator;
        }

        public static T Resolve<T>()
        {
            return CurrentLocator.GetInstance<T>();
        }

        public static object Resolve(Type type)
        {
            return CurrentLocator.GetInstance(type);
        }
    }
}