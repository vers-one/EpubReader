namespace VersOne.Epub.WpfDemo.WpfEnvironment
{
    /// <summary>
    /// Provides methods for various operations related to window contexts and dialog windows.
    /// </summary>
    internal interface IWindowManager
    {
        /// <summary>
        /// Creates an instance of a window associated with the type of the provided view model and returns its window context.
        /// </summary>
        /// <param name="viewModel">View model for which the window needs to be created. The type of the window is chosen by the type of the view model.</param>
        /// <returns>Window context for the created window.</returns>
        IWindowContext CreateWindow(object viewModel);

        /// <summary>
        /// Gets the window context for the active window.
        /// </summary>
        /// <returns>Window context for the active window.</returns>
        IWindowContext FindActiveWindow();

        /// <summary>
        /// Shows the standard open file dialog window, waits for the user's action, and then returns the dialog result.
        /// </summary>
        /// <param name="openFileDialogParameters">The parameters that configure the behavior of the dialog window.</param>
        /// <returns>The result of the user's action in the dialog window.</returns>
        OpenFileDialogResult ShowOpenFileDialog(OpenFileDialogParameters openFileDialogParameters);
    }
}
