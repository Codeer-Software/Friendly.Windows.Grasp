namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls  that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modeless dialog's window driver type.</typeparam>
    public class InvokeModeless<T>
    {
        /// <summary>
        /// Create window driver.
        /// </summary>
        /// <returns>window driver.</returns>
        public delegate T CreateWindowDriver();

        /// <summary>
        /// Show modeless.
        /// </summary>
        public delegate void ShowModeless();

        ShowModeless _show;
        CreateWindowDriver _create;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="show">Show modeless.</param>
        /// <param name="create">Create window driver.</param>
        public InvokeModeless(ShowModeless show, CreateWindowDriver create)
        {
            _show = show;
            _create = create;
        }


        /// <summary>
        /// Invoke.
        /// </summary>
        /// <returns>Modeless dialog's window driver.</returns>
        protected T Invoke()
        {
            _show();
            return _create();
        }
    }
}
