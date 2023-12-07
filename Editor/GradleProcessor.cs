using System.IO;
using UnityEditor.Android;

namespace Chartboost.Core.Editor
{
    public class GradleProcessor : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 0;

        private const string EXCLUDE_CONFIGURATION = "apply from: 'ChartboostCore.androidlib/exclude_configuration.gradle'";
        private const string UNITY_LIBRARY_GRADLE = "unityLibrary/build.gradle";

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            var rootDirectoryInfo = new DirectoryInfo(path);
            if (rootDirectoryInfo.Parent == null) 
                return;
            var rootPath = rootDirectoryInfo.Parent.FullName;
            var unityLibraryGradle = Directory.GetFiles(rootPath, UNITY_LIBRARY_GRADLE,
                SearchOption.TopDirectoryOnly);

            foreach (var gradlePath in unityLibraryGradle)
            {
                var contents = File.ReadAllText(gradlePath);
                if (contents.Contains(EXCLUDE_CONFIGURATION)) 
                    continue;
                
                contents += System.Environment.NewLine + EXCLUDE_CONFIGURATION;
                File.WriteAllText(gradlePath, contents);
            }
        }
    }
}
