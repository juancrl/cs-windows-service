using System.IO;
using System;

namespace WatchDogService
{
    public class FSWatcher
    {
        private FileSystemWatcher InputWatcher { get; set; }
        private FileSystemWatcher OutputWatcher { get; set; }
        private string InputFolder { get; set; }
        private string OutputFolder { get; set; }

        public FSWatcher() { }
        public FSWatcher(string[] args)
        {
            InputWatcher = new FileSystemWatcher(args[0]);
            OutputWatcher = new FileSystemWatcher(args[1]);
            InputFolder = args[0];
            OutputFolder = args[1];
        }


        public void RunService()
        {
            OutputWatcher.NotifyFilter = InputWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName |
                                    NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                                    NotifyFilters.Security | NotifyFilters.Size;

            OutputWatcher.Filter = InputWatcher.Filter = "*.*";

            InputWatcher.Changed += new FileSystemEventHandler(InputWatcher_Changed);
            InputWatcher.Created += new FileSystemEventHandler(InputWatcher_Created);
            InputWatcher.Deleted += new FileSystemEventHandler(InputWatcher_Deleted);
            InputWatcher.Renamed += new RenamedEventHandler(InputWatcher_Renamed);
            InputWatcher.IncludeSubdirectories = true;
            InputWatcher.EnableRaisingEvents = true;

            OutputWatcher.Changed += new FileSystemEventHandler(OutputWatcher_Changed);
            OutputWatcher.Created += new FileSystemEventHandler(OutputWatcher_Created);
            OutputWatcher.Deleted += new FileSystemEventHandler(OutputWatcher_Deleted);
            OutputWatcher.Renamed += new RenamedEventHandler(OutputWatcher_Renamed);
            OutputWatcher.IncludeSubdirectories = true;
            OutputWatcher.EnableRaisingEvents = true;
        }

        private void ProcessResult(int FolderType, FileSystemEventArgs args, RenamedEventArgs rargs)
        {
            var type = FolderType.Equals(1) ? "Input : " : "Output : ";
            if (rargs != null)
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\WatchDog.txt", Environment.NewLine + type + args.ChangeType + ":" + rargs.FullPath);
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\WatchDog.txt", Environment.NewLine + type + args.ChangeType + ":" + rargs.OldFullPath);
            }
            else
            {
                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\WatchDog.txt", Environment.NewLine + type + args.ChangeType + ":" + args.FullPath);
            }
        }

        private void InputWatcher_Changed(object e, FileSystemEventArgs args)
        {
            ProcessResult(1, args, null);
        }

        private void InputWatcher_Created(object e, FileSystemEventArgs args)
        {
            ProcessResult(1, args, null);
        }

        private void InputWatcher_Deleted(object e, FileSystemEventArgs args)
        {
            ProcessResult(1, args, null);
        }

        private void InputWatcher_Error(object e, ErrorEventArgs args)
        {
            return;
        }

        private void InputWatcher_Renamed(object e, RenamedEventArgs args)
        {
            ProcessResult(1, null, args);
        }

        private void OutputWatcher_Changed(object e, FileSystemEventArgs args)
        {
            ProcessResult(2, args, null);
        }

        private void OutputWatcher_Created(object e, FileSystemEventArgs args)
        {
            ProcessResult(2, args, null);
        }

        private void OutputWatcher_Deleted(object e, FileSystemEventArgs args)
        {
            ProcessResult(2, args, null);
        }

        private void OutputWatcher_Error(object e, ErrorEventArgs args)
        {
            return;
        }

        private void OutputWatcher_Renamed(object e, RenamedEventArgs args)
        {
            ProcessResult(2, null, args);
        }
    }
}