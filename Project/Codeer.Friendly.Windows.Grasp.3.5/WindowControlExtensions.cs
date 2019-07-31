using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeer.Friendly.Windows.Grasp
{
#if ENG
    /// <summary>
    /// Extended method of WindowControl.
    /// </summary>
#else
    /// <summary>
    /// WindowControlの拡張メソッドです。
    /// </summary>
#endif
    public static class WindowControlExtensions
    {
#if ENG
        /// <summary>
        /// Convert WindowControl to T.
        /// T must have a constructor that takes WindowsControl as an argument.
        /// </summary>
        /// <typeparam name="T">Type of destination.</typeparam>
        /// <param name="src">WindowControl.</param>
        /// <returns>T.</returns>
#else
        /// <summary>
        /// WindowControlからT型に変換します。
        /// T型はWindowControlを引数に取るコンストラクタを持つ必要があります。
        /// </summary>
        /// <typeparam name="T">T変換先の型。</typeparam>
        /// <param name="src">WindowControl.</param>
        /// <returns>T。</returns>
#endif
        public static T Convert<T>(this WindowControl src) where T : class
        {
            if (src == null) return null;
            return (T)Activator.CreateInstance(typeof(T), new object[] { src });
        }
    }
}
