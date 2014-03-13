using System;
using System.Drawing;

namespace Codeer.Friendly.Windows.Grasp.Inside
{
	/// <summary>
	/// ウィンドウ情報。
	/// </summary>
	[Serializable]
	public class WindowInfo
	{
		IntPtr _handle;
		string _text = string.Empty;
		string _typeFullName = string.Empty;
		string _dotNetFieldPath = string.Empty;
		string _className = string.Empty;
		int _dialogId;
        int[] _zIndex = new int[0];
        int[] _logicalTreeIndex = new int[0];
        int[] _visualTreeIndex = new int[0];
        WindowInfo[] _children = new WindowInfo[0];
		Rectangle _bounds;
        
        [NonSerialized]
        object _targetObject;

		/// <summary>
		/// ウィンドウハンドル。
		/// </summary>
		public IntPtr Handle { get { return _handle; } set { _handle = value; } }

		/// <summary>
		/// ウィンドウテキスト。
		/// </summary>
		public string Text { get { return _text; } set { _text = value; } }

		/// <summary>
		/// .Netタイプフルネーム。
		/// </summary>
		public string TypeFullName { get { return _typeFullName; } set { _typeFullName = value; } }

		/// <summary>
		/// .Netの場合、この変数を取得するためのフルパス。
		/// </summary>
		public string DotNetFieldPath { get { return _dotNetFieldPath; } set { _dotNetFieldPath = value; } }

		/// <summary>
		/// ダイアログID。
		/// </summary>
		public int DialogId { get { return _dialogId; } set { _dialogId = value; } }

		/// <summary>
		/// ZIndex。
		/// </summary>
		public int[] ZIndex { get { return _zIndex; } set { _zIndex = value; } }

        /// <summary>
        /// LogicalTreeで取得した順番Index。
        /// </summary>
        public int[] LogicalTreeIndex { get { return _logicalTreeIndex; } set { _logicalTreeIndex = value; } }

        /// <summary>
        /// VisualTreeで取得した順番Index。
        /// </summary>
        public int[] VisualTreeIndex { get { return _visualTreeIndex; } set { _visualTreeIndex = value; } }

		/// <summary>
		/// ウィンドウクラス名称。
		/// </summary>
		public string ClassName { get { return _className; } set { _className = value; } }

		/// <summary>
		/// 子ウィンドウ。
		/// </summary>
		public WindowInfo[] Children { get { return _children; } set { _children = value; } }

        /// <summary>
        /// スクリーン座標での矩形。
        /// </summary>
        public Rectangle Bounds { get { return _bounds; } set { _bounds = value; } }

        /// <summary>
        /// 対象オブジェクト。
        /// シリアライズ対象外。
        /// </summary>
        public object TargetObject { get { return _targetObject; } set { _targetObject = value; } }
    }
}
