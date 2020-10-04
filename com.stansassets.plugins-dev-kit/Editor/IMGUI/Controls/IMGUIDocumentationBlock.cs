using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StansAssets.Plugins.Editor
{
    [Serializable]
    public class IMGUIDocumentationBlock
    {
        [SerializeField]
        List<IMGUIDocumentationUrl> m_DocUrls = new List<IMGUIDocumentationUrl>();

        public void AddDocumentationUrl(string title, string url)
        {
            var feature = new IMGUIDocumentationUrl(title,url);
            m_DocUrls.Add(feature);
        }

        public void Draw()
        {
            using (new IMGUIBlockWithSpace(new GUIContent("Documentation")))
            {
                using (new IMGUIIndentLevel(1))
                {
                    for (var i = 0; i < m_DocUrls.Count; i += 2)
                    {
                        using (new IMGUIBeginHorizontal())
                        {
                            m_DocUrls[i].DrawLink(GUILayout.Width(150));
                            if (m_DocUrls.Count > i + 1) 
                                m_DocUrls[i + 1].DrawLink(GUILayout.Width(150));

                            GUILayout.FlexibleSpace();
                        }
                    }

                    EditorGUILayout.Space();
                }
            }
        }
    }
}
