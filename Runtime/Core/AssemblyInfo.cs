using System.Runtime.CompilerServices;
using Chartboost.Core;

[assembly: InternalsVisibleTo(AssemblyInfo.CoreAssemblyAndroid)]
[assembly: InternalsVisibleTo(AssemblyInfo.CoreAssemblyIOS)]
[assembly: InternalsVisibleTo(AssemblyInfo.UnmanagedAdapterAssemblyIOS)]

namespace Chartboost.Core
{
    internal class AssemblyInfo
    {
        public const string CoreAssemblyAndroid = "Chartboost.Core.Android";
        public const string CoreAssemblyIOS = "Chartboost.Core.IOS";
        public const string UnmanagedAdapterAssemblyIOS = "Chartboost.Core.Consent.Unmanaged.IOS";
    }
}
