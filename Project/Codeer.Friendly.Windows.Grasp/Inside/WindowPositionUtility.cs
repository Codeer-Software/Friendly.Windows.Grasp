using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Codeer.Friendly.Windows.Grasp.Inside
{
	/// <summary>
	/// ウィンドウ位置に関するユーティリティー。
	/// </summary>
	public static class WindowPositionUtility
	{
        /// <summary>
        /// Zインデックスで最前面のウィンドウハンドルを取得。
        /// </summary>
        public static IntPtr GetZTopHandle()
        {
            IntPtr[] sorted = GetTopLevelWindows();
            return (sorted.Length == 0) ? (IntPtr.Zero) : (sorted[sorted.Length - 1]);
        }

        /// <summary>
        /// トップレベルウィンドウの取得。
        /// </summary>
        /// <returns>トップレベルウィンドウ。</returns>
        public static IntPtr[] GetTopLevelWindows()
        {
            int processId = Process.GetCurrentProcess().Id;
            int currentThreadId = NativeMethods.GetCurrentThreadId();
            IntPtr serverWnd = IntPtr.Zero;
            List<IntPtr> handles = new List<IntPtr>();
            NativeMethods.EnumWindowsDelegate callback = delegate(IntPtr hWnd, IntPtr lParam)
            {
                if (!NativeMethods.IsWindow(hWnd))
                {
                    return 1;
                }
                if (!NativeMethods.IsWindowVisible(hWnd))
                {
                    return 1;
                }
                if (!NativeMethods.IsWindowEnabled(hWnd))
                {
                    return 1;
                }
                int windowProcessId = 0;
                int threadId = NativeMethods.GetWindowThreadProcessId(hWnd, out windowProcessId);
                if (processId == windowProcessId && currentThreadId == threadId)
                {
                    handles.Add(hWnd);
                }
                return 1;
            };
            NativeMethods.EnumWindows(callback, IntPtr.Zero);
            GC.KeepAlive(callback);
            IntPtr[] sorted = WindowPositionUtility.SortByZIndex(handles.ToArray());
            return sorted;
        }

		/// <summary>
		/// Zインデックスでソート。
		/// </summary>
		/// <param name="handles">ハンドル。</param>
		/// <returns>ソート結果。</returns>
		public static IntPtr[] SortByZIndex(IntPtr[] handles)
		{
			if (handles.Length <= 1)
			{
				return handles;
			}

			//同階層のウィンドウを全て取得。
			List<IntPtr> all = new List<IntPtr>();
			IntPtr last = NativeMethods.GetWindow(handles[0], (uint)NativeMethods.GetWindowCmd.GW_HWNDLAST);
			IntPtr check = last;
			all.Add(last);
			while (true)
			{
				IntPtr ptr = NativeMethods.GetWindow(check, (uint)NativeMethods.GetWindowCmd.GW_HWNDPREV);
				if (all.IndexOf(ptr) != -1)
				{
					break;
				}
				all.Add(ptr);
				check = ptr;
			}

			//ソート。
			List<IntPtr> list = new List<IntPtr>(handles);
			list.Sort(delegate(IntPtr h1, IntPtr h2)
			{
				int index1 = all.IndexOf(h1);
				int index2 = all.IndexOf(h2);
				if (index1 < index2)
				{
					return -1;
				}
				if (index2 < index1)
				{
					return 1;
				}
				return 0;
			});
			return list.ToArray();
		}
	}
}
