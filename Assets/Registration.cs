using System;
using System.Collections.Generic;
using Assets.Interfaces;
using Assets.Services;
using HoloToolkit.Unity;
using UnityEngine;

namespace Assets
{
    public class Registration : Singleton<Registration>
    {
        private static readonly IDictionary<string, string> RegisteredTypes = new Dictionary<string, string>();


        protected override void Awake()
        {
            base.Awake();

            RegisterTypes();
        }

        private void RegisterTypes()
        {
            RegisterType<IHttpRequestService, HttpRequestService>();
        }

        private void RegisterType<TDestination, TSource>() where TSource : TDestination, new()
        {
            var destinationTypeName = typeof(TDestination).FullName;
            var sourceTypeName = typeof(TSource).FullName;
            if (RegisteredTypes.ContainsKey(destinationTypeName))
            {
                RegisteredTypes[typeof(TDestination).FullName] = typeof(TDestination).FullName;
            }
            else
            {
                RegisteredTypes.Add(destinationTypeName, sourceTypeName);
            }
        }

        public T Resolve<T>()
        {
            var destinationTypeName = typeof(T).FullName;
            if (!RegisteredTypes.ContainsKey(destinationTypeName))
            {
                throw new Exception(string.Format("Type not registered: {0}", destinationTypeName));
            }

            var sourceTypeName = RegisteredTypes[destinationTypeName];
            
            // ReSharper disable once AssignNullToNotNullAttribute
            var instance = Activator.CreateInstance(Type.GetType(sourceTypeName));
            
            // ReSharper disable once AssignNullToNotNullAttribute
            return (T)instance;
        }
    }
}
