using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EpubReaderDemo.Utils;

namespace EpubReaderDemo.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(ExpressionUtils.GetPropertyName(expression)));
        }
    }
}
