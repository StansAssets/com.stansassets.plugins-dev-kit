#if UNITY_2019_4_OR_NEWER

using System.Collections.Generic;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEngine.UIElements;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace StansAssets.Plugins.Editor
{
    sealed class AboutPreferencesWindow : PackagePreferencesWindow
    {
        AboutPreferencesWindow(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        protected override PackageInfo GetPackageInfo()
        {
            return PackageManagerUtility.GetPackageInfo(PluginsDevKitPackage.Name);
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            TabController.ContentContainerFlexGrow(1);
            TabController.AddTab("about", "About", new AboutTab());
            TabController.ActivateTab("about");
        }

        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider()
        {
            var provider = new AboutPreferencesWindow(
                $"{PluginsDevKitPackage.RootMenu}/Plugin Dev Kit",
                SettingsScope.User,
                new[] { "stan", "plugin", "dev", "kit" })
            {
                label = "About"
            };

            return provider;
        }
    }
}

#endif