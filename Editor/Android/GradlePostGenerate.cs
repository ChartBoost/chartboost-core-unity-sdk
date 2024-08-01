using System.IO;
using UnityEditor.Android;

namespace Chartboost.Core.Editor.Android
{
    public class GradlePostGenerate : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder => 0;

        private const string ExcludeConfiguration = "apply from: 'ChartboostCore.androidlib/exclude_configuration.gradle'";
        private const string UnityLibraryGradle = "unityLibrary/build.gradle";

        public void OnPostGenerateGradleAndroidProject(string path)
        {
            var rootDirectoryInfo = new DirectoryInfo(path);
            if (rootDirectoryInfo.Parent == null) 
                return;
            var rootPath = rootDirectoryInfo.Parent.FullName;
            var unityLibraryGradle = Directory.GetFiles(rootPath, UnityLibraryGradle, SearchOption.TopDirectoryOnly);

            foreach (var gradlePath in unityLibraryGradle)
            {
                var contents = File.ReadAllText(gradlePath);
                if (contents.Contains(ExcludeConfiguration)) 
                    continue;
                
                contents += System.Environment.NewLine + ExcludeConfiguration;
                File.WriteAllText(gradlePath, contents);
            }
        }
    }
}
