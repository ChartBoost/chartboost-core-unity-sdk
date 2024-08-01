#if UNITY_IOS
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Chartboost.Core.Editor
{
    public class CorePostprocessor : IPostprocessBuildWithReport
    {
        private const string AppTrackingTransparencyFramework = "AppTrackingTransparency.framework";
        private const string BitcodeKey = "ENABLE_BITCODE";
        private const string BitcodeValue = "NO";
        private const string InfoPlist = "Info.plist";
        private const string TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";
        private const string NSUserTrackingUsageDescription = "NSUserTrackingUsageDescription";
        
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
                pbxProject.AddFrameworkToProject(frameworkGUI, AppTrackingTransparencyFramework, false);
                return true;
            }
            
            static bool DisableBitcode(PBXProject pbxProject)
            {
                pbxProject.SetBuildProperty(pbxProject.ProjectGuid(), BitcodeKey, BitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.GetUnityMainTargetGuid(), BitcodeKey, BitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.TargetGuidByName(PBXProject.GetUnityTestTargetName()), BitcodeKey, BitcodeValue);
                pbxProject.SetBuildProperty(pbxProject.GetUnityFrameworkTargetGuid(), BitcodeKey, BitcodeValue);
                return true;
            }
            #endif
        }
        
        private static void PListModifications(string pathToBuiltProject)
        {
            #if UNITY_IOS
            var plistPath = Path.Combine(pathToBuiltProject, InfoPlist);
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
                
                plistRoot.SetString(NSUserTrackingUsageDescription, TrackingDescription);
                return true;
            }
            #endif
        }
    }
}
#endif
