using System.Windows;
using System.Windows.Controls;
using VersOne.Epub.WpfDemo.ViewModels;

namespace VersOne.Epub.WpfDemo.Controls
{
    /// <summary>
    /// A data template selector class that chooses between the <see cref="NavigationHeaderTemplate" /> and <see cref="NavigationItemTemplate" /> templates
    /// based on the type of the navigation item.
    /// </summary>
    public class NavigationTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the data template to render a navigation header.
        /// </summary>
        public DataTemplate NavigationHeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the data template to render a navigation link.
        /// </summary>
        public DataTemplate NavigationItemTemplate { get; set; }

        /// <summary>
        /// Returns the <see cref="NavigationItemTemplate" /> if the <paramref name="item" /> is a navigation link and <see cref="NavigationHeaderTemplate" /> otherwise.
        /// </summary>
        /// <param name="item">The navigation item for which to select the template.</param>
        /// <param name="container">The data-bound object. Not used in this template selector.</param>
        /// <returns>The selected data template.</returns>
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
