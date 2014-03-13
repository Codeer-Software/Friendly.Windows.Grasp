#define CODE_ANALYSIS
using System;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Codeer.Friendly.DotNetExecutor;
using System.CodeDom.Compiler;
using System.Text;
using System.IO;
using Codeer.Friendly.Windows.Grasp.Properties;
using System.Collections.Generic;
using Codeer.Friendly.Windows.Grasp.Inside.InApp;

namespace Codeer.Friendly.Windows.Grasp.Inside
{
    /// <summary>
    /// 対象アプリケーション初期化。
    /// </summary>
    public static class TargetAppInitializer
    {
        /// <summary>
        /// 対象アプリケーション初期化。
        /// </summary>
        /// <param name="app">アプリケーションクラス。</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static AppVar Initialize(WindowsAppFriend app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }
            string key = typeof(WindowControl).Module.Name + "[Initialize]";
            object ohterSystemAnalyzersObj;
            if (!app.TryGetAppControlInfo(key, out ohterSystemAnalyzersObj))
            {
                //自身のアセンブリをロードさせる。
                WindowsAppExpander.LoadAssembly(app, typeof(TargetAppInitializer).Assembly);

                //WpfAnalyzerをコンパイルしてインストール
                AppVar ohterSystemAnalyzers = null;
                try
                {
                    app["System.Windows.Application.Current"]()["Windows"]()["Count"]()["ToString"](); //ここでWPFのライブラリがロードできるかチェックする。
                    if ((bool)app[typeof(TargetAppInitializer), "InstallWpfInApp"]().Core)
                    {
                        AppVar wpfAnalyzer = app.Dim(new NewInfo("Codeer.Friendly.Windows.Wpf.Grasp.WpfAnalyzer"));
                        ohterSystemAnalyzers = app.Dim(new IOtherSystemWindowAnalyzer[1]);
                        ohterSystemAnalyzers["[]"](0, wpfAnalyzer);
                    }
                }
                catch { }
                if (ohterSystemAnalyzers == null)
                {
                    ohterSystemAnalyzers = app.Dim(new IOtherSystemWindowAnalyzer[0]);
                }
                app.AddAppControlInfo(key, ohterSystemAnalyzers);
                ohterSystemAnalyzersObj = ohterSystemAnalyzers;
            }
            return ohterSystemAnalyzersObj as AppVar;
        }

        /// <summary>
        /// WPFモジュールのインストール
        /// </summary>
        /// <returns></returns>
        static bool InstallWpfInApp()
        {
            TypeFinder finder = new TypeFinder();
            Type t = finder.GetType("Codeer.Friendly.Windows.Wpf.Grasp.WpfAnalyzer");
            if (t != null)
            {
                return true;
            }

            //参照
            List<string> reference = new List<string>();
            reference.Add(typeof(TargetAppInitializer).Assembly.Location);
            reference.Add(typeof(AppVar).Assembly.Location);
            reference.Add("System.dll");
            reference.Add("System.Drawing.dll");
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                switch (asm.GetName().Name)
                {
                    case "PresentationCore":
                    case "PresentationFramework":
                    case "WindowsBase":
                    case "System.Xaml":
                        reference.Add(asm.Location);
                        break;
                }
            }
            CompilerResults compilerResults = CspCompiler.Compile(reference.ToArray(), Resources.WpfAnalyzer);
            return !compilerResults.Errors.HasErrors;
        }
    }
}
