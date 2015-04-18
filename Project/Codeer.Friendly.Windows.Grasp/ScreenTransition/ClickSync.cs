namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls that has action that synchronize with the before action.
    /// </summary>
    public class ClickSync : InvokeSync
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="finish">Finish.</param>
        /// <param name="sync">Synchronize.</param>
        public ClickSync(Finish finish, Sync sync) : base(finish, sync) { }

#if ENG
        /// <summary>
        /// Performs a click.
        /// </summary>
#else
        /// <summary>
        /// クリックです。
        /// </summary>
#endif
        public void EmulateClick() { Invoke(); }
    }
}
