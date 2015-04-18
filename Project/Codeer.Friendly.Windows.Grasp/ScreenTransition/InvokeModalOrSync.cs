namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls that has action that show modal dialog or synchronize with the before action.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
    public class InvokeModalOrSync<T>
    {
        WindowsAppFriend _app;
        InvokeModal<T>.ShowModal _show;
        InvokeModal<T>.CreateWindowDriver _create;
        InvokeSync.Sync _sync;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="show">Show modal function.</param>
        /// <param name="create">Create window driver function.</param>
        /// <param name="sync">Synchronize with the before action.</param>
        public InvokeModalOrSync(WindowsAppFriend app, InvokeModal<T>.ShowModal show, InvokeModal<T>.CreateWindowDriver create, InvokeSync.Sync sync)
        {
            _app = app;
            _show = show;
            _create = create;
            _sync = sync;
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
            if (w == null)
            {
                _sync();
                return default(T);
            }
            return _create(w, a);
        }
    }
}
