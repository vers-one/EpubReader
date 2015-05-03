using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EpubReaderDemo.Utils
{
    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataContextProperty = DependencyProperty.Register("DataContext", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object DataContext
        {
            get
            {
                return (object)GetValue(DataContextProperty);
            }
            set
            {
                SetValue(DataContextProperty, value);
            }
        }
    }
}
