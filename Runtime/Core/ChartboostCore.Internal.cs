using System;
using System.Collections.Generic;
using System.Linq;
using Chartboost.Core.Default;
using UnityEngine;

namespace Chartboost.Core
{
    /// <summary>
    /// Internal Developer methods for Core SDK
    /// </summary>
    public abstract partial class ChartboostCore 
    {
        private static ChartboostCore _instance;

        /// <summary>
        /// Find Core SDK Instance based on Assembly Definition files availability.
        /// </summary>
        /// <returns></returns>
        private static ChartboostCore FindInstance()
        {
            if (_instance != null)
                return _instance;
            
            var availableSDKs = FindAllAssignableTypes<ChartboostCore>("com.chartboost.core");
            if (!Application.isEditor && availableSDKs != null && Activator.CreateInstance(availableSDKs.First()) is ChartboostCore sdk) {
                _instance = sdk;
                ChartboostCoreLogger.Log($"Instance set to platform SDK {sdk.GetType()}.");
            }
            else
            {
                _instance = new ChartboostCoreDefault();
                ChartboostCoreLogger.LogError("Could not find an implementation of ChartboostCore SDK to use!");
            }
            return _instance;
        }
        
        /// <summary>
        /// Find all assignable types based on an assembly filter and common Type.
        /// </summary>
        /// <param name="assemblyFilter">Desired assembly filter, e.g "chartboost.core".</param>
        /// <typeparam name="T">Common type.</typeparam>
        /// <returns>All assignable types found.</returns>
        private static IEnumerable<Type> FindAllAssignableTypes<T>(string assemblyFilter) {
            var assignableType = typeof(T);
        
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var filteredAssemblies = assemblies.Where(assembly 
                => assembly.FullName.Contains(assemblyFilter));
        
            var allTypes = filteredAssemblies.SelectMany(assembly => assembly.GetTypes());
            var assignableTypes = allTypes.Where(type 
                => type != assignableType && assignableType.IsAssignableFrom(type) && type.FullName != null && !type.FullName.Contains("Default"));

            return assignableTypes;
        }
    }
}
