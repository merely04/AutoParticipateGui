using System;
using System.Linq;
using Microsoft.Win32;

namespace AutoParticipateGui.Services
{
    public class RegistryService
    {
        private const string MainPath = @"Software\AutoParticipate";
        private const string PythonPath = @"Software\Python\PythonCore\3.7\InstallPath";

        static RegistryService()
        {
            
        }

        private static RegistryKey MainKey => Registry.CurrentUser.CreateSubKey(MainPath);

        private static RegistryKey PythonKey = Registry.CurrentUser.OpenSubKey(PythonPath);

        public static bool IsChromeBrowserInstalled
        {
            get
            {
                try
                {
                    var browsersKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet") ??
                                      Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                    if (browsersKey != null)
                    {
                        var browserNames = browsersKey?.GetSubKeyNames().ToList();
                        if (browserNames.Count > 0 && browserNames.Contains("Google Chrome"))
                        {
                            return true;
                        }
                    }
                }
                catch {}

                return default;
            }
        }
        
        public static string PythonExecutablePath
        {
            get
            {
                try
                {
                    if (PythonKey != null)
                    {
                        var value = PythonKey.GetValue("ExecutablePath").ToString();
                        return value;
                    }
                }
                catch {}

                return default;
            }
        }

        public static bool UseNotification
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(UseNotification)).ToString();
                    if (bool.TryParse(value, out var boolValue))
                    {
                        return boolValue;
                    }
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(UseNotification), value);
                }
                catch {}
            }
        }
        
        public static string TelegramBotToken
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(TelegramBotToken)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(TelegramBotToken), value);
                }
                catch {}
            }
        }
        
        public static long TelegramUserId
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(TelegramUserId)).ToString();
                    if (long.TryParse(value, out var longValue))
                    {
                        return longValue;
                    }
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(TelegramUserId), value);
                }
                catch {}
            }
        }
        
        public static bool UseLiker
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(UseLiker)).ToString();
                    if (bool.TryParse(value, out var boolValue))
                    {
                        return boolValue;
                    }
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(UseLiker), value);
                }
                catch {}
            }
        }
        
        public static bool UseProxy
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(UseProxy)).ToString();
                    if (bool.TryParse(value, out var boolValue))
                    {
                        return boolValue;
                    }
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(UseProxy), value);
                }
                catch {}
            }
        }
        
        public static string ProxyLine
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(ProxyLine)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(ProxyLine), value);
                }
                catch {}
            }
        }
        
        public static string ProxyFileName
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(ProxyFileName)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(ProxyFileName), value);
                }
                catch {}
            }
        }
        
        public static string Cookie
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(Cookie)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(Cookie), value);
                }
                catch {}
            }
        }
        
        public static string UserAgent
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(UserAgent)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(UserAgent), value);
                }
                catch {}
            }
        }
        
        public static string BrowserVendor
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(BrowserVendor)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(BrowserVendor), value);
                }
                catch {}
            }
        }
        
        public static string OsRenderer
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(OsRenderer)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(OsRenderer), value);
                }
                catch {}
            }
        }
        
        public static string OsPlatform
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(OsPlatform)).ToString();
                    return value;
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(OsPlatform), value);
                }
                catch {}
            }
        }
        
        public static int Timeout
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(Timeout)).ToString();
                    if (int.TryParse(value, out var intValue))
                    {
                        return intValue;
                    }
                }
                catch {}

                return 10;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(Timeout), value);
                }
                catch {}
            }
        }
        
        public static int ThreadTimeout
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(ThreadTimeout)).ToString();
                    if (int.TryParse(value, out var intValue))
                    {
                        return intValue;
                    }
                }
                catch {}

                return 100;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(ThreadTimeout), value);
                }
                catch {}
            }
        }
        
        public static int LoadPageTimeout
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(LoadPageTimeout)).ToString();
                    if (int.TryParse(value, out var intValue))
                    {
                        return intValue;
                    }
                }
                catch {}

                return 120;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(LoadPageTimeout), value);
                }
                catch {}
            }
        }
        
        public static bool WaitPage
        {
            get
            {
                try
                {
                    var value = MainKey.GetValue(nameof(WaitPage)).ToString();
                    if (bool.TryParse(value, out var boolValue))
                    {
                        return boolValue;
                    }
                }
                catch {}

                return default;
            }

            set
            {
                try
                {
                    MainKey.SetValue(nameof(WaitPage), value);
                }
                catch {}
            }
        }
    }
}