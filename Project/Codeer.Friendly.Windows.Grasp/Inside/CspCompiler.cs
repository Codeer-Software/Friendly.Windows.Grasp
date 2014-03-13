using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.Grasp.Inside
{
    /// <summary>
    /// C#コンパイラ
    /// </summary>
    static class CspCompiler
    {
        /// <summary>
        /// コンパイル。
        /// </summary>
        /// <param name="reference">参照DLL。</param>
        /// <param name="code">コード。</param>
        /// <returns>コンパイル結果。</returns>
        internal static CompilerResults Compile(string[] reference, string code)
        {
            if (reference == null)
            {
                throw new ArgumentNullException("reference");
            }
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters param = new CompilerParameters();
            param.GenerateInMemory = true;
            //参照の追加
            for (int i = 0; i < reference.Length; i++)
            {
                param.ReferencedAssemblies.Add(reference[i]);
            }
            param.IncludeDebugInformation = true;

            //コンパイル
            return codeProvider.CompileAssemblyFromSource(param, code);
        }
    }
}
