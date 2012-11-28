using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Dashboard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            serviceController1.ServiceName = System.Configuration.ConfigurationManager.AppSettings["SERVICE_NAME"].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void WatchFile()
        {
            FileSystemWatcher fsw = new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            fsw.Filter = @"\WatchDog.txt";
            fsw.NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fsw.Changed +=new FileSystemEventHandler(fsw_Changed);
        }

        private void fsw_Changed(object e, FileSystemEventArgs args)
        {
            //richTextBox1.Text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\WatchDog.txt");
            //richTextBox1.Refresh();            
        }

        public void UpdateStatus()
        {
            try
            {
                var status = serviceController1.Status.ToString();
                if (status.Equals("Running"))
                {
                    btnStop.Visible = true;
                    btnRestart.Visible = true; btnStart.Visible = false;
                    toolStripStatusLabel4.ForeColor = label2.ForeColor = Color.Green;
                }
                else if (status.Equals("Stopped"))
                {
                    btnStop.Visible = false;
                    btnRestart.Visible = false;
                    btnStart.Visible = true;
                    toolStripStatusLabel4.ForeColor = label2.ForeColor = Color.Maroon;
                }                
                toolStripStatusLabel4.Text = label2.Text = status;

                if (System.Configuration.ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString() == "" ||
                    System.Configuration.ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString() == "")
                {
                    MessageBox.Show("Please choose Service Folders...!");
                    showSettingsWindows();
                }
                else if(!Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString()) ||
                        !Directory.Exists(System.Configuration.ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString()))
                {
                    MessageBox.Show("Please Service Folders does not exists...!");
                    showSettingsWindows();
                }
                else
                {
                    label5.Text = System.Configuration.ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString();
                    label6.Text = System.Configuration.ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString();
                }
                WatchFile();
            }
            catch (Exception ex)
            {
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 abt = new AboutBox1(); abt.ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStop.Enabled = false;
                btnStop.Click += new EventHandler(btnStop_Click);
                btnStart.Click += new EventHandler(btnStart_Click);
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string[] args = { System.Configuration.ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString(), 
                                    System.Configuration.ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString() };
                serviceController1.Start(args);
                serviceController1.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                UpdateStatus();
            }
            catch (Exception ex) {
                MessageBox.Show("Error Occured in Starting the Service...!\n" + ex.Message);
                this.Close();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                serviceController1.Stop();
                serviceController1.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped); 
                UpdateStatus();
            }
            catch (Exception ex) { }
        }

       

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSettingsWindows();
        }

        private void showSettingsWindows()
        {
            try
            {
                SettingsForm settings = new SettingsForm();
                settings.ShowDialog(this);
                UpdateStatus();
                this.Refresh();
            }
            catch (Exception ex) { }
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help...!", "", MessageBoxButtons.OK);
        }
    }
}