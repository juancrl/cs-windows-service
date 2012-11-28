using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Dashboard
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            txtInputPath.Text=ConfigurationManager.AppSettings["INPUT_FOLDER"].ToString();
            txtOutputPath.Text = ConfigurationManager.AppSettings["OUTPUT_FOLDER"].ToString();
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtInputPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtOutputPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnBrowseSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtInputPath.Text.Equals("") && !txtOutputPath.Text.Equals(""))
                {
                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["INPUT_FOLDER"].Value = txtInputPath.Text;
                    config.AppSettings.Settings["OUTPUT_FOLDER"].Value = txtOutputPath.Text;
                    config.Save(ConfigurationSaveMode.Minimal, true);
                    ConfigurationManager.RefreshSection("appSettings");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Choose the folder paths...!", "", MessageBoxButtons.OK);
                }

            }
            catch(Exception ex)
            {            
            }
        }
    }
}
