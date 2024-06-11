using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Management.Automation;
using System.IO;

namespace Starlight_Tool
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

        public static void RunPSCode(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };
            process.Start();
            process.WaitForExit();
        }

        public static bool IsChocolateyInstalled()
        {
            return Directory.Exists(@"C:\ProgramData\chocolatey");
        }

        public static bool DoesPackagesFileExist()
        {
            return File.Exists("packages.config");
        }

        public static void RunChecks()
        {
            var installCommand = @"-ExecutionPolicy Bypass -Command ""iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))""";

            //Chocolately 
            if (!IsChocolateyInstalled())
            {
                MessageBox.Show("Chocolately has not been found! Press OK to install", "Requirements have not been met");
                RunPSCode(installCommand);
            }
            //Files
            if (!DoesPackagesFileExist())
            {
                //Download empty packages file
            }
        }
    }
}
