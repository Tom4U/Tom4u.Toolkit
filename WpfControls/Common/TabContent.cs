// TabContent.cs, version 1.2
// The code in this file is Copyright (c) Ivan Krivyakov
// See http://www.ikriv.com/legal.php for more information

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Tom4u.Toolkit.WpfControls.Strings;

namespace Tom4u.Toolkit.WpfControls.Common
{
    public static partial class TabContent
    {
        public static readonly DependencyProperty IsCachedProperty = DependencyProperty.RegisterAttached(
                "IsCached",
                typeof(bool),
                typeof(TabContent),
                new UIPropertyMetadata(false, OnIsCachedChanged));

        /// <summary>
        ///     Used instead of TabControl.ContentTemplate for cached tabs
        /// </summary>
        public static readonly DependencyProperty TemplateProperty = DependencyProperty.RegisterAttached(
                "Template",
                typeof(DataTemplate),
                typeof(TabContent),
                new UIPropertyMetadata(null));

        /// <summary>
        ///     Used instead of TabControl.ContentTemplateSelector for cached tabs
        /// </summary>
        public static readonly DependencyProperty TemplateSelectorProperty = DependencyProperty.RegisterAttached(
                "TemplateSelector",
                typeof(DataTemplateSelector),
                typeof(TabContent),
                new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for InternalTabControl.  This enables animation, styling, binding, etc...
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty InternalTabControlProperty = DependencyProperty.RegisterAttached(
                "InternalTabControl",
                typeof(TabControl),
                typeof(TabContent),
                new UIPropertyMetadata(null, OnInternalTabControlChanged));

        // Using a DependencyProperty as the backing store for InternalCachedContent.  This enables animation, styling, binding, etc...
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty InternalCachedContentProperty = DependencyProperty.RegisterAttached(
                "InternalCachedContent",
                typeof(ContentControl),
                typeof(TabContent),
                new UIPropertyMetadata(null));

        // Using a DependencyProperty as the backing store for InternalContentManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InternalContentManagerProperty = DependencyProperty.RegisterAttached(
            "InternalContentManager",
            typeof(object),
            typeof(TabContent),
            new UIPropertyMetadata(null));

        public static bool GetIsCached(DependencyObject obj)
        {
            return (bool?) obj?.GetValue(IsCachedProperty) ?? false;
        }

        public static void SetIsCached(DependencyObject obj, bool value)
        {
            obj?.SetValue(IsCachedProperty, value);
        }


        public static DataTemplate GetTemplate(DependencyObject obj)
        {
            return (DataTemplate) obj?.GetValue(TemplateProperty);
        }

        public static void SetTemplate(DependencyObject obj, DataTemplate value)
        {
            obj?.SetValue(TemplateProperty, value);
        }


        public static DataTemplateSelector GetTemplateSelector(DependencyObject obj)
        {
            return (DataTemplateSelector) obj?.GetValue(TemplateSelectorProperty);
        }

        public static void SetTemplateSelector(DependencyObject obj, DataTemplateSelector value)
        {
            obj?.SetValue(TemplateSelectorProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static TabControl GetInternalTabControl(DependencyObject obj)
        {
            return (TabControl) obj?.GetValue(InternalTabControlProperty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetInternalTabControl(DependencyObject obj, TabControl value)
        {
            obj?.SetValue(InternalTabControlProperty, value);
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public static ContentControl GetInternalCachedContent(DependencyObject obj)
        {
            return (ContentControl) obj?.GetValue(InternalCachedContentProperty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetInternalCachedContent(DependencyObject obj, ContentControl value)
        {
            obj?.SetValue(InternalCachedContentProperty, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object GetInternalContentManager(DependencyObject obj)
        {
            return obj?.GetValue(InternalContentManagerProperty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetInternalContentManager(DependencyObject obj, object value)
        {
            obj?.SetValue(InternalContentManagerProperty, value);
        }

        private static void OnIsCachedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj == null) return;

            if (!(obj is TabControl tabControl))
                throw new InvalidOperationException("Cannot set TabContent.IsCached on object of type " +
                                                    args.NewValue.GetType().Name +
                                                    ". Only objects of type TabControl can have TabContent.IsCached property.");

            var newValue = (bool) args.NewValue;

            if (!newValue)
            {
                if (args.OldValue != null && (bool) args.OldValue)
                    throw new NotImplementedException(ErrorMessages.TabCachingOffNotImplemented);

                return;
            }

            EnsureContentTemplateIsNull(tabControl);
            tabControl.ContentTemplate = CreateContentTemplate();
            EnsureContentTemplateIsNotModified(tabControl);
        }

        private static DataTemplate CreateContentTemplate()
        {
            const string xaml =
                "<DataTemplate><Border b:TabContent.InternalTabControl=\"{Binding RelativeSource={RelativeSource AncestorType=TabControl}}\" /></DataTemplate>";

            var context = new ParserContext {XamlTypeMapper = new XamlTypeMapper(Array.Empty<string>())};

            context.XamlTypeMapper.AddMappingProcessingInstruction("b", typeof(TabContent).Namespace ?? string.Empty,
                typeof(TabContent).Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("b", "b");

            var template = (DataTemplate) XamlReader.Parse(xaml, context);
            return template;
        }

        private static void OnInternalTabControlChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj == null) return;

            if (!(obj is Decorator container))
            {
                var message = "Cannot set TabContent.InternalTabControl on object of type " + obj.GetType().Name +
                              ". Only controls that derive from Decorator, such as Border can have a TabContent.InternalTabControl.";
                throw new InvalidOperationException(message);
            }

            if (args.NewValue == null) return;

            if (!(args.NewValue is TabControl))
                throw new InvalidOperationException("Value of TabContent.InternalTabControl cannot be of type " +
                                                    args.NewValue.GetType().Name + ", it must be of type TabControl");

            var tabControl = (TabControl) args.NewValue;
            var contentManager = GetContentManager(tabControl, container);
            contentManager.UpdateSelectedTab();
        }

        private static ContentManager GetContentManager(TabControl tabControl, Decorator container)
        {
            var contentManager = (ContentManager) GetInternalContentManager(tabControl);
            var contentManagerExists = contentManager != null;

            if (contentManagerExists)
            {
                /*
                 * Tab content template is applied again, and new instance of the Border control (container) has been created.
                 * The old container referenced by the content manager is no longer visible and needs to be replaced
                 */
                contentManager.ReplaceContainer(container);
            }
            else
            {
                contentManager = new ContentManager(tabControl, container);
                SetInternalContentManager(tabControl, contentManager);
            }

            return contentManager;
        }

        private static void EnsureContentTemplateIsNull(TabControl tabControl)
        {
            if (tabControl.ContentTemplate == null) return;

            throw new InvalidOperationException(ErrorMessages.ContentTemplateNotNull);
        }

        private static void EnsureContentTemplateIsNotModified(TabControl tabControl)
        {
            var descriptor =
                DependencyPropertyDescriptor.FromProperty(TabControl.ContentTemplateProperty, typeof(TabControl));

            descriptor.AddValueChanged(
                tabControl,
                (sender, args) => throw new InvalidOperationException(ErrorMessages.ContentTemplateModified));
        }
    }
}