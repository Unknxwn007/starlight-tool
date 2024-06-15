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
            FormBorderStyle = FormBorderStyle.FixedSingle; //UI
            pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, 25); //Logo
            ThemeManager(this, panel1, label1); //Light / Dark Mode
            button1.Location = new Point(221, 227);
        }

        private void button1_Click(object sender, System.EventArgs e) { CompleteInstallationProcess(); }

        private void button3_Click(object sender, System.EventArgs e) { Process.Start("notepad.exe", "packages.config"); }
    }
}
