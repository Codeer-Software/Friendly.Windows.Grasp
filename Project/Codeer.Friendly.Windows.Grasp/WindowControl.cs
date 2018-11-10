#define CODE_ANALYSIS
using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using Codeer.Friendly.Windows.Grasp.Inside;
using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.Friendly.Windows.Grasp.Properties;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Codeer.Friendly.Windows.Grasp
{
#if ENG
    /// <summary>
    /// Allows basic manipulation and identification of windows and their child windows.
    /// Also supports WPF.
    /// </summary>
#else
    /// <summary>
    /// Windowの基本的操作と子ウィンドウの取得、特定ができます。
    /// WPFにも対応しています。
    /// </summary>
#endif
    public class WindowControl : IAppVarOwner, IUIObject
    {
        WindowsAppFriend _app;
        WindowInfo _root;
        AppVar _appVar;
        AppVar _windowInfoInApp;
        bool _autoRefresh = true;

#if ENG
        /// <summary>
        /// Returns the associated application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスを取得します。
        /// </summary>
#endif
        public WindowsAppFriend App { get { return _app; } }

#if ENG
        /// <summary>
        /// Returns the window's handle.
        /// </summary>
#else
        /// <summary>
        /// 対応するウィンドウハンドルを取得します。
        /// </summary>
#endif
        public IntPtr Handle { get { return _root.Handle; } }

#if ENG
        /// <summary>
        /// Controls auto-refresh. 
        /// True by default. 
        /// When true, Refresh() is automatically called to update the window tree for accessing child windows. 
        /// When false, WindowControl stores the window tree from the last time it was updated. 
        /// </summary>
#else
        /// <summary>
        /// 自動リフレッシュ。
        /// 既定ではtrueです。
        /// trueの場合子ウィンドウ取得時に自動でRefreshを呼び出し更新します。
        /// falseにすると更新しなくなり、最後に更新した時点のウィンドウツリー構成が保たれます。
        /// </summary>
#endif
        public bool AutoRefresh { get { return _autoRefresh; } set { _autoRefresh = value; } }

#if ENG
        /// <summary>
        /// Returns an AppVar for a .NET object for the corresponding window.
        /// Can be used only when a corresponding window is a .Net object. 
        /// </summary>
#else
        /// <summary>
        /// 対応するウィンドウの.Netのオブジェクトが格納されたAppVarを取得します。
        /// 対応するウィンドウが.Netのオブジェクトである場合のみ使用可能です。
        /// </summary>
#endif
        public AppVar AppVar
        {
            get
            {
                if (_appVar == null)
                {
                    _appVar = _windowInfoInApp["_targetObject"]();
                    if ((bool)_app[typeof(object), "ReferenceEquals"](_appVar, null).Core)
                    {
                        _appVar = null;
                    }
                }
                return _appVar;
            }
        }

#if ENG
        /// <summary>
        /// Returns the window's dialog ID. 
        /// </summary>
#else
        /// <summary>
        /// ダイアログID
        /// </summary>
#endif
        public int DialogId { get { return _root.DialogId; } }

#if ENG
        /// <summary>
        /// Returns the window's full type name. 
        /// </summary>
#else
        /// <summary>
        /// ウィンドウクラス名称。
        /// </summary>
#endif
        public string WindowClassName { get { return _root.ClassName; } }

#if ENG
        /// <summary>
        /// Returns the .net full type name. 
        /// </summary>
#else
        /// <summary>
        /// .Netのクラスのタイプフルネーム。
        /// </summary>
#endif
        public string TypeFullName { get { return _root.TypeFullName; } }

#if ENG
        /// <summary>
        /// Returns a WindowControl for the window's parent window. 
        /// </summary>
#else
        /// <summary>
        /// 親ウィンドウ。
        /// </summary>
#endif
        public WindowControl ParentWindow
        {
            get
            {
                IntPtr parent = (IntPtr)App[typeof(NativeMethods), "GetParent"](Handle).Core;
                return (parent == IntPtr.Zero) ? null : new WindowControl(App, parent);
            }
        }

#if ENG
        /// <summary>
        /// Returns the size of IUIObject.
        /// </summary>
#else
        /// <summary>
        /// IUIObjectのサイズを取得します。
        /// </summary>
#endif
        public Size Size
        {
            get
            {
                var rc = (Rectangle)App[typeof(NativeMethods), "GetWindowRectEx"](Handle).Core;
                return rc.Size;
            }
        }

#if ENG
        /// <summary>
        /// It acquires delegates to call operations on variables in the test target application.
        /// Can be used only when the corresponding window is a .Net object. 
        /// </summary>
        /// <param name="operation">Name of the operation.</param>
        /// <returns>Operation delegate.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// 対応するウィンドウが.Netのオブジェクトである場合のみ使用可能です。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation]
        {
            get
            {
                AppVar v = AppVar;
                if (v == null)
                {
                    throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.WindowAppVarAccessDisable));
                }
                return v[operation];
            }
        }

#if ENG
        /// <summary>
        /// It acquires delegates to call operations on variables in the test target application.
        /// Can be used only when the corresponding window is a .Net object. 
        /// </summary>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="async">Asynchronous execution object.</param>
        /// <returns>Operation delegate.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// 対応するウィンドウが.Netのオブジェクトである場合のみ使用可能です。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, Async async]
        {
            get
            {
                AppVar v = AppVar;
                if (v == null)
                {
                    throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.WindowAppVarAccessDisable));
                }
                return v[operation, async];
            }
        }

#if ENG
        /// <summary>
        /// It acquires delegates to call operations on variables in the test target application.
        /// Can be used only when the corresponding window is a .Net object. 
        /// </summary>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation. 
        /// In many cases, overloads can be resolved based on the passed arguments without using OperationTypeInfo.
        /// </param>
        /// <returns>Operation delegate.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// 対応するウィンドウが.Netのオブジェクトである場合のみ使用可能です。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo]
        {
            get
            {
                AppVar v = AppVar;
                if (v == null)
                {
                    throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.WindowAppVarAccessDisable));
                }
                return v[operation, operationTypeInfo];
            }
        }

