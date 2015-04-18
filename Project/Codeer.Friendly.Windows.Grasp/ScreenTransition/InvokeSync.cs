namespace Codeer.Friendly.Windows.Grasp.ScreenTransition
{
    /// <summary>
    /// Provides operations on controls that has action that synchronize with the before action.
    /// </summary>
    public class InvokeSync
    {
        /// <summary>
        /// Finish.
        /// </summary>
        public delegate void Finish();
        
        /// <summary>
        /// Synchronize.
        /// </summary>
        public delegate void Sync();

        Finish _finish;
        Sync _sync;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="finish">Finish.</param>
        /// <param name="sync">Synchronize.</param>
        public InvokeSync(Finish finish, Sync sync)
        {
            _finish = finish;
            _sync = sync;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        protected void Invoke()
        {
            _finish();
            _sync();
        }
    }
}
