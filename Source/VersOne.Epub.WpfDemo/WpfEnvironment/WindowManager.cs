﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace VersOne.Epub.WpfDemo.WpfEnvironment
{
    internal class WindowManager: IWindowManager
    {
        private static readonly WindowManager instance;
        private Dictionary<string, WindowInfo> registeredWindowsByViewName;
        private Dictionary<Type, WindowInfo> registeredWindowsByViewModel;
        private Dictionary<string, IWindowContext> openWindows;
        private IWindowContext lastActivatedWindowContext;

        static WindowManager()
        {
            instance = new WindowManager();
        }

        private WindowManager()
        {
            registeredWindowsByViewName = new Dictionary<string, WindowInfo>();
            registeredWindowsByViewModel = new Dictionary<Type, WindowInfo>();
            openWindows = new Dictionary<string, IWindowContext>();
            lastActivatedWindowContext = null;
            EnumerateWindowsInAssembly();
        }

        public static IWindowManager Instance
        {
            get
            {
                return instance;
            }
        }

        public IWindowContext CreateWindow(object viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }
            if (!registeredWindowsByViewModel.TryGetValue(viewModel.GetType(), out WindowInfo windowInfo))
            {
                throw new ArgumentException($"There are no registered window for {viewModel.GetType().FullName} type.");
            }
            if (!(Activator.CreateInstance(windowInfo.WindowType) is Window window))
            {
                throw new InvalidOperationException($"There was an error while trying to create an instance of {windowInfo.WindowType.FullName} window class.");
            }
            window.DataContext = viewModel;
            IWindowContext windowContext = new WindowContext(this, windowInfo.ViewName, window, viewModel);
            windowContext.Activated += Activated;
            windowContext.Showing += Showing;
            windowContext.Closed += Closed;
            return windowContext;
        }

        public IWindowContext FindActiveWindow()
        {
            return lastActivatedWindowContext;
        }

        public OpenFileDialogResult ShowOpenFileDialog(OpenFileDialogParameters openFileDialogParameters)
        {
            if (openFileDialogParameters == null)
            {
                throw new ArgumentNullException("openFileDialogParameters");
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (!String.IsNullOrEmpty(openFileDialogParameters.Filter))
            {
                openFileDialog.Filter = openFileDialogParameters.Filter;
            }
            openFileDialog.Multiselect = openFileDialogParameters.Multiselect;
            if (!String.IsNullOrEmpty(openFileDialogParameters.InitialDirectory))
            {
                openFileDialog.InitialDirectory = openFileDialogParameters.InitialDirectory;
            }
#pragma warning disable IDE0017 // The order of the calls is important here
            OpenFileDialogResult result = new OpenFileDialogResult();
            result.DialogResult = openFileDialog.ShowDialog() == true;
            result.SelectedFilePaths = result.DialogResult ? openFileDialog.FileNames.ToList() : new List<string>();
#pragma warning restore IDE0017
            return result;
        }

        private void EnumerateWindowsInAssembly()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.Name.EndsWith("View", StringComparison.OrdinalIgnoreCase) && typeof(Window).IsAssignableFrom(type))
                {
                    RegisterWindow(type);
                }
            }
        }

        private void RegisterWindow(Type windowType)
        {
            string viewName = windowType.Name.Substring(0, windowType.Name.Length - 4);
            if (registeredWindowsByViewName.ContainsKey(viewName))
            {
                throw new Exception($"View {viewName} has been declared more than once.");
            }
            string viewModelTypeName = viewName + "ViewModel";
            Type viewModelType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(type => String.Compare(type.Name, viewModelTypeName, StringComparison.OrdinalIgnoreCase) == 0);
            WindowInfo windowInfo = new WindowInfo(viewName, windowType, viewModelType);
            registeredWindowsByViewName.Add(viewName, windowInfo);
            if (viewModelType != null)
            {
                registeredWindowsByViewModel.Add(viewModelType, windowInfo);
            }
        }

        private void Activated(object sender, EventArgs e)
        {
            lastActivatedWindowContext = (IWindowContext)sender;
        }

        private void Showing(object sender, EventArgs e)
        {
            IWindowContext windowContext = (IWindowContext)sender;
            openWindows.Add(windowContext.ViewName, windowContext);
        }

        private void Closed(object sender, EventArgs e)
        {
            IWindowContext windowContext = (IWindowContext)sender;
            openWindows.Remove(windowContext.ViewName);
            windowContext.Showing -= Showing;
            windowContext.Closed -= Closed;
        }
    }
}
