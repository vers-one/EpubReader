using System.Windows;
using System.Windows.Controls;
using VersOne.Epub.WpfDemo.ViewModels;

namespace VersOne.Epub.WpfDemo.Controls
{
    public class NavigationTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NavigationHeaderTemplate { get; set; }
        public DataTemplate NavigationItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is NavigationItemViewModel navigationItemViewModel)
            {
                return navigationItemViewModel.IsLink ? NavigationItemTemplate : NavigationHeaderTemplate;
            }
            else
            {
                return null;
            }
        }
    }
}
