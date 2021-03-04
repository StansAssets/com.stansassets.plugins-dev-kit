using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace StansAssets.Plugins
{
    public abstract class LocalProjectSettingsSingleton<T> : LocalProjectSettings where T : LocalProjectSettings, new()
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
                    var inst = new T();
                    s_Instance = InitFromCache(inst);

                    if (s_Instance == null)
                    {
                        s_Instance = inst;
                        Save(inst);
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
            Save(Instance);
#endif
        }

        /// <summary>
        ///     // Only applicable while in the editor.
        /// </summary>
        static void Save(T asset)
        {
            // Only applicable while in the editor.
#if UNITY_EDITOR
            var path = Path.Combine(asset.SettingsFilePath);
            var directory = Path.GetDirectoryName(path);

            if (directory == null)
                throw new InvalidOperationException($"Failed to get directory from package settings path: {path}");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            CacheDocument(asset);
#endif
        }

        static T InitFromCache(T asset)
        {
            if (File.Exists(asset.SettingsFilePath))
            {
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(asset.SettingsFilePath);
                    var text = reader.ReadToEnd();
                    return JsonUtility.FromJson<T>(text);
                }
                finally
                {
                    reader?.Close();
                }
            }

            return null;
        }

        static Task CacheDocument(T asset)
        {
            var path = asset.SettingsFilePath;
            return Task.Run(() => {
                try
                {
                    File.WriteAllText(path, JsonUtility.ToJson(asset));
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                finally
                {
                    Debug.Log($"Personal project settings saved: {path}");
                }
            });
        }
    }
}