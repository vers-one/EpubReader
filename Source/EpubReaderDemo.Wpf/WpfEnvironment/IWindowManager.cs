namespace EpubReaderDemo.Wpf.WpfEnvironment
{
    internal interface IWindowManager
    {
        IWindowContext CreateWindow(object viewModel);
        IWindowContext FindActiveWindow();
        OpenFileDialogResult ShowOpenFileDialog(OpenFileDialogParameters openFileDialogParameters);
    }
}
