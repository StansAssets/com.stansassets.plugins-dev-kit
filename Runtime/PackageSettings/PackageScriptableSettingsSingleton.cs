using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace StansAssets.Plugins
{
    /// <summary>
    ///     Package Scriptable Settings singleton pattern implementation.
    /// </summary>
    public abstract class PackageScriptableSettingsSingleton<T> : PackageScriptableSettings where T : PackageScriptableSettings
    {
        static T s_Instance;

        /// <summary>
        ///     Returns a singleton class instance
        ///     If current instance is not assigned it will try to find an object of the instance type,
        ///     in case instance already exists in a project. If not, new instance will be created,
        ///     and saved under a <see cref="PackagesConfig.SettingsPath" /> location
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = Resources.Load(typeof(T).Name) as T;

                    if (s_Instance == null)
                    {
                        s_Instance = CreateInstance<T>();
                        SaveToAssetDatabase(s_Instance);
                    }
                }

                return s_Instance;
            }
        }

        /// <summary>
        ///     Saves instance to an editor database.
        ///     Only applicable while in the editor.
        /// </summary>
        public static void Save()
        {
            // Only applicable while in the editor.
#if UNITY_EDITOR

            //TODO use Undo
            EditorUtility.SetDirty(Instance);
            AssetDatabase.SaveAssetIfDirty(Instance);
#endif
        }

        /// <summary>
        ///     // Only applicable while in the editor.
        /// </summary>
        static void SaveToAssetDatabase(T asset)
        {
            // Only applicable while in the editor.
#if UNITY_EDITOR
            var path = Path.Combine(asset.SettingsFilePath);
            var directory = Path.GetDirectoryName(path);

            if (directory == null)
                throw new InvalidOperationException($"Failed to get directory from package settings path: {path}");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            AssetDatabase.CreateAsset(asset, path);
#endif
        }
    }
}
