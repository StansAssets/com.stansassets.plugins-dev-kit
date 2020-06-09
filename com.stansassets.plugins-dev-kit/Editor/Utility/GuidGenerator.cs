using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace StansAssets.Plugins.Editor
{
    public static class GuidGenerator
    {
        public static void  RegenerateGuid(string assetPath)
        {
            try
            {
                var path = $"{assetPath}.meta";
                var metafile = File.ReadAllText(path);
                var startGuid = metafile.IndexOf("guid:") + 6;
                var endGuid = metafile.Substring(startGuid).IndexOf("\n");
                var oldGuid = metafile.Substring(startGuid, endGuid);
                metafile = metafile.Replace(oldGuid, Guid.NewGuid().ToString("N"));
                File.WriteAllText(path, metafile);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public static void  RegenerateGuids(IEnumerable<string> assetPaths)
        {
            foreach (var assetPath in assetPaths)
            {
                RegenerateGuid(assetPath);
            }
            
        }
    }
}
