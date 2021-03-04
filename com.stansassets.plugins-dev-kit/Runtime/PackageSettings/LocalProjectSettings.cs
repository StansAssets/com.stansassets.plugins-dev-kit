using System;
using UnityEngine;

namespace StansAssets.Plugins
{
    [Serializable]
    public class LocalProjectSettings
    {
        public LocalProjectSettings()
        {
            SettingsFileName = GetType().Name;
            PackageName = "newPackage";
        }
        
        public LocalProjectSettings(string packageName, string settingsFileName)
        {
            SettingsFileName = settingsFileName;
            PackageName = packageName;
        }

        /// <summary>
        /// Plugin package name.
        /// </summary>
        public string PackageName { get; }

        /// <summary>
        /// Plugin settings folder path.
        /// </summary>
        public string SettingsFolderPath => $"{Application.dataPath}/{PackagesConfig.LibraryPath}/{PackageName}";

        /// <summary>
        /// Plugin settings file path.
        /// </summary>
        public string SettingsFilePath => $"{SettingsFolderPath}/{SettingsFileName}.json";

        /// <summary>
        /// Settings file name.
        /// </summary>
        public string SettingsFileName { get; }
    }
}