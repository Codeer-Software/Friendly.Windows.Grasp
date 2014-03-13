using System;

namespace Codeer.Friendly.Windows.Grasp.Inside.InApp
{
    /// <summary>
    /// メッセージユーティリティー。
    /// </summary>
    public static class MessageUtility
    {
        /// <summary>
        /// メッセージを連続送信。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <param name="info">メッセージ情報。</param>
        public static void SendMessage(IntPtr windowHandle, MessageInfo[] info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            for (int i = 0; i < info.Length; i++)
            {
                NativeMethods.SendMessage(windowHandle, info[i].Message, info[i].WParam, info[i].LParam);
            }
        }
    }
}
