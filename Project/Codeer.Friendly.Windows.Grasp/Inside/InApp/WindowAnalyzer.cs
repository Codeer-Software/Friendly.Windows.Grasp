using System;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.Grasp.Inside.InApp
{
	/// <summary>
	/// ウィンドウ解析。
	/// </summary>
	public static class WindowAnalyzer
	{
		const int WindowClassNameMax = 1024;

        /// <summary>
        /// ウィンドウハンドルからオブジェクトを取得。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <param name="analyzer">別システムウィンドウ解析。</param>
        /// <returns>オブジェクト。</returns>
        public static object FromHandle(IntPtr windowHandle, IOtherSystemWindowAnalyzer[] analyzer)
		{
			for(int i = 0; i < analyzer.Length; i++)
			{
                object obj = analyzer[i].FromHandle(windowHandle);
				if (obj != null)
				{
					return obj;
				}
			}
			return Control.FromHandle(windowHandle);
		}

        /// <summary>
        /// オブジェクトからウィンドウハンドル取得。
        /// </summary>
        /// <param name="obj">オブジェクト。</param>
        /// <param name="analyzer">別システムウィンドウ解析。</param>
        /// <returns>ウィンドウハンドル。</returns>
        public static IntPtr GetHandle(object obj, IOtherSystemWindowAnalyzer[] analyzer)
		{
            for (int i = 0; i < analyzer.Length; i++)
            {
                IntPtr handle = analyzer[i].GetHandle(obj);
				if (handle != IntPtr.Zero)
				{
					return handle;
				}
			}
			Control control = obj as Control;
			if (control != null)
			{
				return control.Handle;
			}
			return IntPtr.Zero;
		}

        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="analyzer">別システムウィンドウ解析。</param>
        /// <returns>解析結果。</returns>
        public static WindowInfo Analyze(IntPtr handle, IOtherSystemWindowAnalyzer[] analyzer)
        {
            //子ウィンドウを全て取得する
            List<IntPtr> children = new List<IntPtr>();
            NativeMethods.EnumWindowsDelegate callback = delegate(IntPtr hWnd, IntPtr lParam)
            {
                children.Add(hWnd);
                return 1;
            };
            NativeMethods.EnumChildWindows(handle, callback, IntPtr.Zero);
            GC.KeepAlive(callback);

            //ウィンドウ情報を取得する
            WindowInfo info = new WindowInfo();
            info.Handle = handle;
            NativeMethods.RECT rc = new NativeMethods.RECT();
            NativeMethods.GetWindowRect(handle, ref rc);
            List<int> zIndex = new List<int>();
            GetWindowInfo(new Point(rc.left, rc.top), info, children, zIndex, analyzer);
            return info;
        }
        
        /// <summary>
        /// .Netのタイプ名称が一致するウィンドウを全て取得。
        /// </summary>
        /// <param name="root">ウィンドウ情報。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>オブジェクト配列。</returns>
        static object[] GetFromTypeFullName(WindowInfo root, string typeFullName)
        {
            List<object> infos = new List<object>();
            FindWindow(root, infos, delegate(WindowInfo info)
            {
                return info.TypeFullName == typeFullName;
            });
            return infos.ToArray();
        }

        /// <summary>
        /// WindowInfoヒットチェック。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <returns>ヒットしたか。</returns>
        delegate bool IsHit(WindowInfo info);

        /// <summary>
        /// ウィンドウ検索。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <param name="hitWindows">ヒットウィンドウ格納バッファ。</param>
        /// <param name="checkHit">ヒット確認デリゲート。</param>
        static void FindWindow(WindowInfo info, List<object> hitWindows, IsHit checkHit)
        {
            if (checkHit(info))
            {
                hitWindows.Add(info.TargetObject);
            }
            for (int i = 0; i < info.Children.Length; i++)
            {
                FindWindow(info.Children[i], hitWindows, checkHit);
            }
        }

        /// <summary>
        /// ロジカルツリーインデックスからオブジェクトを取得。
        /// </summary>
        /// <param name="info">情報。</param>
        /// <param name="logicalTreeIndex">ロジカルツリーインデックス。</param>
        /// <returns>オブジェクト。</returns>
        public static object IdentifyFromLogicalTreeIndex(WindowInfo info, int[] logicalTreeIndex)
        {
            return IdentifyFromTreeIndex(info, delegate(WindowInfo i) { return i.LogicalTreeIndex; }, logicalTreeIndex);
        }

        /// <summary>
        /// ロジカルツリーインデックスからオブジェクトを取得。
        /// </summary>
        /// <param name="info">情報。</param>
        /// <param name="visualTreeIndex">ロジカルツリーインデックス。</param>
        /// <returns>オブジェクト。</returns>
        public static object IdentifyFromVisualTreeIndex(WindowInfo info, int[] visualTreeIndex)
        {
            return IdentifyFromTreeIndex(info, delegate(WindowInfo i) { return i.VisualTreeIndex; }, visualTreeIndex);
        }

        /// <summary>
        /// 対象のインデックスを取得するための関数。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <returns>対象のインデックス。</returns>
        delegate int[] GetTargetIndices(WindowInfo info);

        /// <summary>
        /// ツリーインデックスからオブジェクトを取得。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <param name="getTargetIndices">対象のインデックスを取得するための関数。</param>
        /// <param name="treeIndex">ツリーインデックス。</param>
        /// <returns>オブジェクト。</returns>
        static object IdentifyFromTreeIndex(WindowInfo info, GetTargetIndices getTargetIndices, int[] treeIndex)
        {
            int[] targetIndex = getTargetIndices(info);
            if (targetIndex.Length == treeIndex.Length)
            {
                bool isSame = true;
                for (int i = 0; i < targetIndex.Length; i++)
                {
                    if (targetIndex[i] != treeIndex[i])
                    {
                        isSame = false;
                        break;
                    }
                }
                if (isSame)
                {
                    return info.TargetObject;
                }
            }
            for (int i = 0; i < info.Children.Length; i++)
            {
                object obj = IdentifyFromTreeIndex(info.Children[i], getTargetIndices, treeIndex);
                if (obj != null)
                {
                    return obj;
                }
            }
            return null;
        }

		/// <summary>
        /// ウィンドウ情報取得。
		/// </summary>
        /// <param name="pos">ルートウィンドウの左上座標。</param>
        /// <param name="info">ウィンドウ情報格納バッファ。</param>
        /// <param name="children">子ウィンドウハンドル。</param>
        /// <param name="analyzer">別システムウィンドウ解析。</param>
        /// <param name="zIndex">Zインデックス。</param>
        private static void GetWindowInfo(Point pos, WindowInfo info, List<IntPtr> children, List<int> zIndex, IOtherSystemWindowAnalyzer[] analyzer)
		{
			NativeMethods.RECT rc = new NativeMethods.RECT();
			NativeMethods.GetWindowRect(info.Handle, ref rc);
			info.Bounds = new Rectangle(rc.left - pos.X, rc.top - pos.Y, rc.right - rc.left, rc.bottom - rc.top);

			//ウィンドウテキスト取得
            int len = NativeMethods.GetWindowTextLength(info.Handle);
            StringBuilder builder = new StringBuilder((len + 1) * 8);
			NativeMethods.GetWindowText(info.Handle, builder, len * 8);
			info.Text = builder.ToString();
			
			//z順設定
			info.ZIndex = zIndex.ToArray();

			//ダイアログID取得
			info.DialogId = NativeMethods.GetDlgCtrlID(info.Handle);

			//ウィンドウクラス名称取得
			StringBuilder className = new StringBuilder((WindowClassNameMax + 1) * 8);
			NativeMethods.GetClassName(info.Handle, className, WindowClassNameMax * 8);
			info.ClassName = className.ToString();

            //他のウィンドウシステムのウィンドウの場合は解析処理を委譲
            List<WindowInfo> otherSystemChildren = new List<WindowInfo>();
            for (int i = 0; i < analyzer.Length; i++)
            {
                WindowInfo obj = analyzer[i].Analyze(info.Handle);
                if (obj != null)
                {
                    info.TypeFullName = obj.TypeFullName;
                    info.TargetObject = obj.TargetObject;
                    otherSystemChildren.AddRange(obj.Children);
                    break;
                }
            }
            if (info.TargetObject == null)
            {
                //.Netのコントロールの場合、型を取得する
                Control net = Control.FromHandle(info.Handle);
                if (net != null)
                {
                    info.TypeFullName = net.GetType().FullName;
                    info.TargetObject = net;
                }
            }

			//自分の子ウィンドウを集める
			List<IntPtr> myChildren = new List<IntPtr>();
			for (int i = children.Count - 1; 0 <= i; i--)
			{
				if (NativeMethods.GetParent(children[i]) == info.Handle)
				{
					myChildren.Add(children[i]);
					children.RemoveAt(i);
				}
			}

			//Z順でソートする
			IntPtr[] myChildrenSort = WindowPositionUtility.SortByZIndex(myChildren.ToArray());

			//子のウィンドウ情報を設定する
			zIndex.Add(0);
            List<WindowInfo> allChildren = new List<WindowInfo>();
            for (int i = 0; i < myChildrenSort.Length; i++)
			{
				zIndex[zIndex.Count - 1] = i;
				WindowInfo c = new WindowInfo();
				c.Handle = myChildrenSort[i];
                allChildren.Add(c);
				GetWindowInfo(pos, c, children, zIndex, analyzer);
			}
			zIndex.RemoveAt(zIndex.Count - 1);
            
            //別システムに属するウィンドウはZIndexの整合性が取れなくなるので最後に追加
            allChildren.AddRange(otherSystemChildren);
            info.Children = allChildren.ToArray();
		}
	}
}
