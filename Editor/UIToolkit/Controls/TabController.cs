#if UNITY_2019_4_OR_NEWER

using System;
using System.Collections.Generic;
using System.Linq;
using StansAssets.Foundation.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    /// <summary>
    /// Tab controller based on <see cref="ButtonStrip"/> to switch between tabs
    /// and <see cref="ScrollView"/> to display their contents
    /// </summary>
    public class TabController
    {
        readonly Dictionary<string, VisualElement> m_Tabs = new Dictionary<string, VisualElement>();

        readonly ButtonStrip m_TabsButtons;
        readonly ScrollView m_TabsContainer;

        /// <summary>
        /// Available tabs' labels.
        /// </summary>
        public IEnumerable<string> Tabs => m_Tabs.Keys;

        /// <summary>
        /// Active tab label from <see cref="Tabs"/>.
        /// </summary>
        public string ActiveTab => m_TabsButtons.Value;
        
        /// <summary>
        /// This constructor will looking for already existing elements:
        /// <see cref="ButtonStrip"/> (without name) and <see cref="ScrollView"/> named "tabs-container"
        /// The purpose of this is to support <see cref="PackageSettingsWindow{TWindow}"/>.
        /// </summary>
        /// <param name="root">Element that contains <see cref="ButtonStrip"/> and <see cref="ScrollView"/> named tabs-container</param>
        public TabController(VisualElement root)
        {
            m_TabsButtons = root.Q<ButtonStrip>();
            m_TabsContainer = root.Q<ScrollView>("tabs-container");

            Init();
        }
        
        /// <summary>
        /// Add tab to the window top bar.
        /// </summary>
        /// <param name="label">Tab label.</param>
        /// <param name="content">Tab content.</param>
        /// <exception cref="ArgumentException">Will throw tab with the same label was already added.</exception>
        public void AddTab(string label, VisualElement content)
        {
            if (!m_Tabs.ContainsKey(label))
            {
                m_TabsButtons.AddChoice(label, label);
                m_Tabs.Add(label, content);
                content.viewDataKey = label;
            }
            else
            {
                throw new ArgumentException($"Tab '{label}' already added", nameof(label));
            }
        }

        /// <summary>
        /// Activate tab by label
        /// </summary>
        /// <param name="label">Early specified tab label</param>
        public void ActivateTab(string label)
        {
            if (!m_Tabs.ContainsKey(label))
            {
                return;
            }

            m_TabsButtons.SetValue(label);
        }

        /// <summary>
        /// Set the flexible growth property of tabs content container
        /// </summary>
        /// <param name="styleFloat"></param>
        public void ContentContainerFlexGrow(StyleFloat styleFloat)
        {
            m_TabsContainer.contentContainer.style.flexGrow = styleFloat;
        }

        /// <summary>
        /// Container flex grow property.
        /// </summary>
        public StyleFloat ContainerFlexGrow => m_TabsContainer.contentContainer.style.flexGrow;

        /// <summary>
        /// Refresh current tab
        /// </summary>
        public void RefreshActiveTab()
        {
            if (string.IsNullOrEmpty(ActiveTab))
            {
                return;
            }

            foreach (var tab in m_Tabs)
            {
                tab.Value.RemoveFromHierarchy();
            }

            var element = m_Tabs.First(i => i.Key.Equals(m_TabsButtons.Value)).Value;
            m_TabsContainer.Add(element);
        }

        void Init()
        {
            Assert.IsNotNull(m_TabsButtons);
            Assert.IsNotNull(m_TabsContainer);

            m_TabsButtons.CleanUp();
            m_TabsButtons.OnButtonClick += RefreshActiveTab;

            RefreshActiveTab();
        }
    }
}

#endif