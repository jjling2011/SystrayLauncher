using SystrayLauncher.Langs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SystrayLauncher.Utils
{
    internal static class Misc
    {
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
