using SystrayLauncher.Langs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SystrayLauncher.Utils
{
    internal static class Misc
    {
        #region intro
        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
        #endregion

        public static double CalcScreenScaling()
        {
            var dc = GetDC(IntPtr.Zero);
            double r = 1.0;
            using (var g = Graphics.FromHdc(dc))
            {
                r = Math.Max(g.DpiX, g.DpiY) / 96.0;
            }
            ReleaseDC(IntPtr.Zero, dc);
            return r;
        }

        public static string GetAssemblyVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return version.ToString();
        }

        public static bool Confirm(string message)
        {
            var r = MessageBox.Show(
                message,
                I18N.Confirm,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            return r == DialogResult.Yes;
        }

        public static void Warning(string message)
        {
            MessageBox.Show(
                message,
                I18N.Warning,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
