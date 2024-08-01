using System.Runtime.CompilerServices;
using Chartboost.Core;

[assembly: InternalsVisibleTo(AssemblyInfo.CoreAssemblyAndroid)]
[assembly: InternalsVisibleTo(AssemblyInfo.CoreAssemblyIOS)]

namespace Chartboost.Core
{
    internal class AssemblyInfo
    {
        public const string CoreAssemblyAndroid = "Chartboost.Core.Android";
        public const string CoreAssemblyIOS = "Chartboost.Core.IOS";
    }
}
