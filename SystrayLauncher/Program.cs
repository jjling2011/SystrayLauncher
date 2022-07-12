using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SystrayLauncher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var launcher = new Launcher();
            launcher.Init();
            Application.Run();
            launcher?.Dispose();
        }

        [DllImport("user32.dll")]
        public extern static IntPtr SetProcessDPIAware();
    }
}
