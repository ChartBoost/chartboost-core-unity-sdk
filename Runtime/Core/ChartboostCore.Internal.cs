using System;
using System.Collections.Generic;
using System.Linq;

namespace Chartboost.Core
{
    public abstract partial class ChartboostCore 
    {
        private static ChartboostCore _instance;

        private static ChartboostCore GetInstance()
        {
            if (_instance != null)
                return _instance;
            
            var availableSDKs = FindAllAssignableTypes<ChartboostCore>("com.chartboost.core");
            
            if (Activator.CreateInstance(availableSDKs.First()) is ChartboostCore sdk) {
                _instance = sdk;
                ChartboostCoreLogger.Log($" Instance set to platform SDK {sdk.GetType()}.");
            }
            else
                ChartboostCoreLogger.LogError("Could not find an implementation of ChartboostCore SDK to use!");

            return _instance;
        }
        private static IEnumerable<Type> FindAllAssignableTypes<T>(string assemblyFilter) {
            var assignableType = typeof(T);
        
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var filteredAssemblies = assemblies.Where(assembly 
                => assembly.FullName.Contains(assemblyFilter));
        
            var allTypes = filteredAssemblies.SelectMany(assembly => assembly.GetTypes());
            var assignableTypes = allTypes.Where(type 
                => type != assignableType && assignableType.IsAssignableFrom(type));

            return assignableTypes;
        }
    }
}
