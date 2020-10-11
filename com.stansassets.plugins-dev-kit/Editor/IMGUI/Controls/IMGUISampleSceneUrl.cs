using System;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace StansAssets.Plugins.Editor
{
    [Serializable]
    public class IMGUISampleSceneUrl : IMGUIHyperLabel
    {
        [SerializeField]
        string m_ScenePath;

        public IMGUISampleSceneUrl(string title, string scenePath)
            : base(new GUIContent(
                    title,
                    PluginsEditorSkin.GetGenericIcon("list_arrow_white.png") //TODO unity scene Icon
                ),
                SettingsWindowStyles.DescriptionLabelStyle)
        {
            m_ScenePath = scenePath;
            SetMouseOverColor(SettingsWindowStyles.SelectedElementColor);
        }

        public override bool Draw(params GUILayoutOption[] options)
        {
            var click = base.Draw(options);
            if (click)
            {
                var userFinishedOperation = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                if (userFinishedOperation)
                {
                    EditorSceneManager.OpenScene(m_ScenePath);
                }
            }

            return click;
        }
    }
}
