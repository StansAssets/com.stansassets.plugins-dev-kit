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
        /// Gets Path used to place the SettingsProvider in the tree view of the Preferences or Project Settings window.
        /// The path should be unique among all other settings paths and should use "/" as its separator.
        /// </summary>
        protected abstract string SettingsPath { get; }

        /// <summary>
        /// Gets the Scope of the SettingsProvider. The Scope determines whether the SettingsProvider appears
        /// in the Preferences window (SettingsScope.User) or the Settings window (SettingsScope.Project).
        /// </summary>
        protected abstract SettingsScope Scope { get; }

        /// <summary>
        /// Tab control element.
        /// </summary>
        protected TabController TabController => m_TabController;

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
            
            var packageInfo = GetPackageInfo();
            rootElement.Q<Label>("display-name").text = packageInfo.displayName;
            rootElement.Q<Label>("description").text = packageInfo.description;
            rootElement.Q<Label>("version").text = $"Version: {packageInfo.version}";

            m_TabController = new TabController(rootElement);
        }

        void OnActivateHandler(string searchContext, VisualElement rootElement)
        {
            OnActivate(searchContext, rootElement);

            EditorApplication.delayCall += () =>
            {
                m_TabController.RefreshActiveTab();
            };
        }

        SettingsProvider ConstructSettingsProvider()
        {
            var packageInfo = GetPackageInfo();
            var settingsProvider = new SettingsProvider(SettingsPath, Scope, packageInfo.keywords)
            {
                label = packageInfo.displayName,
            };

            settingsProvider.activateHandler += OnActivateWindow;
            settingsProvider.activateHandler += OnActivateHandler;
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
