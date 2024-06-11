using Starlight_Tool;
using System.IO;
using static Starlight_Tool.Framework;

/*
 *                                                          Information:
 *                                                          
 *     Requires Administrator rights because; it has a check to see if chocolately is installed and then downloads it, which requires administrator rights     
*/

namespace src
{
    public partial class App : Form
    {
        private TabControl tabControlAdv1;

        public App()
        {
            InitializeComponent();
            RunChecks(); //Speaks for itself
            FormBorderStyle = FormBorderStyle.FixedSingle; //UI
            pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, 25); //Logo
            ThemeManager(this, panel1, label1); //Light / Dark Mode
        }

// Install Apps
        private void button1_Click(object sender, EventArgs e)
        {
            RunPSCode("choco install .\\packages.config --confirm --ignore-checksums");
        }

// Edit Applist
        private void button3_Click(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream("packages.config", FileMode.Open);
        }
    }
}
