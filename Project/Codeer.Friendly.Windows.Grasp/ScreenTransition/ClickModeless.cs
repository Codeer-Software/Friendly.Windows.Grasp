namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls  that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modeless dialog's window driver type.</typeparam>
    public class ClickModeless<T> : InvokeModeless<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="show">Show modeless.</param>
        /// <param name="create">Create window driver.</param>
        public ClickModeless(ShowModeless show, CreateWindowDriver create) : base(show, create) { }

#if ENG
        /// <summary>
        /// Performs a click.
        /// </summary>
        /// <returns>Modeless dialog's window driver.</returns>
#else
        /// <summary>
        /// クリックです。
        /// </summary>
        /// <returns>モーダレスダイアログのウィンドウドライバです。</returns>
#endif
        public T EmulateClick() { return Invoke(); }
    }
}
