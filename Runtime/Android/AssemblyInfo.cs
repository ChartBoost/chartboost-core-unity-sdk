using System.Runtime.CompilerServices;
using Chartboost.Core.Android;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
[assembly: InternalsVisibleTo(AssemblyInfo.UnmanagedAdapterAssemblyAndroid)]

namespace Chartboost.Core.Android
{
    internal class AssemblyInfo
    {
        public const string UnmanagedAdapterAssemblyAndroid = "Chartboost.Core.Consent.Unmanaged.Android";
    }
}
