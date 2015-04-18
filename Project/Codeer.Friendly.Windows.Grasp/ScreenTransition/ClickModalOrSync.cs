namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls that has action that show modal dialog or synchronize with the before action.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
    public class ClickModalOrSync<T> : InvokeModalOrSync<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="show">Show modal function.</param>
        /// <param name="create">Create window driver function.</param>
        /// <param name="sync">Synchronize with the before action.</param>
        public ClickModalOrSync(WindowsAppFriend app, InvokeModal<T>.ShowModal show, InvokeModal<T>.CreateWindowDriver create, InvokeSync.Sync sync)
            : base(app, show, create, sync) { }

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
