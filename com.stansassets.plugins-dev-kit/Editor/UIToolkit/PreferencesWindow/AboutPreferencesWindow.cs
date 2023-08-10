#if UNITY_2019_4_OR_NEWER

using JetBrains.Annotations;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Plugins.Editor
{
    [UsedImplicitly]
    sealed class AboutPreferencesWindow : PackagePreferencesWindow
    {
        protected override PackageInfo GetPackageInfo()
            => PackageManagerUtility.GetPackageInfo(PluginsDevKitPackage.Name);

        protected override string SettingsPath => $"{PluginsDevKitPackage.RootMenu}/{GetPackageInfo().displayName}";
        protected override SettingsScope Scope => SettingsScope.User;

        protected override void OnActivate(string searchContext, VisualElement rootElement)
        {
            ContentContainerFlexGrow(1);
            AddTab("About", new AboutTab());
        }

        protected override void OnDeactivate() { }
    }
}

#endif