using System;
using System.ComponentModel;
using System.Linq.Expressions;
using VersOne.Epub.WpfDemo.Utils;

namespace VersOne.Epub.WpfDemo.ViewModels
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
