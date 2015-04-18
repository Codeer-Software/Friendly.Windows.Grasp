namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls  that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
    public class InvokeModal<T>
    {
        /// <summary>
        /// Create window driver.
        /// </summary>
        /// <param name="window">window.</param>
        /// <param name="async">Modal dialog show function's async object.</param>
        /// <returns>window driver.</returns>
        public delegate T CreateWindowDriver(WindowControl window, Async async);
        
        /// <summary>
        /// Show modal.
        /// </summary>
        /// <param name="async">async object.</param>
        public delegate void ShowModal(Async async);

        WindowsAppFriend _app;
        ShowModal _show;
        CreateWindowDriver _create;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="show">Show modal function.</param>
        /// <param name="create">Create window driver function.</param>
        public InvokeModal(WindowsAppFriend app, ShowModal show, CreateWindowDriver create)
        {
            _app = app;
            _show = show;
            _create = create;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <returns>Modal dialog's window driver.</returns>
        protected T Invoke()
        {
            var current = WindowControl.FromZTop(_app);
            var a = new Async();
            _show(a);
            var w = current.WaitForNextModal(a);
            return w == null ? default(T) : _create(w, a);
        }
    }
}
