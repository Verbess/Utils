using System;
using System.Reflection;

namespace Utils.Singleton
{
    internal static class SingletonCreator
    {
        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            Type type = typeof(T);
            ConstructorInfo[] ctorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            ConstructorInfo ctorInfo = Array.Find<ConstructorInfo>(ctorInfos, c => c.GetParameters().Length == 0);

            if (ctorInfo == null)
            {
                string message = String.Format("Non-Public Constructor() not found in {0}!", type);

                throw new Exception(message);
            }
            else
            {
                object obj = ctorInfo.Invoke(null);
                T instance = obj as T;

                return instance;
            }
        }
    }
}