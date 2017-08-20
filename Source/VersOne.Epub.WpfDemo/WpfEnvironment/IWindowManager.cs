namespace VersOne.Epub.WpfDemo.WpfEnvironment
{
    internal interface IWindowManager
    {
        IWindowContext CreateWindow(object viewModel);
        IWindowContext FindActiveWindow();
        OpenFileDialogResult ShowOpenFileDialog(OpenFileDialogParameters openFileDialogParameters);
    }
}
