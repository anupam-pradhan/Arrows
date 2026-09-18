using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace ArrowNook.Editor
{
    public sealed class ArrowNookReleaseGuard : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android && report.summary.platform != BuildTarget.iOS) return;
            if ((report.summary.options & BuildOptions.Development) != 0) return;
            throw new BuildFailedException("ArrowNook currently uses official test ads and a development privacy notice. " +
                "Use a Development Build for testing. Complete docs/ads-and-release.md before enabling store builds.");
        }
    }
}
