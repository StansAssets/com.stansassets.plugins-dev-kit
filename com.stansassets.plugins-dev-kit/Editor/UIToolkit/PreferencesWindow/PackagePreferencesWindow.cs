#if UNITY_2019_4_OR_NEWER

using System;
using System.Collections.Generic;
using System.Linq;
using StansAssets.Foundation.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    /// <summary>
    /// Base class for Plugin Preferences Window
    /// </summary>
    public abstract class PackagePreferencesWindow
    {
        TabController m_TabController;

        /// <summary>
        /// Structure describing a Unity Package.
        /// </summary>
        protected abstract UnityEditor.PackageManager.PackageInfo GetPackageInfo();

        /// <summary>
        /// Gets or sets the display name of the SettingsProvider as it appears in the Settings window.
        /// If not set, the Settings window uses last token of SettingsProvider.settingsPath instead.
        /// </summary>
        protected abstract string DisplayName { get; }

        /// <summary>
        /// Gets Path used to place the SettingsProvider in the tree view of the Settings window.
        /// The path should be unique among all other settings paths and should use "/" as its separator.
        /// </summary>
        protected abstract string SettingsPath { get; }

        /// <summary>
        /// Gets the Scope of the SettingsProvider. The Scope determines whether the SettingsProvider appears
        /// in the Preferences window (SettingsScope.User) or the Settings window (SettingsScope.Project).
        /// </summary>
        protected abstract SettingsScope Scope { get; }

        /// <summary>
        /// Add tab to the window top bar.
        /// </summary>
        /// <param name="label">Tab label.</param>
        /// <param name="content">Tab content.</param>
        /// <exception cref="ArgumentException">Will throw tab with the same label was already added.</exception>
        protected void AddTab(string label, VisualElement content)
        {
            m_TabController.AddTab(label, content);
        }

        /// <summary>
        /// Activate tab by name
        /// </summary>
        /// <param name="name">Early specified tab name</param>
        protected void ActivateTab(string name)
        {
            m_TabController.ActivateTab(name);
        }

        /// <summary>
        /// Set the flexible growth property of tabs content container
        /// </summary>
        /// <param name="styleFloat"></param>
        protected void ContentContainerFlexGrow(StyleFloat styleFloat)
        {
            m_TabController.ContentContainerFlexGrow(styleFloat);
        }

        /// <summary>
        /// Overrides SettingsProvider.OnActivate.
        /// </summary>
        protected abstract void OnActivate(string searchContext, VisualElement rootElement);

        /// <summary>
        /// Overrides SettingsProvider.OnDeactivate.
        /// </summary>
        protected abstract void OnDeactivate();
        
        void OnActivateWindow(string searchContext, VisualElement rootElement)
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

            rootElement.Q<Label>("display-name").text = DisplayName;

            var packageInfo = GetPackageInfo();
            rootElement.Q<Label>("description").text = packageInfo.description;
            rootElement.Q<Label>("version").text = $"Version: {packageInfo.version}";

            m_TabController = new TabController(rootElement);
        }

        SettingsProvider ConstructSettingsProvider()
        {
            var packageInfo = GetPackageInfo();
            var settingsProvider = new SettingsProvider(SettingsPath, Scope, packageInfo.keywords)
            {
                label = DisplayName,
            };

            settingsProvider.activateHandler += OnActivateWindow;
            settingsProvider.activateHandler += OnActivate;
            settingsProvider.deactivateHandler += OnDeactivate;

            return settingsProvider;
        }

        [SettingsProviderGroup]
        static SettingsProvider[] RegisterSettingsProviderGroup()
        {
            var baseType = typeof(PackagePreferencesWindow);
            var group = new List<SettingsProvider>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var derivedTypes = assembly.GetTypes()
                    .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                    .ToArray();

                foreach (var derivedType in derivedTypes)
                {
                    var instance = (PackagePreferencesWindow)Activator.CreateInstance(derivedType);
                    var settingsProvider = instance.ConstructSettingsProvider();
                    group.Add(settingsProvider);
                }
            }

            return group.ToArray();
        }
    }
}

#endif