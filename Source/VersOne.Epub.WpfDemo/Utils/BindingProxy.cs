using System.Windows;

namespace VersOne.Epub.WpfDemo.Utils
{
    /// <summary>
    /// A utility object that allows to capture a data context in one place of a XAML document and then use it in another place of the same document.
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// Identifies the <see cref="DataContext" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the data context object that needs to be captured.
        /// </summary>
        public object DataContext
        {
            get
            {
                return GetValue(DataContextProperty);
            }
            set
            {
                SetValue(DataContextProperty, value);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindingProxy" /> class.
        /// </summary>
        /// <returns>A new instance of the <see cref="BindingProxy" /> class.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}
