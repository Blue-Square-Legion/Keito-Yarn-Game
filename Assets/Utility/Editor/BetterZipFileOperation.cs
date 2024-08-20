/// Adapted from https://github.com/cosformula/buildactions/blob/3864e2bb709a5ff154c4c513c4d17abaf22f215b/Editor/ZipFile/ZipFileOperation.cs
/// which is under the MIT License, see https://github.com/cosformula/buildactions/blob/3864e2bb709a5ff154c4c513c4d17abaf22f215b/LICENSE.md
using SuperUnityBuild.BuildTool;
using System;
using System.IO;
using UnityEngine;
using System.IO.Compression;

namespace Keito.BuildActions
{
    public class BetterZipFileOperation : BuildAction, IPreBuildAction, IPreBuildPerPlatformAction, IPostBuildAction, IPostBuildPerPlatformAction
    {
        public string inputPath = "$BUILDPATH";
        public string outputPath = "$BUILDPATH";
        public string outputFileName = "$PRODUCT_NAME-$RELEASE_TYPE-$YEAR_$MONTH_$DAY.zip";

        public override void PerBuildExecute(BuildReleaseType releaseType, BuildPlatform platform, BuildArchitecture architecture, BuildScriptingBackend scriptingBackend, BuildDistribution distribution, DateTime buildTime, ref UnityEditor.BuildOptions options, string configKey, string buildPath)
        {
            string combinedOutputPath = Path.Combine(outputPath, outputFileName);
            string resolvedOutputPath = BuildAction.ResolvePerBuildExecuteTokens(combinedOutputPath, releaseType, platform, architecture, scriptingBackend, distribution, buildTime, buildPath);

            string resolvedInputPath = BuildAction.ResolvePerBuildExecuteTokens(inputPath, releaseType, platform, architecture, scriptingBackend, distribution, buildTime, buildPath);

            if (!resolvedOutputPath.EndsWith(".zip"))
            {
                resolvedOutputPath += ".zip";
            }

            PerformZip(Path.GetFullPath(resolvedInputPath), Path.GetFullPath(resolvedOutputPath), platform);
        }

        private void PerformZip(string inputPath, string outputPath, BuildPlatform platform)
        {
            try
            {
                if (!Directory.Exists(inputPath))
                {
                    BuildNotificationList.instance.AddNotification(new BuildNotification(
                        BuildNotification.Category.Error,
                        "Zip Operation Failed.", string.Format("Input path does not exist: {0}", inputPath),
                        true, null));
                    return;
                }

                // Make sure that all parent directories in path are already created.
                string parentPath = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(parentPath))
                {
                    Directory.CreateDirectory(parentPath);
                }

                // Delete old file if it exists.
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }
                System.IO.Compression.ZipFile.CreateFromDirectory(inputPath, outputPath);
                // MacOS specific to keep permissions after zip 
                if (platform.platformName == "macOS")
                {
                    // Re-open zip for updating
                    using (var zipArchive = System.IO.Compression.ZipFile.Open(outputPath, ZipArchiveMode.Update))
                    {
                        foreach (var entry in zipArchive.Entries)
                        {
                            // Find MacOS executable and set it to be executable
                            if (entry.FullName.Contains("Contents/MacOS"))
                            {
                                entry.ExternalAttributes = 0755;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
        }
    }
}