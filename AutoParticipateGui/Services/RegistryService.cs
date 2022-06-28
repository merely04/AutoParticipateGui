using System;
using System.Linq;
using Microsoft.Win32;

namespace AutoParticipateGui.Services
{
    public class RegistryService
    {
        private const string LocalPath = @"Software\AutoParticipate";
        private const string PythonPath = @"Software\Python\PythonCore\3.7\InstallPath";
        
        private static RegistryKey LocalKey => Registry.CurrentUser.CreateSubKey(LocalPath);

        public static string GetLocalStringValue(string parameterName)
        {
            using (var key = LocalKey)
            {
                return key.GetValue(parameterName).ToString();   
            }
        }

        public static int GetLocalIntValue(string parameterName)
        {
            using (var key = LocalKey)
            {
                return int.Parse(key.GetValue(parameterName).ToString());
            }
        }
        
        public static long GetLocalLongValue(string parameterName)
        {
            using (var key = LocalKey)
            {
                return long.Parse(key.GetValue(parameterName).ToString());
            }
        }
        
        public static bool GetLocalBoolValue(string parameterName)
        {
            using (var key = LocalKey)
            {
                return bool.Parse(key.GetValue(parameterName).ToString());
            }
        }
        
        public static void SetLocalValue(string parameterName, object value)
        {
            using (var key = LocalKey)
            {
                key.SetValue(parameterName, value);
            }
        }
        
        public static bool IsChromeBrowserInstalled()
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet") ??
                                     Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet"))
            {
                var browsers = key?.GetSubKeyNames().ToList();
                return key is not null && browsers.Count > 0 && browsers.Contains("Google Chrome");
            }
        }
        
        public static string GetPythonExecutablePath()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(PythonPath))
            {
                return key?.GetValue("ExecutablePath").ToString();   
            }
        }
    }
}