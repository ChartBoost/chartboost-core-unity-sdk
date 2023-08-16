using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;

namespace Editor
{
    public class Postprocessor : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPostprocessBuild(BuildReport report)
        {
            var buildTarget = report.summary.platform;
            var pathToBuiltProject = report.summary.outputPath;

            if (buildTarget != BuildTarget.iOS)
                return;
            
            PBXProjectModifications(pathToBuiltProject);
            PListModifications(pathToBuiltProject);
        }
    
        private static void PBXProjectModifications(string pathToBuiltProject)
        {
            #if UNITY_IOS
            var pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            var pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxProjectPath);

            var mods = new HashSet<bool>
            {
                AddAppTrackingTransparencyFramework(pbxProject),
                DisableBitcode(pbxProject)
            };

            if (mods.Any(x => x))
                pbxProject.WriteToFile(pbxProjectPath);

            static bool AddAppTrackingTransparencyFramework(PBXProject pbxProject)
            {
                if (pbxProject == null)
                    return false;

                var frameworkGUI = pbxProject.GetUnityFrameworkTargetGuid();
                pbxProject.AddFrameworkToProject(frameworkGUI, "AppTrackingTransparency.framework", false);
                return true;
            }
            
            static bool DisableBitcode(PBXProject pbxProject)
            {

                const string bitcodeKey = "ENABLE_BITCODE";
                const string bitcodeValue = "NO";

                pbxProject.SetBuildProperty(pbxProject.ProjectGuid(), bitcodeKey, bitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.GetUnityMainTargetGuid(), bitcodeKey, bitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.TargetGuidByName(PBXProject.GetUnityTestTargetName()), bitcodeKey, bitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.GetUnityFrameworkTargetGuid(), bitcodeKey, bitcodeValue);
                return true;
            }
            #endif
        }
        
        private static void PListModifications(string pathToBuiltProject)
        {
            #if UNITY_IOS
            var plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            var mods = new HashSet<bool>
            {
                AddNSUserTrackingUsageDescription(plist)
            };

            if (mods.Any(x => x))
                plist.WriteToFile(plistPath);

            static bool AddNSUserTrackingUsageDescription(PlistDocument plist)
            {
                if (plist == null)
                    return false;

                var plistRoot = plist.root;
                const string TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";
                plistRoot.SetString("NSUserTrackingUsageDescription", TrackingDescription);
                return true;
            }
            #endif
        }
    }
}
