using StansAssets.Foundation;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Plugins.Editor
{
    public static class SettingsWindowStyles
    {
        static GUIStyle s_SeparationStyle;

        public static GUIStyle SeparationStyle
        {
            get
            {
                if (s_SeparationStyle == null)
                    s_SeparationStyle = new GUIStyle();

                if (s_SeparationStyle.normal.background == null)
                {
                    s_SeparationStyle.normal.background = Texture2DUtility.MakePlainColorImage(EditorGUIUtility.isProSkin ? "#292929FF" : "#A2A2A2FF");
                }

                return s_SeparationStyle;
            }
        }

        static GUIStyle s_ServiceBlockHeader;

        public static GUIStyle ServiceBlockHeader =>
            s_ServiceBlockHeader ?? (s_ServiceBlockHeader = new GUIStyle
            {
                fontSize = 13,
                fontStyle = FontStyle.Bold,
                normal = { textColor = DisabledImageColor }
            });

        public static Color ProDisabledImageColor => ColorHelper.MakeColorFromHtml("#999999ED");
        public static Color DisabledImageColor => EditorGUIUtility.isProSkin ? ProDisabledImageColor : ColorHelper.MakeColorFromHtml("#3C3C3CFF");

        static GUIStyle s_MiniLabel;
        public static GUIStyle MiniLabel => s_MiniLabel ?? (s_MiniLabel = new GUIStyle(EditorStyles.miniLabel) { wordWrap = true });
    }
}
