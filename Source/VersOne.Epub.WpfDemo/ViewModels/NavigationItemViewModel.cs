using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    public class NavigationItemViewModel : ViewModel
    {
        private bool isTreeItemExpanded;

        public NavigationItemViewModel(string title, IEnumerable<NavigationItemViewModel> nestedItems)
        {
            IsLink = false;
            Title = title;
            NestedItems = new ObservableCollection<NavigationItemViewModel>(nestedItems);
            isTreeItemExpanded = false;
        }

        public NavigationItemViewModel(string title, string filePathInEpubArchive, string anchor, IEnumerable<NavigationItemViewModel> nestedItems)
        {
            IsLink = true;
            FilePathInEpubArchive = filePathInEpubArchive;
            Anchor = anchor;
            Title = title;
            NestedItems = new ObservableCollection<NavigationItemViewModel>(nestedItems);
            isTreeItemExpanded = false;
        }

        public bool IsLink { get; }
        public string Title { get; }
        public string FilePathInEpubArchive { get; }
        public string Anchor { get; }
        public ObservableCollection<NavigationItemViewModel> NestedItems { get; private set; }

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
