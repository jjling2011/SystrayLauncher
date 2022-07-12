using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystrayLauncher
{
    internal class Launcher : IDisposable
    {
        bool isDisposed = false;
        List<IDisposable> services = new List<IDisposable>();

        public Launcher()
        {

        }

        #region public methods
        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
            foreach (var service in services)
            {
                service?.Dispose();
            }
        }

        public void Init()
        {
            var nis = new Services.NotifyIconSrv();

            services.Add(nis);

            nis.Init();
        }


        #endregion


    }
}
