using System;
using System.Windows;

namespace VersOne.Epub.WpfDemo.WpfEnvironment
{
    /// <summary>
    /// Context of a window. Contains various events, properties, and methods for window-related operations.
    /// </summary>
    internal interface IWindowContext
    {
        /// <summary>
        /// Occurs when the window is activated.
        /// </summary>
        event EventHandler Activated;

        /// <summary>
        /// Occurs directly before the window is shown.
        /// </summary>
        event EventHandler Showing;

        /// <summary>
        /// Occurs directly before the window is closed.
        /// </summary>
        event EventHandler Closing;

        /// <summary>
        /// Occurs after the window is closed.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        /// Gets the name of the view type associated with this window.
        /// </summary>
        string ViewName { get; }

        /// <summary>
        /// Gets the reference to the underlying WPF window.
        /// </summary>
        Window Window { get; }

        /// <summary>
        /// Gets the data context object associated with this window context.
        /// </summary>
        object DataContext { get; }

        /// <summary>
        /// Opens the window and returns without waiting for the newly opened window to close.
        /// </summary>
        /// <param name="showMaximized"><c>true</c> if the window needs to be maximized after it is shown; otherwise, <c>false</c>.</param>
        void Show(bool showMaximized = false);

        /// <summary>
        /// Opens the window and returns only when the newly opened window is closed.
        /// </summary>
        /// <param name="ownerWindowContext">An optional parameter that specifies the context of a window that should to be set as an owner for the newly opened window.</param>
        /// <param name="showMaximized"><c>true</c> if the window needs to be maximized after it is shown; otherwise, <c>false</c>.</param>
        /// <returns>A value that specifies whether the activity was accepted (<c>true</c>) or canceled (<c>false</c>).</returns>
        bool? ShowDialog(IWindowContext ownerWindowContext = null, bool showMaximized = false);

        /// <summary>
        /// Opens the window and returns only when the newly opened window is closed.
        /// </summary>
        /// <param name="ownerHandle">An optional parameter that specifies the handle of a window that should to be set as an owner for the newly opened window.</param>
        /// <param name="showMaximized"><c>true</c> if the window needs to be maximized after it is shown; otherwise, <c>false</c>.</param>
        /// <returns>A value that specifies whether the activity was accepted (<c>true</c>) or canceled (<c>false</c>).</returns>
        bool? ShowDialog(IntPtr ownerHandle, bool showMaximized = false);

        /// <summary>
        /// Closes the window.
        /// </summary>
        void Close();

        /// <summary>
        /// Closes the window with a specified dialog result
        /// which is returned by the <see cref="ShowDialog(IWindowContext, bool)" /> and the <see cref="ShowDialog(IntPtr, bool)" /> methods.
        /// </summary>
        /// <param name="dialogResult">
        /// Dialog result to be returned by the <see cref="ShowDialog(IWindowContext, bool)" /> and the <see cref="ShowDialog(IntPtr, bool)" /> methods.
        /// </param>
        void CloseDialog(bool dialogResult);

        /// <summary>
        /// Sets the focus to the window.
        /// </summary>
        void Focus();
    }
}
