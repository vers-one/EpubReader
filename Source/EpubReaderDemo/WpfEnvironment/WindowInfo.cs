using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.WpfEnvironment
{
    internal class WindowInfo
    {
        public WindowInfo(string viewName, Type windowType, Type viewModelType)
        {
            ViewName = viewName;
            WindowType = windowType;
            ViewModelType = viewModelType;
        }

        public string ViewName { get; private set; }
        public Type WindowType { get; private set; }
        public Type ViewModelType { get; private set; }
    }
}
