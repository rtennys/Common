using System;

namespace Common
{
    public static class EnumHelper
    {
        public static string[] GetNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }

        public static T[] GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).As<T[]>();
        }

        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}