#if ENG
        /// <summary>
        /// It acquires delegates to call operations on variables in the test target application.
        /// Can be used only when the corresponding window is a .Net object. 
        /// </summary>
        /// <param name="operation">Name of the operation.</param>
        /// <param name="operationTypeInfo">
        /// Operation type information
        /// Used to call operation of the same name of a parent class when two or more overloads exist for the indicated operation. 
        /// In many cases, overloads can be resolved based on the passed arguments without using OperationTypeInfo.
        /// </param>
        /// <param name="async">Asynchronous execution object.</param>
        /// <returns>Operation delegate.</returns>
#else
        /// <summary>
        /// テスト対象アプリケーション内の変数の操作を呼び出すdelegateを取得します。
        /// 対応するウィンドウが.Netのオブジェクトである場合のみ使用可能です。
        /// </summary>
        /// <param name="operation">操作。</param>
        /// <param name="operationTypeInfo">操作タイプ情報。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>操作実行delegate。</returns>
#endif
        public FriendlyOperation this[string operation, OperationTypeInfo operationTypeInfo, Async async]
        {
            get
            {
                AppVar v = AppVar;
                if (v == null)
                {
                    throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.WindowAppVarAccessDisable));
                }
                return v[operation, operationTypeInfo, async];
            }
        }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="windowHandle">Window handle of the window to manipulate using the WindowControl.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
#endif
        public WindowControl(WindowsAppFriend app, IntPtr windowHandle)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            InitializeFromaHandle(app, windowHandle);
        }

#if ENG
        /// <summary>
        /// Currently deprecated. 
        /// Please use WindowControl(AppVar windowObject).
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="windowObject">AppVar for a window variable within the application, pertaining to the window to manipulate.</param>
#else
        /// <summary>
        /// 現在非推奨です。
        /// WindowControl(AppVar windowObject)を使用してください。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowObject">WindowControlで操作する対象のウィンドウのオブジェクトの格納されたアプリケーション内変数です。</param>
#endif
        [Obsolete("Please use WindowControl(AppVar windowObject).", false)]
        public WindowControl(WindowsAppFriend app, AppVar windowObject)
            : this(windowObject) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="windowObject">AppVar for a window variable within the application, pertaining to the window to manipulate.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="windowObject">WindowControlで操作する対象のウィンドウのオブジェクトの格納されたアプリケーション内変数です。</param>
#endif
        public WindowControl(AppVar windowObject)
        {
            if (windowObject == null)
            {
                throw new ArgumentNullException("windowObject");
            }
            WindowsAppFriend app = (WindowsAppFriend)windowObject.App;
            AppVar ohterSystemAnalyzers = TargetAppInitializer.Initialize(app);
            InitializeFromaHandle(app, (IntPtr)app[typeof(WindowAnalyzer), "GetHandle"](windowObject, ohterSystemAnalyzers).Core);
        }


#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">Source.</param>
#else
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="src">元</param>
#endif
        protected WindowControl(WindowControl src)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }
            _app = src._app;
            _root = src._root;
            _windowInfoInApp = src._windowInfoInApp;
        }

#if ENG
        /// <summary>
        /// Creates a WindowControl for the window closest to the foreground.
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <returns>WindowControl to manipulate the accessed window.</returns>
#else
        /// <summary>
        /// 最前面ウィンドウを取得します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <returns>最前面ウィンドウ。</returns>
#endif
        public static WindowControl FromZTop(WindowsAppFriend app)
        {
            TargetAppInitializer.Initialize(app);
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            IntPtr handle = IntPtr.Zero;
            while (handle == IntPtr.Zero)
            {
                //ダイアログ起動時、有効で表示状態のウィンドウが0になる可能性があるのでリトライ。
                handle = (IntPtr)app[typeof(WindowPositionUtility), "GetZTopHandle"]().Core;
                CheckApplicationConnection(app.ProcessId);
                Thread.Sleep(10);
            }
            return new WindowControl(app, handle);
        }

#if ENG
        /// <summary>
        /// Acquires all of the active top-level windows in the target process. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <returns>Window manipulation objects for all of the active top-level windows of the target process. </returns>
#else
        /// <summary>
        /// 対象プロセスの有効な全てのトップレベルウィンドウを取得。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <returns>対象プロセスの有効な全てのトップレベルウィンドウ。</returns>
#endif
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static WindowControl[] GetTopLevelWindows(WindowsAppFriend app)
        {
            TargetAppInitializer.Initialize(app);
            IntPtr[] handles = (IntPtr[])app[typeof(WindowPositionUtility), "GetTopLevelWindows"]().Core;
            List<WindowControl> list = new List<WindowControl>();
            foreach (IntPtr element in handles)
            {
                try
                {
                    list.Add(new WindowControl(app, element));
                }
                catch { }
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Identifies a top-level window based on the string value returned from the Windows Api's GetWindowText. 
        /// Fails when two or more windows match the same condition. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="text">Window text.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// WindowsApiのGetWindowTextで取得した文字列が、指定の文字列になる有効なトップレベルウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl IdentifyFromWindowText(WindowsAppFriend app, string text)
        {
            TargetAppInitializer.Initialize(app);
            return IdentifyTopLevelWindow(GetFromWindowText(app, text));
        }

#if ENG
        /// <summary>
        /// Identifies a top-level window based on its full .Net type name. 
        /// Fails when two or more windows match the same condition. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="typeFullName">Full .Net type name.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致する有効なトップレベルウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl IdentifyFromTypeFullName(WindowsAppFriend app, string typeFullName)
        {
            TargetAppInitializer.Initialize(app);
            return IdentifyTopLevelWindow(GetFromTypeFullName(app, typeFullName));
        }

#if ENG
        /// <summary>
        /// Identifies a window based its window class name. 
        /// Fails when two or more windows match the same criteria. 
        /// Window class name can be easily investigated with TestAssistant.
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="className">Window class name.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// ウィンドウクラス名称に一致する有効なトップレベルウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl IdentifyFromWindowClass(WindowsAppFriend app, string className)
        {
            TargetAppInitializer.Initialize(app);
            return IdentifyTopLevelWindow(GetFromWindowClass(app, className));
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window returning the specified string via the GetWindowText Windows API is found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="text">Window text.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// WindowsApiのGetWindowTextで取得した文字列が、指定の文字列になる有効なトップレベルウィンドウを特定するまで待ちます。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl WaitForIdentifyFromWindowText(WindowsAppFriend app, string text)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromWindowText(app, text); }, null);
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window returning the specified string via the GetWindowText Windows API is found. 
        /// Also returns if the indicated asynchronous operation completes before a window can be found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="text">Window text.</param>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>
        /// Window manipulation object.
        /// Returns null if the Async operation completes before Identify window.
        /// </returns>
#else
        /// <summary>
        /// WindowsApiのGetWindowTextで取得した文字列が、指定の文字列になる有効なトップレベルウィンドウを特定するまで待ちます。
        /// ウィンドウを特定前に渡された非同期オブジェクト(async)が操作完了した場合にも終了します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>ウィンドウ操作クラス。(特定前に、非同期処理が終了した場合はnull)</returns>
#endif
        public static WindowControl WaitForIdentifyFromWindowText(WindowsAppFriend app, string text, Async async)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromWindowText(app, text); }, async);
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window with the indicated full .Net name is found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="typeFullName">Full .Net type name</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致する有効なトップレベルウィンドウを特定するまで待ちます。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl WaitForIdentifyFromTypeFullName(WindowsAppFriend app, string typeFullName)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromTypeFullName(app, typeFullName); }, null);
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window with the indicated full .Net name is found. 
        /// Also returns if the indicated asynchronous operation completes before a window can be found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="typeFullName">Full .Net type name</param>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>
        /// Window manipulation object.
        /// Returns null if the Async operation completes before Identify window.
        /// </returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致する有効なトップレベルウィンドウを特定するまで待ちます。
        /// ウィンドウを特定前に渡された非同期オブジェクト(async)が操作完了した場合にも終了します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>ウィンドウ操作クラス。(特定前に、非同期処理が終了した場合はnull)</returns>
