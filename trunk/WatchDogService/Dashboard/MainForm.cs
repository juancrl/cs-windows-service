using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent(); serviceController1.ServiceName = System.Configuration.ConfigurationManager.AppSettings["SERVICE_NAME"].ToString();
        }

        public void UpdateStatus()
        {
            var status = serviceController1.Status.ToString();
            if (status.Equals("Running"))
            {
                btnStop.Visible = true;
                btnRestart.Visible = true; btnStart.Visible = false;
                toolStripStatusLabel2.ForeColor = label2.ForeColor = Color.Green;
            }
            else if (status.Equals("Stopped"))
            {
                btnStop.Visible = false;
                btnRestart.Visible = false;
                btnStart.Visible = true;
                toolStripStatusLabel2.ForeColor = label2.ForeColor = Color.Maroon;
            }
            toolStripStatusLabel2.Text = label2.Text = status;
            label5.Text = System.Configuration.ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString();
            label6.Text = System.Configuration.ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString();
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
            btnStop.Enabled = false; btnStop.Click += new EventHandler(btnStop_Click); btnStart.Click += new EventHandler(btnStart_Click); btnStop.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try { serviceController1.Start(); serviceController1.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running); UpdateStatus(); }
            catch (Exception ex) { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try { serviceController1.Stop(); serviceController1.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped); UpdateStatus(); }
            catch (Exception ex) { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(); settings.ShowDialog(this); UpdateStatus(); this.Refresh();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help...!", "", MessageBoxButtons.OK);
        }
    }
}