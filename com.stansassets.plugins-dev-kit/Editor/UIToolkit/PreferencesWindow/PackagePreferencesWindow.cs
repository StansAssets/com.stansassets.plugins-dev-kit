#if UNITY_2019_4_OR_NEWER

using System.Collections.Generic;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    /// <summary>
    /// Base class for Plugin Preferences Window
    /// See <see cref="AboutPreferencesWindow"/> as an example for registering window in preferences (user, project)
    /// </summary>
    public abstract class PackagePreferencesWindow : SettingsProvider
    {
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            UIToolkitEditorUtility.CloneTreeAndApplyStyle(rootElement,
                $"{PluginsDevKitPackage.UIToolkitPath}/SettingsWindow/PackageSettingsWindow");

            // Hide search bar from PackageSettingsWindow. In preferences we already have search bar
            // and it's value in "searchContext" parameter
            var searchBar = rootElement.Q<ToolbarSearchField>();
            if (searchBar != null)
            {
                searchBar.style.visibility = Visibility.Hidden;
            }

            var packageInfo = GetPackageInfo();
            rootElement.Q<Label>("display-name").text = packageInfo.displayName.Remove(0, "Stans Assets - ".Length);
            rootElement.Q<Label>("description").text = packageInfo.description;
            rootElement.Q<Label>("version").text = $"Version: {packageInfo.version}";

            TabController = new TabController(rootElement);
        }

        protected TabController TabController { get; private set; }

        protected abstract UnityEditor.PackageManager.PackageInfo GetPackageInfo();

        protected PackagePreferencesWindow(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }
    }
}

#endif