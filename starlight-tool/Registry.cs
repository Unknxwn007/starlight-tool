using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;

namespace starlight_tool
{
    public class RegistryCheck
    {
        public enum RegistryHive
        {
            CurrentUser,
            LocalMachine,
            ClassesRoot,
            CurrentConfig,
            Users
        }

        public class RegistryKeyInfo
        {
            public RegistryHive Hive { get; set; }
            public string KeyName { get; set; }
            public string KeyValue { get; set; }
            public RegistryValueKind KeyType { get; set; }
            public object ExpectedValue { get; set; }

            public RegistryKeyInfo(RegistryHive hive, string keyName, string keyValue, RegistryValueKind keyType, object expectedValue)
            {
                Hive = hive;
                KeyName = keyName;
                KeyValue = keyValue;
                KeyType = keyType;
                ExpectedValue = expectedValue;
            }
        }

        public static List<RegistryKeyInfo> RegistryKeys { get; set; }

        static RegistryCheck()
        {
            RegistryKeyCheck();
        }

        public static void RegistryKeyCheck()
        {
            RegistryKeys = new List<RegistryKeyInfo>
            {
                new RegistryKeyInfo(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\WindowsRuntime\ActivatableClassId\Windows.Gaming.GameBar.PresenceServer.Internal.PresenceWriter", "ActivationType", RegistryValueKind.DWord, 0),
            };
        }

        public static void CheckRegistryKeys()
        {
            foreach (var keyInfo in RegistryKeys)
            {
                RegistryKey registryKey;
                switch (keyInfo.Hive)
                {
                    case RegistryHive.CurrentUser:
                        registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
                        break;
                    case RegistryHive.LocalMachine:
                        registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                        break;
                    case RegistryHive.ClassesRoot:
                        registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.ClassesRoot, RegistryView.Registry64);
                        break;
                    case RegistryHive.CurrentConfig:
                        registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentConfig, RegistryView.Registry64);
                        break;
                    case RegistryHive.Users:
                        registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.Users, RegistryView.Registry64);
                        break;
                    default:
                        throw new Exception("Invalid registry hive");
                }

                using (RegistryKey key = registryKey.OpenSubKey(keyInfo.KeyName, true))
                {
                    if (key != null)
                    {
                        object keyValue = key.GetValue(keyInfo.KeyValue);
                        if (keyValue != null)
                        {
                            if (keyInfo.KeyType == RegistryValueKind.String)
                            {
                                string stringValue = (string)keyValue;
                                if (stringValue != (string)keyInfo.ExpectedValue) { File.AppendAllText("ERROR.LOG", $"Key {keyInfo.KeyName} has value {stringValue}, expected {keyInfo.ExpectedValue}\r\n"); }
                            }
                            else if (keyInfo.KeyType == RegistryValueKind.DWord)
                            {
                                int intValue = (int)keyValue;
                                if (intValue != (int)keyInfo.ExpectedValue) { File.AppendAllText("ERROR.LOG", $"Key {keyInfo.KeyName} has value {intValue}, expected {keyInfo.ExpectedValue}\r\n"); }
                            }
                        }
                        else { File.AppendAllText("ERROR.LOG", $"Key {keyInfo.KeyName} does not exist\r\n"); }
                    }
                    else { File.AppendAllText("ERROR.LOG", $"Key {keyInfo.KeyName} does not exist\r\n"); }
                }
            }
        }
    }
}