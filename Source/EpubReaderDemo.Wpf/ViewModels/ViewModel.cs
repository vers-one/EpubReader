using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EpubReaderDemo.Wpf.Utils;

namespace EpubReaderDemo.Wpf.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ExpressionUtils.GetPropertyName(expression)));
        }
    }
}
