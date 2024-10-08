using System;

namespace Codeer.Friendly.Windows.Grasp
{

#if ENG
    /// <summary>
    /// Information for sending messages.
    /// Used for arguments to SequentialMessage() in WindowControl.
    /// </summary>
#else
    /// <summary>
    /// メッセージ情報。
    /// </summary>
#endif
    [Serializable]
    public class MessageInfo
    {
#if ENG
        /// <summary>
        /// message.
        /// </summary>
#else
        /// <summary>
        /// メッセージ。
        /// </summary>
#endif
        public int Message { get; set; }

#if ENG
        /// <summary>
        /// wparam.
        /// </summary>
#else
        /// <summary>
        /// wparam。
        /// </summary>
#endif
        public IntPtr WParam { get; set; }

#if ENG
        /// <summary>
        /// lparam.
        /// </summary>
#else
        /// <summary>
        /// lparam。
        /// </summary>
#endif
        public IntPtr LParam { get; set; }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
#endif
        public MessageInfo() { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="wparam">wparam.</param>
        /// <param name="lparam">lparam.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
#endif
        public MessageInfo(int message, long wparam, long lparam)
            : this(message, LongToIntPtr(wparam), LongToIntPtr(lparam)) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="wparam">wparam.</param>
        /// <param name="lparam">lparam.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
#endif
        public MessageInfo(int message, IntPtr wparam, IntPtr lparam)
        {
            Message = message;
            WParam = wparam;
            LParam = lparam;
        }

        /// <summary>
        /// longからIntPtrに変換。
        /// </summary>
        /// <param name="value">long値。</param>
        /// <returns>IntPtr値。</returns>
        static IntPtr LongToIntPtr(long value)
        {
            if (IntPtr.Size == 4)
            {
                uint tmp = (uint)value;
                return new IntPtr((int)tmp);
            }
            return new IntPtr(value);
        }
    }
}
