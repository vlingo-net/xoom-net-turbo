using System;
using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo
{
    public class ComponentRegistry
    {
        private static readonly IDictionary<string, object> COMPONENTS = new Dictionary<string, object>();

        public static void Register(string componentName, object componentInstance)
        {
            COMPONENTS.Add(componentName, componentInstance);
        }

        public static void Register(Type componentClass, object componentInstance)
        {
            COMPONENTS.Add(componentClass.FullName, componentInstance);
        }

        public static bool Has(Type componentClass) => COMPONENTS.ContainsKey(componentClass.FullName);

        public static object WithName(string name) => COMPONENTS[name];

        public static object WithType(Type componentClass) => WithName(componentClass.FullName);

        public static bool Has(string componentName) => COMPONENTS.ContainsKey(componentName);

        public static void Clear()
        {
            COMPONENTS.Clear();
        }
    }
}
