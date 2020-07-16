using JetBrains.Annotations;
using StansAssets.Foundation.Editor;
using UnityEngine.UIElements;

namespace StansAssets.Plugins.Editor
{
    public class SettingsBlock : BindableElement
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<SettingsBlock, UxmlTraits> { }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription { name = "label" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((SettingsBlock)ve).Label = m_Label.GetValueFromBag(bag, cc);
            }
        }

        public const string USSClassName = "stansassets-settings-block";
        public const string HeaderUssClassName = USSClassName + "__header";
        public const string ContentUssClassName = USSClassName + "__content";

        readonly Label m_Label;
        readonly VisualElement m_Container;

        public override VisualElement contentContainer => m_Container;

        public string Label
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        public SettingsBlock()
        {
            AddToClassList(USSClassName);
            var header = new VisualElement()
            {
                name = "header",
            };
            header.AddToClassList(HeaderUssClassName);
            hierarchy.Add(header);

            m_Label = new Label();
            header.Add(m_Label);

            m_Container = new VisualElement()
            {
                name = "content",
            };

            m_Container.AddToClassList(ContentUssClassName);
            hierarchy.Add(m_Container);

            UIToolkitEditorUtility.ApplyStyle(this, $"{PluginsDevKitPackage.UIToolkitControlsPath}/SettingsBlock/SettingsBlock");
        }
    }
}