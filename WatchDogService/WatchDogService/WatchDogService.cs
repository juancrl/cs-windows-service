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
        private FileSystemWatcher Watcher = null;
        private FSWatcher FSWatcher = null;
        public WatchDogService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Watcher = new FileSystemWatcher(@"D:\Practice");
            FSWatcher = new FSWatcher(Watcher);            
            FSWatcher.RunService();
        }

        protected override void OnStop()
        {
            
        }
    }
}
