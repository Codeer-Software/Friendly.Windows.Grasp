using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly.Windows.Grasp.Inside
{
	/// <summary>
	/// WindowsApi。
	/// </summary>
	public static class NativeMethods
	{
        /// <summary>
        /// GetWindow関数のコマンド。
        /// </summary>
        public enum GetWindowCmd
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}

        /// <summary>
        /// ウィンドウ取得。
        /// </summary>
        /// <param name="hwd">元ウィンドウハンドル。</param>
        /// <param name="uCmd">関係性。</param>
        /// <returns>ウィンドウ。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindow(IntPtr hwd, uint uCmd);

		/// <summary>
        /// WndProcメッセージ情報。
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		internal struct CWPSTRUCT
		{
			public UIntPtr lparam;
			public UIntPtr wparam;
			public uint message;
			public IntPtr hwnd;
		}

		/// <summary>
        /// メッセージ情報。
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		internal struct MSG
		{
			public IntPtr hwnd;
			public uint message;
			public UIntPtr wParam;
			public UIntPtr lParam;
			public Int32 time;
			public Point pt;
		}

		/// <summary>
        /// 矩形。
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
        }

        /// <summary>
        /// 先祖フラグ
        /// </summary>
        public enum GetAncestorFlags
        {
            GA_PARENT = 1,
            GA_ROOT = 2,
            GA_ROOTOWNER = 3
        }

        /// <summary>
        /// アクティブなウィンドウを取得
        /// </summary>
        /// <returns>アクティブなウィンドウ</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        /// 先祖ウィンドウ取得
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="flags">フラグ</param>
        /// <returns>先祖ウィンドウ</returns>
        [DllImport("user32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetAncestor(IntPtr hWnd, GetAncestorFlags flags);

        /// <summary>
        /// 前面のウィンドウを取得
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <returns>前面のウィンドウ</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// ウィンドウ列挙時のハンドラ。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lParam">パラメータ。</param>
        /// <returns>0を返すと列挙終了。</returns>
        internal delegate int EnumWindowsDelegate(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// メッセージ送信。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// ウィンドウ列挙。
		/// </summary>
        /// <param name="lpEnumFunc">列挙コールバック。</param>
        /// <param name="lParam">パラメータ。</param>
        /// <returns>結果。</returns>
		[DllImport("user32.dll")]
        internal static extern int EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lParam);

		/// <summary>
        /// 子ウィンドウ列挙。
		/// </summary>
        /// <param name="hWndParent">親ウィンドウ。</param>
        /// <param name="lpEnumFunc">列挙コールバック。</param>
        /// <param name="lParam">パラメータ。</param>
        /// <returns>結果。</returns>
		[DllImport("user32.dll")]
        internal static extern int EnumChildWindows(IntPtr hWndParent, EnumWindowsDelegate lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// 指定のウィンドウハンドルが存在するか。
        /// </summary>
        /// <param name="hWnd">ハンドル。</param>
        /// <returns>存在するか。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

		/// <summary>
        /// 可視状態であるか。
		/// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>可視状態であるか。</returns>
		[DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// 有効なウィンドウであるか。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>有効なウィンドウであるか。</returns>
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsWindowEnabled(IntPtr hWnd);

        /// <summary>
        /// 指定のウィンドウハンドルの所属するスレッドとプロセスの取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lpdwProcessId">プロセスID。</param>
        /// <returns>スレッドID。</returns>
        [DllImport("user32.dll")]
        internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		/// <summary>
        /// Window文字列取得。
		/// </summary>
        /// <param name="hWnd">ハンドル。</param>
        /// <param name="lpString">文字列格納バッファ。</param>
        /// <param name="nMaxCount">最大文字列。</param>
        /// <returns>結果。</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		/// <summary>
        /// ウィンドウテキストの長さを取得する。
		/// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ウィンドウテキストの長さ。</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// ウィンドウテキストの設定。
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル。</param>
        /// <param name="lpString">設定文字列。</param>
        /// <returns>成否。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowText(IntPtr hwnd, String lpString);

		/// <summary>
        /// ダイアログIDの取得。
		/// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ダイアログID。</returns>
		[DllImport("user32.dll")]
		internal static extern int GetDlgCtrlID(IntPtr hWnd);

		/// <summary>
        /// 親ウィンドウを取得する。
		/// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>親ウィンドウ。</returns>
		[DllImport("user32.dll")]
		internal static extern IntPtr GetParent(IntPtr hWnd);

		/// <summary>
        /// ウィンドウ矩形の取得。
		/// </summary>
        /// <param name="hwnd">ウィンドウハンドル。</param>
        /// <param name="lpRect">矩形。</param>
        /// <returns>成否。</returns>
		[DllImport("user32.dll")]
		internal static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        /// <summary>
        /// ウィンドウ矩形の取得。
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル。</param>
        /// <returns>矩形</returns>
        internal static Rectangle GetWindowRectEx(IntPtr hwnd)
        {
            var rc = new RECT();
            GetWindowRect(hwnd, ref rc);
            return new Rectangle(rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top);
        }

		/// <summary>
        /// クラス名称を取得。
		/// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lpClassName">クラス名称格納バッファ。</param>
        /// <param name="nMaxCount">最大文字数。</param>
        /// <returns>文字サイズ。</returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// フォーカスの設定。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>前のフォーカスウィンドウハンドル。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// 現在のスレッドIDを取得。
        /// </summary>
        /// <returns>現在のスレッドID。</returns>
        [DllImport("kernel32.dll")]
        internal static extern int GetCurrentThreadId();

        /// <summary>
        /// クライアント座標からスクリーン座標に変換
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lpPoint">座標</param>
        /// <returns>成否</returns>
        [DllImport("user32.dll")]
        internal static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        /// <summary>
        /// クライアント座標からスクリーン座標に変換
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="point"> クライアント座標</param>
        /// <returns>スクリーン座標</returns>
        internal static Point ClientToScreenEx(IntPtr hWnd, Point point)
        {
            ClientToScreen(hWnd, ref point);
            return point;
        }
    }
}
