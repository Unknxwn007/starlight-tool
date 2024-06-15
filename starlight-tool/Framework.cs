using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace starlight_tool
{
    public class Framework
    {
        public static void ThemeManager(Form form, Control control, Control control2)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            object value = key.GetValue("AppsUseLightTheme");
            bool isDarkModeEnabled = value != null && (int)value == 0;

            if (isDarkModeEnabled)
            {
                form.BackColor = Color.FromArgb(32, 32, 32);
                control.BackColor = Color.FromArgb(24, 24, 24);
                control2.ForeColor = Color.FromArgb(200, 200, 200);
            }
            else
            {
                form.BackColor = Color.FromArgb(200, 200, 200);
                control.BackColor = Color.FromArgb(175, 175, 175);
                control2.ForeColor = Color.FromArgb(24, 24, 24);
            }
        }

        public static void RunPSCode(string command, bool noWindow)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = noWindow
                }
            };

            process.Start();
            process.WaitForExit();
        }

        public static void CompleteInstallationProcess()
        {
            // Remove Onedrive
            RunPSCode("Get-AppxPackage⁣ Microsoft. OneDrive | Remove-AppxPackage.", true);
        }
    }
}
