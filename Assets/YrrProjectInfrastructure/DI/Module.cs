using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Infrastructure.DI
{
    public abstract class Module : MonoBehaviour
    {
        internal IEnumerable<(Type, object)> GetServices()
        {
            FieldInfo[] fields = GetType().GetFields
           (
               BindingFlags.Instance |
               BindingFlags.NonPublic
               | BindingFlags.Public
               | BindingFlags.DeclaredOnly
           );

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute<ServiceAttribute>();

                if (attribute == null)
                {
                    var service = field.GetValue(this);
                    var type = service.GetType();
                    yield return (type, service);
                }

                if (attribute != null)
                {
                    var type = attribute.Contract;
                    var service = field.GetValue(this);
                    yield return (type, service);
                }
            }
        }
    }
}
