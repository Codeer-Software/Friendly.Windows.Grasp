using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Codeer.Friendly.Windows.Grasp
{
#if ENG
    /// <summary>
    /// Inherits Exception.
    /// Thrown when window identification fails.
    /// </summary>
#else
    /// <summary>
    /// ウィンドウ特定失敗例外。
    /// </summary>
#endif
    [Serializable]
    public class WindowIdentifyException : Exception
    {
#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
#endif
        public WindowIdentifyException() { }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="message">message.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
#endif
        public WindowIdentifyException(string message) : base(message) { }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="innerException">Internal Exception.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">内部例外。</param>
#endif
        public WindowIdentifyException(string message, Exception innerException) : base(message, innerException) { }

#if ENG
        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="info">Serialize Infomation.</param>
        /// <param name="context">Context.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
#endif
        protected WindowIdentifyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

#if ENG
        /// <summary>
        /// Serialize.
        /// </summary>
        /// <param name="info">Serialize Infomation.</param>
        /// <param name="context">Context.</param>
#else
        /// <summary>
        /// シリアライズ。
        /// </summary>
        /// <param name="info">シリアライズ情報。</param>
        /// <param name="context">コンテキスト。</param>
#endif
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}