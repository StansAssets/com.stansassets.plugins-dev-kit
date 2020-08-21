using System.Windows.Markup;
using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    public class AboutTab : BaseTab
    {
        public AboutTab()
            : base($"{PluginsDevKitPackage.UIToolkitPath}/AboutTab/AboutTab")
        {
            SetButtonHyperlink("GamesElement", "https://stansassets.com/#portfolio");
            SetButtonHyperlink("PluginsElement", "https://assetstore.unity.com/publishers/2256");
            SetButtonHyperlink("TeamElement", "https://stansassets.com/#our-team");

            SetButtonHyperlink("supportEmail", "mailto:support@stansassets.com");
            SetButtonHyperlink("ceoEmail", "mailto:ceo@stansassets.com");

            SetButtonHyperlink("LinkedinElement", "https://www.linkedin.com/in/lacost");
            SetButtonHyperlink("TwitterElement", "https://twitter.com/stansassets");
            SetButtonHyperlink("FacebookElement", "https://www.facebook.com/stansassets/");

            SetButtonHyperlink("YoutubeElement", "https://www.youtube.com/user/stansassets/videos");
            SetButtonHyperlink("GooglePlusElement", "https://plus.google.com/+StansassetsOfficia");
            SetButtonHyperlink("TwitchElement", "https://www.twitch.tv/stans_assets");

            SetButtonHyperlink("LogoElement", "https://stansassets.com/");
        }

        void SetButtonHyperlink(string btnName, string url)
        {
            var btn = this.Q<Button>(btnName);
            btn.clicked += () =>
                Application.OpenURL(url);
        }
    }
}
