using System;

namespace Codeer.Friendly.Windows.Grasp.Inside.InApp
{
    /// <summary>
    /// 別システムのウィンドウ解析。
    /// </summary>
	public interface IOtherSystemWindowAnalyzer
	{
        /// <summary>
        /// ウィンドウハンドルからオブジェクト取得。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <returns>オブジェクト。</returns>
        object FromHandle(IntPtr windowHandle);

        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <returns>ウィンドウ情報。</returns>
        WindowInfo Analyze(IntPtr windowHandle);

        /// <summary>
        /// オブジェクトからウィンドウハンドル取得。
        /// </summary>
        /// <param name="obj">オブジェクト。</param>
        /// <returns>ウィンドウハンドル。</returns>
		IntPtr GetHandle(object obj);
	}
}