#endif
        public static WindowControl WaitForIdentifyFromTypeFullName(WindowsAppFriend app, string typeFullName, Async async)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromTypeFullName(app, typeFullName); }, async);
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window with the indicated window class is found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="className">Window class name.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// ウィンドウクラス名称に一致する有効なトップレベルウィンドウを特定するまで待ちます。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public static WindowControl WaitForIdentifyFromWindowClass(WindowsAppFriend app, string className)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromWindowClass(app, className); }, null);
        }

#if ENG
        /// <summary>
        /// Waits until a top-level window with the indicated window class is found. 
        /// Also returns if the indicated asynchronous operation completes before a window can be found. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="className">Window class name.</param>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>
        /// Window manipulation object.
        /// Returns null if the Async operation completes before Identify window.
        /// </returns>
#else
        /// <summary>
        /// ウィンドウクラス名称に一致する有効なトップレベルウィンドウを特定するまで待ちます。
        /// ウィンドウを特定前に渡された非同期オブジェクト(async)が操作完了した場合にも終了します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>ウィンドウ操作クラス。(特定前に、非同期処理が終了した場合はnull)</returns>
#endif
        public static WindowControl WaitForIdentifyFromWindowClass(WindowsAppFriend app, string className, Async async)
        {
            TargetAppInitializer.Initialize(app);
            return WaitForIdentifyWindow(app, delegate { return GetFromWindowClass(app, className); }, async);
        }

        /// <summary>
        /// ウィンドウ検索ロジック。
        /// </summary>
        /// <returns>検索結果。</returns>
        delegate WindowControl[] FindTopLevelWindow();

        /// <summary>
        /// ウィンドウ特定待ち。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="find">ウィンドウ検索ロジック。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>特定されたウィンドウ。(特定前に、非同期処理が終了した場合はnull)</returns>
        private static WindowControl WaitForIdentifyWindow(WindowsAppFriend app, FindTopLevelWindow find, Async async)
        {
            while (true)
            {
                WindowControl[] next = find();
                if (next.Length == 1)
                {
                    return next[0];
                }
                if (async != null && async.IsCompleted)
                {
                    return null;
                }
                Thread.Sleep(10);
            }
        }

