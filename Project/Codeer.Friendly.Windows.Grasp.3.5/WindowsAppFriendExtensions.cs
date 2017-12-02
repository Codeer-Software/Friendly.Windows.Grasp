using System;

namespace Codeer.Friendly.Windows.Grasp
{
#if ENG
    /// <summary>
    /// Extended method of WindowsAppFriend.
    /// The first argument defined in WindowControl makes it possible to call the static method of WindowAppFriend with extension method.
    /// </summary>
#else
    /// <summary>
    /// WindowsAppFriendの拡張メソッドです。
    /// WindowControlに定義されている第一引数がWindowAppFriendのstaticメソッドを拡張メソッドで呼び出せるようにしています。
    /// </summary>
#endif
    public static class WindowsAppFriendExtensions
    {
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
        public static WindowControl FromZTop(this WindowsAppFriend app)
            => WindowControl.FromZTop(app);

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
        public static WindowControl[] GetTopLevelWindows(this WindowsAppFriend app)
            => WindowControl.GetTopLevelWindows(app);

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
        public static WindowControl IdentifyFromWindowText(this WindowsAppFriend app, string text)
            => WindowControl.IdentifyFromWindowText(app, text);

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
        public static WindowControl IdentifyFromTypeFullName(this WindowsAppFriend app, string typeFullName)
            => WindowControl.IdentifyFromTypeFullName(app, typeFullName);

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
        public static WindowControl IdentifyFromWindowClass(this WindowsAppFriend app, string className)
            => WindowControl.IdentifyFromWindowClass(app, className);

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
        public static WindowControl WaitForIdentifyFromWindowText(this WindowsAppFriend app, string text)
            => WindowControl.WaitForIdentifyFromWindowText(app, text);

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
        public static WindowControl WaitForIdentifyFromWindowText(this WindowsAppFriend app, string text, Async async)
            => WindowControl.WaitForIdentifyFromWindowText(app, text, async);

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
        public static WindowControl WaitForIdentifyFromTypeFullName(this WindowsAppFriend app, string typeFullName)
            => WindowControl.WaitForIdentifyFromTypeFullName(app, typeFullName);

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
        public static WindowControl WaitForIdentifyFromTypeFullName(this WindowsAppFriend app, string typeFullName, Async async)
            => WindowControl.WaitForIdentifyFromTypeFullName(app, typeFullName, async);

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
        public static WindowControl WaitForIdentifyFromWindowClass(this WindowsAppFriend app, string className)
            => WindowControl.WaitForIdentifyFromWindowClass(app, className);

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
        public static WindowControl WaitForIdentifyFromWindowClass(this WindowsAppFriend app, string className, Async async)
            => WindowControl.WaitForIdentifyFromWindowClass(app, className, async);

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
        public static WindowControl[] GetFromWindowText(this WindowsAppFriend app, string text)
            => WindowControl.GetFromWindowText(app, text);


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
        public static WindowControl[] GetFromTypeFullName(this WindowsAppFriend app, string typeFullName)
            => WindowControl.GetFromTypeFullName(app, typeFullName);

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
        public static WindowControl[] GetFromWindowClass(this WindowsAppFriend app, string className)
            => WindowControl.GetFromWindowClass(app, className);

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
        public static WindowControl WaitForNextWindow(this WindowsAppFriend app, Action action)
            => WindowControl.WaitForNextWindow(app, () => action());

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
        public static WindowControl WaitForNextWindow(this WindowsAppFriend app, Action action, Async async)
            => WindowControl.WaitForNextWindow(app, () => action(), async);

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
        public static WindowControl[] GetNextWindows(this WindowsAppFriend app, Action action)
            => WindowControl.GetNextWindows(app, () => action());
    }
}
