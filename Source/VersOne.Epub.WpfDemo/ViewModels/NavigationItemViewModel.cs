using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// View model for a navigation item (which can be a link or a header).
    /// </summary>
    public class NavigationItemViewModel : ViewModel
    {
        private bool isTreeItemExpanded;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItemViewModel" /> class for a navigation header.
        /// </summary>
        /// <param name="title">The title of the navigation header.</param>
        /// <param name="nestedItems">A list of the nested navigation items.</param>
        public NavigationItemViewModel(string title, IEnumerable<NavigationItemViewModel> nestedItems)
        {
            IsLink = false;
            Title = title;
            NestedItems = new ObservableCollection<NavigationItemViewModel>(nestedItems);
            isTreeItemExpanded = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationItemViewModel" /> class for a navigation link.
        /// </summary>
        /// <param name="title">The title of the navigation link.</param>
        /// <param name="filePathInEpubArchive">The absolute path for the content file in the EPUB archive referenced by this link.</param>
        /// <param name="anchor">An optional anchor inside the HTML content file that needs to be scrolled to once the content is fully loaded.</param>
        /// <param name="nestedItems">A list of the nested navigation items.</param>
        public NavigationItemViewModel(string title, string filePathInEpubArchive, string anchor, IEnumerable<NavigationItemViewModel> nestedItems)
        {
            IsLink = true;
            FilePathInEpubArchive = filePathInEpubArchive;
            Anchor = anchor;
            Title = title;
            NestedItems = new ObservableCollection<NavigationItemViewModel>(nestedItems);
            isTreeItemExpanded = false;
        }

        /// <summary>
        /// Gets a value indicating whether this navigation item is a clickable link (as opposed to a non-clickable navigation header).
        /// </summary>
        public bool IsLink { get; }

        /// <summary>
        /// Gets the title of the navigation item (which is either the text of the header or the title of the navigation link).
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the absolute path for the content file in the EPUB archive referenced by this navigation item (used only when the value of the <see cref="IsLink" /> is <c>true</c>).
        /// </summary>
        public string FilePathInEpubArchive { get; }

        /// <summary>
        /// Gets an optional anchor inside the HTML content file that needs to be scrolled to once the content is fully loaded.
        /// </summary>
        public string Anchor { get; }

        /// <summary>
        /// Gets a collection of the nested navigation items.
        /// </summary>
        public ObservableCollection<NavigationItemViewModel> NestedItems { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the tree node associated with this navigation item is expanded or not (used only when this navigation item has nested items).
        /// </summary>
        public bool IsTreeItemExpanded
        {
            get
            {
                return isTreeItemExpanded;
            }
            set
            {
                isTreeItemExpanded = value;
                NotifyPropertyChanged();
            }
        }
    }
}
