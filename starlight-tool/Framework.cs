using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

            if (process.ExitCode != 0) { throw new Exception($"PowerShell command '{command}' failed with exit code {process.ExitCode}"); }
        }

        public static bool IsChocolateyInstalled()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "choco",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "-v"
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return !string.IsNullOrWhiteSpace(output);
        }

        public static void RunChecks()
        {
            if (!IsChocolateyInstalled()) { DialogResult result = MessageBox.Show("Chocolatey not installed, exiting..", "Error", MessageBoxButtons.OK); if (result == DialogResult.OK) { Environment.Exit(0); } }
        }

        public static void CompleteInstallationProcess()
        {
            //RegistryCheck.CheckRegistryKeys();
            #region Fix Small Bugs
            // Fixes spinning icon not showing when shutting down or restarting BUT also makes start not show up instantly
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
            //if (key != null) { key.SetValue("UserPreferencesMask", new byte[] { 0x90, 0x12, 0x03, 0x80, 0x10, 0x00, 0x00, 0x00 }, RegistryValueKind.Binary); key.Close(); }
            #endregion
            #region Quality of Life
            // Bluetooth tray icon
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Bluetooth", true);
            if (key != null) { key.SetValue("Notification Area Icon", 0, RegistryValueKind.DWord); key.Close(); }
            #endregion
        }
    }
}