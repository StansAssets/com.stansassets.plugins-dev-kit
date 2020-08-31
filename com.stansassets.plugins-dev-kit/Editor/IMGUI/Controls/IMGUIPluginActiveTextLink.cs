using System;
using UnityEngine;

namespace StansAssets.Plugins.Editor
{
    [Serializable]
    public class IMGUIPluginActiveTextLink : IMGUIHyperLabel
    {
        public IMGUIPluginActiveTextLink(string title)
            : base(new GUIContent(title), SettingsWindowStyles.DescriptionLabelStyle)
        {
            SetColor(SettingsWindowStyles.ActiveLinkColor);
            SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
        }
    }
}
