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
        readonly Dictionary<string, (string label, VisualElement element)> m_Tabs =
            new Dictionary<string, (string label, VisualElement element)>();

        readonly ButtonStrip m_TabsButtons;
        readonly ScrollView m_TabsContainer;

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
        /// Use to define custom tabbed menu
        /// </summary>
        /// <param name="tabsButtons"><see cref="ButtonStrip"/> buttons used to switch between tabs</param>
        /// <param name="tabsContainer"><see cref="ScrollView"/> the container that will display the contents of the tabs</param>
        public TabController(ButtonStrip tabsButtons, ScrollView tabsContainer)
        {
            m_TabsButtons = tabsButtons;
            m_TabsContainer = tabsContainer;

            Init();
        }

        /// <summary>
        /// Add tab to the window top bar.
        /// </summary>
        /// <param name="name">Tab name</param>
        /// <param name="label">Tab label.</param>
        /// <param name="content">Tab content.</param>
        /// <exception cref="ArgumentException">Will throw tab with the same label was already added.</exception>
        public void AddTab(string name, string label, VisualElement content)
        {
            if (!m_Tabs.ContainsKey(label))
            {
                m_TabsButtons.AddChoice(label, label);
                m_Tabs.Add(name, (label, content));
                content.viewDataKey = label;
            }
            else
            {
                throw new ArgumentException($"Tab '{label}'[{name}] already added", nameof(label));
            }
        }

        /// <summary>
        /// Activate tab by name
        /// </summary>
        /// <param name="name">Early specified tab name</param>
        public void ActivateTab(string name)
        {
            if (!m_Tabs.ContainsKey(name))
            {
                return;
            }

            var tab = m_Tabs[name];
            m_TabsButtons.SetValue(tab.label);
        }

        /// <summary>
        /// Set the flexible growth property of tabs content container
        /// </summary>
        /// <param name="styleFloat"></param>
        public void ContentContainerFlexGrow(StyleFloat styleFloat)
        {
            m_TabsContainer.contentContainer.style.flexGrow = styleFloat;
        }

        void Init()
        {
            Assert.IsNotNull(m_TabsButtons);
            Assert.IsNotNull(m_TabsContainer);

            m_TabsButtons.CleanUp();
            m_TabsButtons.OnButtonClick += ActivateTab;

            ActivateTab();
        }

        void ActivateTab()
        {
            if (string.IsNullOrEmpty(m_TabsButtons.Value))
            {
                return;
            }

            foreach (var tab in m_Tabs)
            {
                tab.Value.element.RemoveFromHierarchy();
            }

            if (!m_Tabs.Any())
            {
                return;
            }

            var (_, element) = m_Tabs.First(i => i.Value.label.Equals(m_TabsButtons.Value)).Value;
            m_TabsContainer.Add(element);
        }
    }
}

#endif