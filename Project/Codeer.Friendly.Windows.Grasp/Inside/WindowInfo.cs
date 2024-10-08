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
        [NonSerialized]
        object _targetObject;

		/// <summary>
		/// ウィンドウハンドル。
		/// </summary>
		public IntPtr Handle { get; set; }

        /// <summary>
        /// ウィンドウテキスト。
        /// </summary>
        public string Text { get; set; }

		/// <summary>
		/// .Netタイプフルネーム。
		/// </summary>
		public string TypeFullName { get; set; }

		/// <summary>
		/// .Netの場合、この変数を取得するためのフルパス。
		/// </summary>
		public string DotNetFieldPath { get; set; }

		/// <summary>
		/// ダイアログID。
		/// </summary>
		public int DialogId { get; set; }

		/// <summary>
		/// ZIndex。
		/// </summary>
		public int[] ZIndex { get; set; }

        /// <summary>
        /// LogicalTreeで取得した順番Index。
        /// </summary>
        public int[] LogicalTreeIndex { get; set; }

        /// <summary>
        /// VisualTreeで取得した順番Index。
        /// </summary>
        public int[] VisualTreeIndex { get; set; }

		/// <summary>
		/// ウィンドウクラス名称。
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// 子ウィンドウ。
		/// </summary>
		public WindowInfo[] Children { get; set; }

        /// <summary>
        /// スクリーン座標での矩形。
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// 対象オブジェクト取得。
        /// シリアライズ対象外。
        /// </summary>
        /// <returns>対象オブジェクト</returns>
        public object GetTargetObject() => _targetObject;

        /// <summary>
        /// 対象オブジェクト設定。
        /// シリアライズ対象外。
        /// </summary>
		/// <param name="obj">対象オブジェクト</param>
        public void SetTargetObject(object obj) => _targetObject = obj;
    }
}
