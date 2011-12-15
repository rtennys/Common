using System;
using Microsoft.Practices.ServiceLocation;

namespace Common
{
    public static class Ioc
    {
        public static IServiceLocator CurrentLocator
        {
            get { return ServiceLocator.Current; }
        }

        public static IServiceLocator Initialize(IServiceLocator serviceLocator)
        {
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
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