using StansAssets.Foundation.Editor;
using UnityEditor.PackageManager;

namespace StansAssets.Plugins.Editor
{
    static class PluginsDevKitPackage
    {
        public const string Name = "com.stansassets.plugins-dev-kit";
        public static readonly string RootPath = PackageManagerUtility.GetPackageRootPath(Name);
        public static readonly string UIToolkitPath = $"{RootPath}/Editor/UIToolkit";
        public static readonly string UIToolkitControlsPath = $"{UIToolkitPath}/Controls";

#if UNITY_2019_4_OR_NEWER || UNITY_2020_2_OR_NEWER
        /// <summary>
        ///  Foundation package info.
        /// </summary>
        public static PackageInfo GetPackageInfo()
        {
            return PackageManagerUtility.GetPackageInfo(Name);
        }
#endif
    }
}
