using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// The base class for all view models in this application.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers the <see cref="PropertyChanged" /> event.
        /// </summary>
        /// <param name="propertyName">
        /// Property name to be supplied for the <see cref="PropertyChanged" /> event.
        /// If this parameter is not set, then the property name of the caller is used.
        /// </param>
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
