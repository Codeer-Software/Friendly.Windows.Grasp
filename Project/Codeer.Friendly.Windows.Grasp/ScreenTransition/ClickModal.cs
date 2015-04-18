namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls  that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
    public class ClickModal<T> : InvokeModal<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="show">Show modal function.</param>
        /// <param name="create">Create window driver function.</param>
        public ClickModal(WindowsAppFriend app, ShowModal show, CreateWindowDriver create)
            : base(app, show, create) { }

#if ENG
        /// <summary>
        /// Performs a click.
        /// </summary>
        /// <returns>Modal dialog's window driver.</returns>
#else
        /// <summary>
        /// クリックです。
        /// </summary>
        /// <returns>モーダルダイアログのウィンドウドライバです。</returns>
#endif
        public T EmulateClick() { return Invoke(); }
    }
}
