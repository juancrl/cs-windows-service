namespace WatchDogService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WatchDogProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.WatchDogInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // WatchDogProcessInstaller
            // 
            this.WatchDogProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.WatchDogProcessInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WatchDogInstaller});
            this.WatchDogProcessInstaller.Password = null;
            this.WatchDogProcessInstaller.Username = null;
            // 
            // WatchDogInstaller
            // 
            this.WatchDogInstaller.Description = "WatchDogInstaller";
            this.WatchDogInstaller.DisplayName = "WatchDogService";
            this.WatchDogInstaller.ServiceName = "WatchDogService";
            this.WatchDogInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WatchDogProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WatchDogProcessInstaller;
        private System.ServiceProcess.ServiceInstaller WatchDogInstaller;
    }
}