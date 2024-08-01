using Chartboost.Editor;
using NUnit.Framework;

namespace Chartboost.Core.Tests.Editor
{
    public class VersionValidator
    {
        private const string UnityPackageManagerPackageName = "com.chartboost.core";
        private const string NuGetPackageName = "Chartboost.CSharp.Core.Unity";
        
        [Test]
        public void ValidateVersion() 
            => VersionCheck.ValidateVersions(UnityPackageManagerPackageName, NuGetPackageName);
    }
}
