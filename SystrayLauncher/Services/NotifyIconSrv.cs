using SystrayLauncher.Langs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystrayLauncher.Services
{
    internal class NotifyIconSrv : IDisposable
    {
        NotifyIcon notifyIcon;
        bool isDisposed = false;
        public static NotifyIconSrv Instance { get; private set; }

        public NotifyIconSrv()
        {

        }

        #region public methods
        public void Init()
        {
            Instance = this;
            notifyIcon = CreateNotifyIcon();
            RefreshMenu();

            notifyIcon.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    OpenShortcutsFolder();
                }
            };
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
        }
        #endregion

        #region private methods
        void RefreshMenu()
        {
            var menu = notifyIcon.ContextMenuStrip;
            menu.Items.Clear();
            GenerateShortCutMenu(menu);
            AppendBasicMenu(menu);
        }

        ToolStripMenuItem[] WalkThroughDir(string path)
        {
            var r = new List<ToolStripMenuItem>();
            foreach (string d in Directory.GetDirectories(path))
            {
                var items = WalkThroughDir(d);
                var name = d.Split('\\').Last();
                var icon = Properties.Resources.Folder_16x;
                r.Add(new ToolStripMenuItem(name, icon, items));
            }
            foreach (string f in Directory.GetFiles(path))
            {
                var name = f.Split('\\').Last();
                if (name.ToLower() == "desktop.ini")
                {
                    continue;
                }
                var icon = Icon.ExtractAssociatedIcon(f).ToBitmap();
                var item = new ToolStripMenuItem(name, icon, (s, e) =>
                {
                    try
                    {
                        Process.Start(f);
                    }
                    catch (Exception err)
                    {
                        var m = $"{I18N.StartProgFail}\n\n{I18N.Program}: {f}\n\n{I18N.Message}: {err}";
                        Utils.Misc.Warning(m);
                    }
                });
                r.Add(item);
            }
            return r.ToArray();
        }


        void GenerateShortCutMenu(ContextMenuStrip root)
        {
            var items = root.Items;
            var path = Datas.Consts.ShortcutsFolder;
            if (Directory.Exists(path))
            {
                items.AddRange(WalkThroughDir(path));
            }
        }

        NotifyIcon CreateNotifyIcon()
        {
            var ver = Utils.Misc.GetAssemblyVersion();
            var name = Properties.Resources.AppName;
            var title = $"{name} v{ver}";
            var ni = new NotifyIcon()
            {
                Visible = true,
                Text = title,

                // https://iconarchive.com/show/qetto-2-icons-by-ampeross/shortcut-icon.html
                Icon = Properties.Resources.AppIcon,

                BalloonTipText = title,
                ContextMenuStrip = new ContextMenuStrip(),

            };

            return ni;
        }

        void AppendBasicMenu(ContextMenuStrip root)
        {
            var items = root.Items;
            items.Add(new ToolStripSeparator());

            var menu = new ToolStripMenuItem(I18N.Tools, Properties.Resources.Property_16x, new ToolStripMenuItem[] {
                new ToolStripMenuItem(I18N.OpenShortcutsFolder, Properties.Resources.Shortcut_16x, (s, e) =>
                {
                    OpenShortcutsFolder();
                }),

                new ToolStripMenuItem(I18N.OpenStartupFolder, Properties.Resources.StartRemoteDebugger_16x, (s, e) =>
                {
                    Process.Start(@"shell:startup");
                }),


                new ToolStripMenuItem(I18N.RefreshMenu, Properties.Resources.QuickRefresh_16x, (s, e) =>
                {
                    RefreshMenu();
                }),

                new ToolStripMenuItem("GitHub",null,(s,e)=>{
                    var gh = Properties.Resources.GitHub;
                    var msg = $"{I18N.Open}: {gh}";
                    if (Utils.Misc.Confirm(msg))
                    {
                        Process.Start(gh);
                    }
                }),
            });

            menu.DropDownItems.Add(new ToolStripSeparator());

            menu.DropDownItems.Add(new ToolStripMenuItem(I18N.Exit, Properties.Resources.CloseSolution_16x, (s, e) =>
            {
                if (Utils.Misc.Confirm(I18N.ExitApp))
                {
                    notifyIcon.Icon = null;
                    Application.Exit();
                }
            }));

            items.Add(menu);
        }

        private static void OpenShortcutsFolder()
        {
            var d = Datas.Consts.ShortcutsFolder;
            if (!Directory.Exists(d))
            {
                Directory.CreateDirectory(d);
            }
            Process.Start(d);
        }

        #endregion
    }
}
