using System;
using UnityEngine.Networking;

namespace StansAssets.Plugins.Editor
{
    public static class GitHubUtility
    {
        public static void GetLatestRelease (string url, Action<GitHubRelease> callback)
        {
            var release = new GitHubRelease ();
            var rq = UnityWebRequest.Get (GetReleaseInfoURL (url));
            rq.SendWebRequest ().completed += obj => {
                release.ReadJson (rq.downloadHandler.text);
                callback (release);
            };
        }

        public static string GetReleaseInfoURL (string repositoryURL)
        {
            if (repositoryURL.Contains ("github.com")) {
                return repositoryURL.Replace (@".git", @"/releases/latest")
                                    .Replace (@"ssh://git@github.com:", @"https://api.github.com/repos/");
            }
            else if (repositoryURL.Contains ("npmjs.org")) {
                throw new NotImplementedException ();
            }
            
            throw new NotImplementedException ();
        }
    }
}
