// Tom4u.Toolkit
// Copyright (C) 2020  Thomas Ohms
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Windows.Controls;
using System.Windows.Data;

namespace Tom4u.Toolkit.WpfControls.Common
{
    public static partial class TabContent
    {
        private class ContentManager
        {
            private readonly TabControl tabControl;
            private Decorator border;

            public ContentManager(TabControl tabControl, Decorator border)
            {
                this.tabControl = tabControl;
                this.border = border;
                var control = this.tabControl;
                if (control != null) control.SelectionChanged += (sender, args) => { UpdateSelectedTab(); };
            }

            public void ReplaceContainer(Decorator newBorder)
            {
                if (ReferenceEquals(border, newBorder)) return;

                border.Child = null; // detach any tab content that old border may hold
                border = newBorder;
            }

            public void UpdateSelectedTab()
            {
                border.Child = GetCurrentContent();
            }

            private ContentControl GetCurrentContent()
            {
                var item = tabControl.SelectedItem;
                if (item == null) return null;

                var tabItem = tabControl.ItemContainerGenerator.ContainerFromItem(item);
                if (tabItem == null) return null;

                var cachedContent = GetInternalCachedContent(tabItem);
                if (cachedContent != null) return cachedContent;

                cachedContent = new ContentControl
                {
                    DataContext = item,
                    ContentTemplate = GetTemplate(tabControl),
                    ContentTemplateSelector = GetTemplateSelector(tabControl)
                };

                cachedContent.SetBinding(ContentControl.ContentProperty, new Binding());
                SetInternalCachedContent(tabItem, cachedContent);

                return cachedContent;
            }
        }
    }
}