using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;

namespace WatchDogService
{
    partial class WatchDogService : ServiceBase
    {        
        private FSWatcher FSWatcher = null;
        private string[] args = null;
        public WatchDogService()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            if (args.Length < 2)
            {
                args = new string[] { Environment.SystemDirectory.ToString(), Environment.SystemDirectory.ToString() };
            }
            FSWatcher = new FSWatcher(args);
            FSWatcher.RunService();
        }

        protected override void OnStop()
        {
            
        }
    }
}