#if ENG
        /// <summary>
        /// Retrieves all top-level windows matching the indicated window text. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="text">Window text.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定のウィンドウテキストに一致する有効なトップレベルウィンドウをすべて取得します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public static WindowControl[] GetFromWindowText(WindowsAppFriend app, string text)
        {
            TargetAppInitializer.Initialize(app);
            List<WindowControl> list = new List<WindowControl>();
            foreach (WindowControl element in GetTopLevelWindows(app))
            {
                if (element.GetWindowText() == text)
                {
                    list.Add(element);
                }
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Retrieves all top-level windows matching the indicated full .Net type name. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="typeFullName">Full .NET type name.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致する有効なトップレベルウィンドウをすべて取得します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public static WindowControl[] GetFromTypeFullName(WindowsAppFriend app, string typeFullName)
        {
            TargetAppInitializer.Initialize(app);
            List<WindowControl> list = new List<WindowControl>();
            foreach (WindowControl element in GetTopLevelWindows(app))
            {
                if (element.TypeFullName == typeFullName)
                {
                    list.Add(element);
                }
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Retrieves all top-level windows matching the indicated window class type name. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="className">Window class name.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定のウィンドウクラス名称に一致する有効なトップレベルウィンドウをすべて取得します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public static WindowControl[] GetFromWindowClass(WindowsAppFriend app, string className)
        {
            TargetAppInitializer.Initialize(app);
            List<WindowControl> list = new List<WindowControl>();
            foreach (WindowControl element in GetTopLevelWindows(app))
            {
                if (element.WindowClassName == className)
                {
                    list.Add(element);
                }
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Updates the stored child window tree. 
        /// Child windows are obtained based on information retrieved when Refresh() is called. 
        /// </summary>
#else
        /// <summary>
        /// 内部的に保持する子ウィンドウのツリーを更新します。
        /// 以降はこの時点の情報に従って、子ウィンドウの取得、特定が可能となります。
        /// </summary>
#endif
        public void Refresh()
        {
            AppVar ohterSystemAnalyzers = TargetAppInitializer.Initialize(_app);
            _windowInfoInApp = _app[typeof(WindowAnalyzer), "Analyze"](_root.Handle, ohterSystemAnalyzers);
            _root = (WindowInfo)(_windowInfoInApp.Core);
            _appVar = null;
        }

        /// <summary>
        /// 内部的に保持する子ウィンドウのツリーを更新します。
        /// 以降はこの時点の情報に従って、子ウィンドウの取得、特定が可能となります。
        /// </summary>
        private void RefreshAuto()
        {
            if (_autoRefresh)
            {
                Refresh();
            }
        }

#if ENG
        /// <summary>
        /// For WPF windows.
        /// Specifies GUI elements based on the indicated logical tree index.
        /// The logical tree index is decided by the order of acquisition of LogicalTreeHelper.GetChildren.
        /// This is easy to probe using TestAssistant.
        /// </summary>
        /// <param name="logicalTreeIndex">Logical tree index array.</param>
        /// <returns>AppVar for a variable within the application.</returns>
#else
        /// <summary>
        /// WPFのウィンドウ用です。
        /// 指定のロジカルツリーインデックスに対応するGUI要素を特定します。
        /// ロジカルツリーインデックスはLogicalTreeHelper.GetChildrenの取得順によって決定されます。
        /// </summary>
        /// <param name="logicalTreeIndex">ロジカルツリーインデックス。</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar IdentifyFromLogicalTreeIndex(params int[] logicalTreeIndex)
        {
            RefreshAuto();
            AppVar item = _app[typeof(WindowAnalyzer), "IdentifyFromLogicalTreeIndex"](_windowInfoInApp, logicalTreeIndex);
            if ((bool)_app[typeof(object), "ReferenceEquals"](item, null).Core)
            {
                throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
            }
            return item;
        }

#if ENG
        /// <summary>
        /// For WPF windows.
        /// Specifies GUI elements based on the indicated visual tree index.
        /// The visual tree index is decided by the order of acquisition of VisualTreeHelper.GetChild.
        /// This is easy to probe using TestAssistant.
        /// </summary>
        /// <param name="visualTreeIndex">Visual tree index array.</param>
        /// <returns>AppVar for a variable within the application.</returns>
#else
        /// <summary>
        /// WPFのウィンドウ用です。
        /// 指定のビジュアルツリーインデックスに対応するGUI要素を特定します。
        /// ビジュアルツリーインデックスはVisualTreeHelper.GetChildの取得順によって決定されます。
        /// </summary>
        /// <param name="visualTreeIndex">ロジカルツリーインデックス。</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar IdentifyFromVisualTreeIndex(params int[] visualTreeIndex)
        {
            RefreshAuto();
            AppVar item = _app[typeof(WindowAnalyzer), "IdentifyFromVisualTreeIndex"](_windowInfoInApp, visualTreeIndex);
            if ((bool)_app[typeof(object), "ReferenceEquals"](item, null).Core)
            {
                throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
            }
            return item;
        }

#if ENG
        /// <summary>
        /// Retrieves the child window with indicated Z-index. 
        /// The Windows API's GetWindow is used to determine the Z-order. 
        /// Note that MFC dialogs are sorted in the opposite order. 
        /// Z-index can be easily investigated with TestAssistant. 
        /// </summary>
        /// <param name="zindex">Z-index array</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 指定のZ順の子ウィンドウを特定します。
        /// Z順のソートにはWindowsApiのGetWindowが用いられています。
        /// MFCのダイアログの場合、逆順で取得されるので気を付けてください。
        /// </summary>
        /// <param name="zindex">Zインデックス。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public WindowControl IdentifyFromZIndex(params int[] zindex)
        {
            RefreshAuto();
            if (zindex == null)
            {
                throw new ArgumentNullException("zindex");
            }
            WindowInfo info = _root;
            for (int i = 0; i < zindex.Length; i++)
            {
                if (0 <= zindex[i] && zindex[i] < info.Children.Length)
                {
                    info = info.Children[zindex[i]];
                }
                else
                {
                    throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
                }
            }
            return new WindowControl(_app, info.Handle);
        }

#if ENG
        /// <summary>
        /// Identifies a window based its dialog ID. 
        /// Dialog ID can be easily investigated with TestAssistant. 
        /// </summary>
        /// <param name="id">Dialog ID.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 指定のダイアログIDに一致するウィンドウを特定します。
        /// 複数階層になっている場合は、順番にダイアログIDを指定してください。
        /// </summary>
        /// <param name="id">ダイアログID。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public WindowControl IdentifyFromDialogId(params int[] id)
        {
            RefreshAuto();
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            WindowInfo info = _root;
            for (int i = 0; i < id.Length; i++)
            {
                int findCount = 0;
                int index = 0;
                for (int j = 0; j < info.Children.Length; j++)
                {
                    if (info.Children[j].DialogId == id[i])
                    {
                        findCount++;
                        index = j;
                    }
                }

                //見つからなかった場合
                if (findCount == 0)
                {
                    throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
                }
                //複数個発見した場合
                else if (1 < findCount)
                {
                    throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.ManyFoundDialogId));
                }
                //idの最後を発見した場合はそれを返す。
                if (i == id.Length - 1)
                {
                    return new WindowControl(_app, info.Children[index].Handle);
                }
                //途中の場合は次の検索に移る。
                else
                {
                    info = info.Children[index];
                }
            }
            throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
        }

#if ENG
        /// <summary>
        /// Identifies a top-level window based on the string value returned from the Windows Api's GetWindowText. 
        /// Fails when two or more windows match the same condition. 
        /// </summary>
        /// <param name="text">Window text.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// WindowsApiのGetWindowTextで取得した文字列が指定の文字列になるウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public WindowControl IdentifyFromWindowText(string text)
        {
            return Identify(GetFromWindowText(text));
        }

#if ENG
        /// <summary>
        /// Identifies a window matching the indicated rectangle. 
        /// Fails when two or more windows match the same criteria. 
        /// A rectangle can be easily investigated with TestAssistant. 
        /// </summary>
        /// <param name="x">x point.</param>
        /// <param name="y">y point.</param>
        /// <param name="width">width.</param>
        /// <param name="height">height.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 指定の矩形に一致するウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="x">X。</param>
        /// <param name="y">Y。</param>
        /// <param name="width">幅。</param>
        /// <param name="height">高さ。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public WindowControl IdentifyFromBounds(int x, int y, int width, int height)
        {
            return Identify(GetFromBounds(x, y, width, height));
        }

#if ENG
        /// <summary>
        /// Identifies a window based its full .Net type name. 
        /// Fails when two or more windows match the same criteria. 
        /// Full type name can be easily investigated with TestAssistant. 
        /// </summary>
        /// <param name="typeFullName">Full .NET type name.</param>
        /// <returns>AppVar for a variable within the application.</returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致するウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>アプリケーション内変数。</returns>
#endif
        public AppVar IdentifyFromTypeFullName(string typeFullName)
        {
            return Identify(GetFromTypeFullName(typeFullName));
        }

#if ENG
        /// <summary>
        /// Identifies a window based its window class name. 
        /// Fails when two or more windows match the same criteria. 
        /// Window class name can be easily investigated with TestAssistant.
        /// </summary>
        /// <param name="className">Window class name.</param>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// ウィンドウクラス名称に一致するウィンドウを特定します。
        /// 条件に一致するウィンドウが複数存在する場合は特定に失敗します。
        /// </summary>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <returns>ウィンドウ操作クラス。</returns>
#endif
        public WindowControl IdentifyFromWindowClass(string className)
        {
            return Identify(GetFromWindowClass(className));
        }

#if ENG
        /// <summary>
        /// Retrieves all windows corresponding to the indicated window text.
        /// </summary>
        /// <param name="text">Window text.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定のウィンドウテキストに一致するウィンドウをすべて取得します。
        /// </summary>
        /// <param name="text">ウィンドウテキスト。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public WindowControl[] GetFromWindowText(string text)
        {
            RefreshAuto();
            List<WindowControl> infos = new List<WindowControl>();
            FindWindow(_root, infos, delegate(WindowInfo info)
            {
                return info.Text == text;
            });
            return infos.ToArray();
        }

#if ENG
        /// <summary>
        /// It acquires all the windows matching the indicated bounds.
        /// </summary>
        /// <param name="x">x point.</param>
        /// <param name="y">y point.</param>
        /// <param name="width">width.</param>
        /// <param name="height">height.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定の矩形に一致するウィンドウをすべて取得します。
        /// </summary>
        /// <param name="x">X。</param>
        /// <param name="y">Y。</param>
        /// <param name="width">幅。</param>
        /// <param name="height">高さ。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public WindowControl[] GetFromBounds(int x, int y, int width, int height)
        {
            RefreshAuto();
            Rectangle bounds = new Rectangle(x, y, width, height);
            List<WindowControl> infos = new List<WindowControl>();
            FindWindow(_root, infos, delegate(WindowInfo info)
            {
                return info.Bounds == bounds;
            });
            return infos.ToArray();
        }

#if ENG
        /// <summary>
        /// Retrieves all windows with the indicated full .Net type name.
        /// </summary>
        /// <param name="typeFullName">Full .NET type name.</param>
        /// <returns>AppVars for variables in the target application.</returns>
#else
        /// <summary>
        /// 指定の.netタイプフルネームに一致するウィンドウをすべて取得します。
        /// </summary>
        /// <param name="typeFullName">タイプフルネーム。</param>
        /// <returns>アプリケーション内変数配列。</returns>
#endif
        public AppVar[] GetFromTypeFullName(string typeFullName)
        {
            RefreshAuto();
            AppVar ary = _app[typeof(WindowAnalyzer), "GetFromTypeFullName"](_windowInfoInApp, typeFullName);
            List<AppVar> list = new List<AppVar>();
            foreach (AppVar elemennt in new Enumerate(ary))
            {
                list.Add(elemennt);
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Retrieves all windows with the indicated window class.
        /// </summary>
        /// <param name="className">Window class name.</param>
        /// <returns>Window manipulation objects.</returns>
#else
        /// <summary>
        /// 指定のウィンドウクラス名称に一致するウィンドウをすべて取得します。
        /// </summary>
        /// <param name="className">ウィンドウクラス名称。</param>
        /// <returns>ウィンドウ操作クラス配列。</returns>
#endif
        public WindowControl[] GetFromWindowClass(string className)
        {
            RefreshAuto();
            List<WindowControl> infos = new List<WindowControl>();
            FindWindow(_root, infos, delegate(WindowInfo info)
            {
                return info.ClassName == className;
            });
            return infos.ToArray();
        }

#if ENG
        /// <summary>
        /// Currently deprecated. 
        /// Use is not recommended since several modal dialogs can be shown asynchronously and cause confusion. 
        /// Please use one of the following: 
        /// WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass。
        /// It waits for the next window besides itself to become the frontmost window in the application. 
        /// Used to synchronously wait for the next window to be shown when windows are
        /// displayed asynchronously. 
        /// </summary>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// 現在非推奨です。
        /// 自身を除くウィンドウがアプリケーション内で最前面になるのを待ちます。
        /// しかし、モーダレスウィンドウが複数枚表示されている場合の動作が誤解を招く恐れがあるので非推奨とさせていただきます。
        /// 次のいずれかを使用してください。WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass。
        /// </summary>
        /// <returns>次のZインデックスの最前面ウィンドウ。</returns>
#endif
        [Obsolete("Please use one of the following. WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass", false)]
        public WindowControl WaitForNextZTop()
        {
            return WaitForNextZTop(null);
        }

#if ENG
        /// <summary>
        /// Currently deprecated. 
        /// Use is not recommended since several modal dialogs can be shown asynchronously and cause confusion. 
        /// Please use one of the following: 
        /// WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass。
        /// It waits for the next window besides itself to become the frontmost window in the application. 
        /// Used to synchronously wait for the next window to be shown when windows are
        /// displayed asynchronously. 
        /// Also returns if the indicated asynchronous operation completes before a window moves to the front. 
        /// </summary>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>
        /// Window manipulation object.
        /// Returns null if the Async operation completes before a window moves to the front.
        /// </returns>
#else
        /// <summary>
        /// 現在非推奨です。
        /// 自身を除くウィンドウがアプリケーション内で最前面になるのを待ちます。
        /// しかし、モーダレスウィンドウが複数枚表示されている場合の動作が誤解を招く恐れがあるので非推奨とさせていただきます。
        /// 次のいずれかを使用してください。WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass。
        /// </summary>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>次のZインデックスの最前面ウィンドウ(表示前に、非同期処理が終了した場合はnull)。</returns>
#endif
        [Obsolete("Please use one of the following. WaitForNextModal, WaitForIdentifyFromWindowText, WaitForIdentifyFromTypeFullName, WaitForIdentifyFromWindowClass", false)]
        public WindowControl WaitForNextZTop(Async async)
        {
            //トップレベルウィンドウのみ可能
            if (!IsTopLevelWindow())
            {
                throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.TopLevelOnly));
            }
            IntPtr next = (IntPtr)App[typeof(WindowPositionUtility), "GetZTopHandle"]().Core;
            while (next == Handle || next == IntPtr.Zero)
            {
                CheckApplicationConnection(_app.ProcessId);
                if (async != null && async.IsCompleted)
                {
                    return null;
                }
                Thread.Sleep(10);
                next = (IntPtr)App[typeof(WindowPositionUtility), "GetZTopHandle"]().Core;
            }
            return new WindowControl(_app, next);
        }

#if ENG
        /// <summary>
        /// Used to synchronously wait for the next window to be shown when modal dialogs are displayed asynchronously. 
        /// Returns when its own window enters the Disable state and another window becomes the application's only top-level window. 
        /// </summary>
        /// <returns>Window manipulation object.</returns>
#else
        /// <summary>
        /// モーダルダイアログ表示が表示される処理を非同期で呼び出した場合に、次の画面が表示されるまで同期をとるのに使用します。
        /// 自身のウィンドウがDisable状態になり、別のウィンドウがアプリケーション内でただ一つの有効状態で可視状態のトップレベルウィンドウになった場合それを返します。
        /// </summary>
        /// <returns>モーダルダイアログ。</returns>
#endif
        public WindowControl WaitForNextModal()
        {
            return WaitForNextModal(null);
        }

#if ENG
        /// <summary>
        /// Used to synchronously wait for the next window to be shown when modal dialogs are displayed asynchronously. 
        /// Returns when its own window enters the Disable state and another window becomes the application's only top-level window. 
        /// Also returns if the indicated asynchronous operation completes before a window moves to the front. 
        /// Also returns if this window is destroyed before a window moves to the front. 
        /// </summary>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>
        /// Window manipulation object.
        /// Returns null if the Async operation completes before a window moves to the front.
        /// Returns null if this window is destroyed before a window moves to the front.
        /// </returns>
#else
        /// <summary>
        /// モーダルダイアログ表示が表示される処理を非同期で呼び出した場合に、次の画面が表示されるまで同期をとるのに使用します。
        /// 自身のウィンドウがDisable状態になり、別のウィンドウがアプリケーション内でただ一つの有効状態で可視状態のトップレベルウィンドウになった場合それを返します。
        /// また、次のモーダルダイアログが表示されるまでに渡された非同期オブジェクト(async)が操作完了した場合にも終了します。
        /// また、次のモーダルダイアログが表示されるまでに、このウィンドウが破棄された場合にも終了します。
        /// </summary>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>モーダルダイアログ。(表示前に非同期処理が終了した場合はnull、表示前にこのウィンドウが破棄された場合もnull)。</returns>
#endif
        public WindowControl WaitForNextModal(Async async)
        {
            //トップレベルウィンドウのみ可能
            if (!IsTopLevelWindow())
            {
                if (!IsWindow())
                {
                    return null;
                }
                throw new NotSupportedException(ToErrorWidthWindowInfo(Resources.TopLevelOnly));
            }
            while (true)
            {
                IntPtr[] next = (IntPtr[])App[typeof(WindowPositionUtility), "GetTopLevelWindows"]().Core;
                if (!IsWindow())
                {
                    return null;
                }
                if (next.Length == 1 && next[0] != Handle)
                {
                    return new WindowControl(App, next[0]);
                }
                if (async != null && async.IsCompleted)
                {
                    return null;
                }
                Thread.Sleep(10);
            }
        }

#if ENG
        /// <summary>
        /// Operation in which Window is displayed. 
        /// </summary>
#else
        /// <summary>
        /// ウィンドウが表示される動作
        /// </summary>
#endif
        public delegate void ShowWindowAction();

#if ENG
        /// <summary>
        /// Wait for the next window to appear. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="action">Wait for the next window to appear. </param>
        /// <returns>Next window.</returns>
#else
        /// <summary>
        /// 次のウィンドウが表示されるのを待ちます。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="action">ウィンドウが表示される動作。</param>
        /// <returns>次のウィンドウ。</returns>
#endif
        public static WindowControl WaitForNextWindow(WindowsAppFriend app, ShowWindowAction action)
            => WaitForNextWindow(app, action, null);

#if ENG
        /// <summary>
        /// Wait for the next window to appear. 
        /// </summary>
        /// <param name="app">Application manipulation object. </param>
        /// <param name="action">Wait for the next window to appear. </param>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>Next window.</returns>
#else
        /// <summary>
        /// ウィンドウが表示される動作
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="action">ウィンドウが表示される動作。</param>
        /// <param name="async">非同期処理オブジェクト。</param>
        /// <returns>次のウィンドウ。(表示前に非同期処理が終了した場合はnull)。</returns>
#endif
        public static WindowControl WaitForNextWindow(WindowsAppFriend app, ShowWindowAction action, Async async)
        { 
            var oldWindows = GetTopLevelWindows(app);
            action();

            while (true)
            {
                if (async != null && async.IsCompleted) return null;

                var newWindows = GetTopLevelWindows(app);
                foreach (var x in newWindows)
                {
                    var hit = false;
                    foreach (var y in oldWindows)
                    {
                        if (x.Handle == y.Handle)
                        {
                            hit = true;
                            break;
                        }
                    }
                    if (!hit)
                    {
                        return x;
                    }
                }
            }
        }

#if ENG
        /// <summary>
        /// Retrieve the next window displayed after the specified processing. 
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="action">Wait for the next window to appear. </param>
        /// <returns>Next windows.</returns>
#else
        /// <summary>
        /// 指定の処理の次に表示されたウィンドウを取得します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="action">ウィンドウが表示される動作。</param>
        /// <returns>次のウィンドウ。</returns>
#endif
        public static WindowControl[] GetNextWindows(WindowsAppFriend app, ShowWindowAction action)
        {
            var oldWindows = GetTopLevelWindows(app);
            action();
            var newWindows = GetTopLevelWindows(app);

            var list = new List<WindowControl>();
            foreach (var x in newWindows)
            {
                var hit = false;
                foreach (var y in oldWindows)
                {
                    if (x.Handle == y.Handle)
                    {
                        hit = true;
                        break;
                    }
                }
                if (!hit)
                {
                    list.Add(x);
                }
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Waits for the window to be destroyed. 
        /// </summary>
#else
        /// <summary>
        /// ウィンドウが破棄されるのを待ちます。
        /// </summary>
#endif
        public void WaitForDestroy()
        {
            while (IsWindow())
            {
                Thread.Sleep(10);
            }
        }

#if ENG
        /// <summary>
        /// Waits for the window to be destroyed. 
        /// Also returns if the indicated asynchronous operation completes before the window is destroyed. 
        /// </summary>
        /// <param name="async">Asynchronous object.</param>
#else
        /// <summary>
        /// ウィンドウが破棄されるのを待ちます。
        /// また、ウィンドウが破棄されるまでに渡された非同期オブジェクト(async)が操作完了した場合にも終了します。
        /// </summary>
        /// <param name="async">非同期処理オブジェクト。</param>
#endif
        public void WaitForDestroy(Async async)
        {
            if (async == null)
            {
                throw new ArgumentNullException("async");
            }
            while (IsWindow() && !async.IsCompleted)
            {
                Thread.Sleep(10);
            }
        }

#if ENG
        /// <summary>
        /// Check Window Valid.
        /// </summary>
        /// <returns>Window Valid.</returns>
#else
        /// <summary>
        /// ウィンドウハンドルを持つウィンドウが存在しているかどうかを調べます。
        /// </summary>
        /// <returns>ウィンドウハンドルを持つウィンドウが存在しているかどうか。</returns>
#endif
        public bool IsWindow()
        {
            return (bool)App[typeof(NativeMethods), "IsWindow"](Handle).Core;
        }

#if ENG
        /// <summary>
        /// Set's the window's text.
        /// Executed in the target thread of the target application.
        /// </summary>
        /// <param name="text">Window text.</param>
#else
        /// <summary>
        /// ウィンドウテキストの設定。
        /// 対象プロセスの指定のスレッドで実行します。
        /// </summary>
        /// <param name="text">ウィンドウテキスト。</param>
#endif
        public void SetWindowText(string text)
        {
            _app[typeof(NativeMethods), "SetWindowText"](Handle, text);
        }

#if ENG
        /// <summary>
        /// Retrieves the window's text
        /// Executed in the target thread of the target application.
        /// </summary>
        /// <returns>Window text.</returns>
#else
        /// <summary>
        /// ウィンドウテキスト取得。
        /// 対象プロセスの指定のスレッドで実行します。
        /// </summary>
        /// <returns>ウィンドウテキスト。</returns>
#endif
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public string GetWindowText()
        {
            return (string)App[GetType(), "GetWindowTextInTarget"](Handle).Core;
        }

#if ENG
        /// <summary>
        /// Sets focus to this window.
        /// Executed in the target thread of the target application.
        /// </summary>
        /// <returns>Window handle for the window that had focus before SetFocus() was called.</returns>
#else
        /// <summary>
        /// フォーカスの設定。
        /// 対象プロセスの指定のスレッドで実行します。
        /// </summary>
        /// <returns>設定前にフォーカスを保持していたウィンドウハンドル。</returns>
#endif
        public IntPtr SetFocus()
        {
            return (IntPtr)(_app[typeof(NativeMethods), "SetFocus"](Handle).Core);
        }

#if ENG
        /// <summary>
        /// Sends a message to the window.
        /// Executed in the target thread of the target application.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="wparam">WPARAM.</param>
        /// <param name="lparam">LPARAM.</param>
        /// <returns>Result.</returns>
#else
        /// <summary>
        /// メッセージ送信。
        /// 対象プロセスの指定のスレッドで実行します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">WPARAM。</param>
        /// <param name="lparam">LPARAM。</param>
        /// <returns>結果。</returns>
#endif
        public IntPtr SendMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            return (IntPtr)_app[typeof(NativeMethods), "SendMessage"](Handle, message, wparam, lparam).Core;
        }

#if ENG
        /// <summary>
        /// Sends a message to the window.
        /// Executed in the target thread of the target application.
        /// Executes SendMessage asynchronously since PostMessage can fail. 
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="wparam">WPARAM.</param>
        /// <param name="lparam">LPARAM.</param>
        /// <param name="async">Asynchronous object.</param>
        /// <returns>Result. AppVar is IntPtr in target process.</returns>
#else
        /// <summary>
        /// 非同期メッセージ送信。
        /// 対象プロセスの指定のスレッドで実行します。
        /// メッセージの優先順位などを考慮し、あくまでSendMessageを非同期で実行します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">WPARAM。</param>
        /// <param name="lparam">LPARAM。</param>
        /// <param name="async">非同期実行オブジェクト(実行終了を検知する場合に使用する)</param>
        /// <returns>結果。IntPtrが格納されます。</returns>
#endif
        public AppVar SendMessage(int message, IntPtr wparam, IntPtr lparam, Async async)
        {
            return _app[typeof(NativeMethods), "SendMessage", async](Handle, message, wparam, lparam);
        }

#if ENG
        /// <summary>
        /// Calls SendMessage multiple times in the target application's thread. 
        /// </summary>
        /// <param name="info">Array of message information.</param>
#else
        /// <summary>
        /// メッセージを連続送信。
        /// 対象プロセスの指定のスレッドで、連続してSendMessageを実行しましす。
        /// </summary>
        /// <param name="info">メッセージ情報</param>
#endif
        public void SequentialMessage(params MessageInfo[] info)
        {
            _app[typeof(MessageUtility), "SendMessage"](Handle, info);
        }

#if ENG
        /// <summary>
        /// Calls SendMessage multiple times in the target application's thread. 
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous object.</param>
        /// <param name="info">Array of message information.</param>
#else
        /// <summary>
        /// 非同期メッセージを連続送信。
        /// 対象プロセスの指定のスレッドで、連続してSendMessageを実行しましす。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト(実行終了を検知する場合に使用する)。</param>
        /// <param name="info">メッセージ情報</param>
#endif
        public void SequentialMessage(Async async, params MessageInfo[] info)
        {
            _app[typeof(MessageUtility), "SendMessage", async](Handle, info);
        }

#if ENG
        /// <summary>
        /// Convert IUIObject's client coordinates to screen coordinates.
        /// </summary>
        /// <param name="clientPoint">client coordinates.</param>
        /// <returns>screen coordinates.</returns>
#else
        /// <summary>
        /// IUIObjectのクライアント座標からスクリーン座標に変換します。
        /// </summary>
        /// <param name="clientPoint">クライアント座標</param>
        /// <returns>スクリーン座標</returns>
#endif
        public Point PointToScreen(Point clientPoint)
            => (Point)App[typeof(NativeMethods), "ClientToScreenEx"](Handle, clientPoint).Core;

#if ENG
        /// <summary>
        /// Make it active.
        /// </summary>
#else
        /// <summary>
        /// アクティブな状態にします。
        /// </summary>
#endif
        public void Activate()
        {
            var root = (IntPtr)App[typeof(NativeMethods), "GetAncestor"](Handle, NativeMethods.GetAncestorFlags.GA_ROOT).Core;
            while ((IntPtr)App[typeof(NativeMethods), "GetActiveWindow"]().Core != root)
            {
                App[typeof(NativeMethods), "SetForegroundWindow"](root);
                Thread.Sleep(1);
                if (!IsWindow())
                {
                    throw new WindowIdentifyException(Resources.TargetWindowVanish);
                }
            }
            SetFocus();
        }

#if ENG
        /// <summary>
        /// Close Window.
        /// </summary>
#else
        /// <summary>
        /// ウィンドウを閉じます。
        /// </summary>
#endif
        public void Close()
            => SendMessage(0x10, IntPtr.Zero, IntPtr.Zero);

#if ENG
        /// <summary>
        /// Close Window.
        /// </summary>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// ウィンドウを閉じます。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void Close(Async async)
            => SendMessage(0x10, IntPtr.Zero, IntPtr.Zero, async);

        /// <summary>
        /// WindowInfoヒットチェック。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <returns>ヒットしたか。</returns>
        delegate bool IsHit(WindowInfo info);

        /// <summary>
        /// ウィンドウテキスト取得。
        /// 対象プロセスの指定のスレッドで実行します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>ウィンドウテキスト。</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        private static string GetWindowTextInTarget(IntPtr handle)
        {
            int len = NativeMethods.GetWindowTextLength(handle);
            StringBuilder builder = new StringBuilder((len + 1) * 8);
            NativeMethods.GetWindowText(handle, builder, len * 8);
            return builder.ToString();
        }

        /// <summary>
        /// ウィンドウ検索。
        /// </summary>
        /// <param name="info">ウィンドウ情報。</param>
        /// <param name="hitWindows">ヒットウィンドウ格納バッファ。</param>
        /// <param name="checkHit">ヒット確認デリゲート。</param>
        private void FindWindow(WindowInfo info, List<WindowControl> hitWindows, IsHit checkHit)
        {
            if (checkHit(info))
            {
                hitWindows.Add(new WindowControl(_app, info.Handle));
            }
            for (int i = 0; i < info.Children.Length; i++)
            {
                FindWindow(info.Children[i], hitWindows, checkHit);
            }
        }

        /// <summary>
        /// 特定できたかチェック。
        /// 出来ていない場合はユーザーに分かりやすい例外を投げる。
        /// </summary>
        /// <param name="controls">ウィンドウコントロール配列。</param>
        /// <returns>特定されたウィンドウコントロール。</returns>
        private T Identify<T>(T[] controls)
        {
            if (controls.Length == 0)
            {
                throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowNotFound));
            }
            if (controls.Length != 1)
            {
                throw new WindowIdentifyException(ToErrorWidthWindowInfo(Resources.WindowManyFound));
            }
            return controls[0];
        }

        /// <summary>
        /// 特定できたかチェック。
        /// 出来ていない場合はユーザーに分かりやすい例外を投げる。
        /// </summary>
        /// <param name="controls">ウィンドウコントロール配列。</param>
        /// <returns>特定されたウィンドウコントロール。</returns>
        private static T IdentifyTopLevelWindow<T>(T[] controls)
        {
            if (controls.Length == 0)
            {
                throw new WindowIdentifyException(Resources.WindowNotFound);
            }
            if (controls.Length != 1)
            {
                throw new WindowIdentifyException(Resources.WindowManyFound);
            }
            return controls[0];
        }

        /// <summary>
        /// トップレベルウィンドウであるか。
        /// </summary>
        /// <returns>トップレベルウィンドウであるか。</returns>
        bool IsTopLevelWindow()
        {
            return (bool)App[GetType(), "IsTopLevelWindowInTarget"](Handle).Core;
        }

        /// <summary>
        /// トップレベルウィンドウであるか。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>トップレベルウィンドウであるか。</returns>
        static bool IsTopLevelWindowInTarget(IntPtr handle)
        {
            bool isTopLevel = false;
            NativeMethods.EnumWindowsDelegate func = delegate(IntPtr hWnd, IntPtr lParam)
            {
                if (handle == hWnd)
                {
                    isTopLevel = true;
                    return 0;
                }
                return 1;
            };
            NativeMethods.EnumWindows(func, IntPtr.Zero);
            GC.KeepAlive(func);
            return isTopLevel;
        }

        /// <summary>
        /// ウィンドウハンドルから初期化。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        void InitializeFromaHandle(WindowsAppFriend app, IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                throw new WindowIdentifyException(Resources.NotHaveWindowHandle);
            }
            AppVar ohterSystemAnalyzers = TargetAppInitializer.Initialize(app);
            _app = app;
            _windowInfoInApp = _app[typeof(WindowAnalyzer), "Analyze"](windowHandle, ohterSystemAnalyzers);
            _root = (WindowInfo)(_windowInfoInApp.Core);
        }

        /// <summary>
        /// アプリケーションとの通信状態の確認。
        /// </summary>
        /// <param name="id">対象アプリケーションプロセスID</param>
        static void CheckApplicationConnection(int id)
        {
            try
            {
                if (Process.GetProcessById(id) == null)
                {
                    throw new FriendlyOperationException(Resources.ErrorAppCommunication);
                }
            }
            catch
            {
                throw new FriendlyOperationException(Resources.ErrorAppCommunication);
            }
        }

        /// <summary>
        /// ウィンドウ情報を付加したメッセージに変更します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <returns>ウィンドウ情報を付加したエラーメッセージ。</returns>
        private string ToErrorWidthWindowInfo(string message)
        {
            if (_root == null)
            {
                return message;
            }
            string info = string.Format(CultureInfo.CurrentCulture, Resources.WindowInfoAffirmationFormat, _root.Text, _root.TypeFullName, _root.ClassName);
            return message + Environment.NewLine + info;
        }
    }
}