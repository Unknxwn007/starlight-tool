using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static starlight_tool.Framework;

namespace starlight_tool
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            RunChecks();
            FormBorderStyle = FormBorderStyle.FixedSingle; //UI
            pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, 25); //Logo
            ThemeManager(this, panel1, label1); //Light / Dark Mode    
            //button3.Enabled = false; //Disable Installation of more Apps until Install has been finalized
            #region dev stuff
            //Move button1 to middle and hide button 3
            //button1.Location = new Point(221, 227);
            button3.Visible = true;
            #endregion
        }

        private void button1_Click(object sender, System.EventArgs e) { CompleteInstallationProcess(); } // button3.Enabled = true; }

        private void button3_Click(object sender, System.EventArgs e) 
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Configuration files (*.config)|*.config";
            openFileDialog.Title = "Select the packages.config file to get started";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                Process process = new Process();
                process.StartInfo.FileName = "notepad.exe";
                process.StartInfo.Arguments = filePath;
                process.Start();
                process.WaitForExit();

                Text = Globalz.AppName + " - Installing packages..";

                try { RunPSCode("choco install packages.config --confirm", true); }
                catch (Exception ex) { MessageBox.Show("an error occured, check the log for more details.", "ERROR"); File.AppendAllText("ERROR.LOG", ex.Message); }

                //MessageBox.Show($"File '{filePath}' opened for editing.");
                Text = Globalz.AppName;
            }*/
            Process.Start("chocolateygui");
        }
    }
}