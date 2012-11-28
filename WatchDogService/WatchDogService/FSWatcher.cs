using System.IO;

namespace WatchDogService
{
    public class FSWatcher
    {
        public FSWatcher() { }

        public FSWatcher(FileSystemWatcher FSW) { Watcher = FSW; }

        private FileSystemWatcher Watcher { get; set; }

        public void RunService()
        {
            FileSystemWatcher watcher = new FileSystemWatcher("D:/Practice");
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                                    NotifyFilters.Security | NotifyFilters.Size;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            watcher.Created += new FileSystemEventHandler(Watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(Watcher_Deleted);
            watcher.Renamed += new RenamedEventHandler(Watcher_Renamed);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Changed(object e, FileSystemEventArgs args)
        {
            File.AppendAllText(@"D:\sample.txt", "\r\f" + args.ChangeType + ":" + args.FullPath);
        }

        private void Watcher_Created(object e, FileSystemEventArgs args)
        {
            File.AppendAllText(@"D:\sample.txt", "\r\f" + args.ChangeType + ":" + args.FullPath);
        }

        private void Watcher_Deleted(object e, FileSystemEventArgs args)
        {
            File.AppendAllText(@"D:\sample.txt", "\r\f" + args.ChangeType + ":" + args.FullPath);
        }

        private void Watcher_Error(object e, ErrorEventArgs args)
        {
            return;
        }

        private void Watcher_Renamed(object e, RenamedEventArgs args)
        {
            File.AppendAllText(@"D:\sample.txt", "\r\f" + args.ChangeType + ":" + args.FullPath);
            File.AppendAllText(@"D:\sample.txt", "\r\f" + args.ChangeType + ":" + args.OldFullPath);
        }
    }
}