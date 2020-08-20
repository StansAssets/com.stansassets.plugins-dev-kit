using UnityEngine;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    public class AboutTab : BaseTab
    {
        public AboutTab()
            : base($"{PluginsDevKitPackage.UIToolkitPath}/AboutTab/AboutTab")
        {
           // this.Q<Button>("GamesButton").clicked += () => { Application.OpenURL("https://stansassets.com/#portfolio"); };
        }
    }
}
