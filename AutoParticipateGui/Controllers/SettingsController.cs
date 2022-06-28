using System;
using System.Diagnostics;
using System.IO;
using AutoParticipateGui.Services;
using AutoParticipateGui.Types;
using AutoParticipateGui.Views.Pages;

namespace AutoParticipateGui.Controllers
{
    public class SettingsController
    {
        #region Account

        public static string Cookie
        {
            get
            {
                try
                {
                    var data = RegistryService.GetLocalStringValue(nameof(Cookie));
                    return EncryptionService.Decrypt(data);
                }
                catch {}

                return "";
            }

            set
            {
                try
                {
                    var data = EncryptionService.Encrypt(value);
                    RegistryService.SetLocalValue(nameof(Cookie), data);
                }
                catch {}
            }
        }

        #endregion

        #region Notifications
        
        public static bool UseNotifications
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalBoolValue(nameof(UseNotifications));
                }
                catch {}

                return false;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(UseNotifications), value);
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
                    var data = RegistryService.GetLocalStringValue(nameof(TelegramBotToken));
                    return EncryptionService.Decrypt(data);
                }
                catch {}

                return "";
            }

            set
            {
                try
                {
                    var data = EncryptionService.Encrypt(value);
                    RegistryService.SetLocalValue(nameof(TelegramBotToken), data);
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
                    return RegistryService.GetLocalLongValue(nameof(TelegramUserId));
                }
                catch {}

                return 0;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(TelegramUserId), value);
                }
                catch {}
            }
        }

        #endregion

        #region Proxy
        
        public static bool UseProxy
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalBoolValue(nameof(UseProxy));
                }
                catch {}

                return false;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(UseProxy), value);
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
                    var data = RegistryService.GetLocalStringValue(nameof(ProxyLine));
                    return EncryptionService.Decrypt(data);
                }
                catch {}

                return "";
            }

            set
            {
                try
                {
                    var data = EncryptionService.Encrypt(value);
                    RegistryService.SetLocalValue(nameof(ProxyLine), data);
                }
                catch {}
            }
        }

        #endregion

        #region Timeouts

        public static int ParticipationTimeout
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalIntValue(nameof(ParticipationTimeout));
                }
                catch {}

                return 1;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(ParticipationTimeout), value);
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
                    return RegistryService.GetLocalIntValue(nameof(ThreadTimeout));
                }
                catch {}

                return 100;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(ThreadTimeout), value);
                }
                catch {}
            }
        }
        
        public static int VerificationFailTimeout
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalIntValue(nameof(VerificationFailTimeout));
                }
                catch {}

                return 120;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(VerificationFailTimeout), value);
                }
                catch {}
            }
        }
        
        public static int MaxLoadingPageTimeout
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalIntValue(nameof(MaxLoadingPageTimeout));
                }
                catch {}

                return 120;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(MaxLoadingPageTimeout), value);
                }
                catch {}
            }
        }
        
        public static bool WaitFullPageLoad
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalBoolValue(nameof(WaitFullPageLoad));
                }
                catch {}

                return false;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(WaitFullPageLoad), value);
                }
                catch {}
            }
        }

        #endregion

        #region Browser

        public static bool LoadImages
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalBoolValue(nameof(LoadImages));
                }
                catch {}

                return false;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(LoadImages), value);
                }
                catch {}
            }
        }
        
        public static ClickType ClickType
        {
            get
            {
                try
                {
                    return (ClickType)RegistryService.GetLocalIntValue(nameof(ClickType));
                }
                catch {}

                return ClickType.Element;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(ClickType), (int)value);
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
                    return RegistryService.GetLocalStringValue(nameof(UserAgent));
                }
                catch {}

                return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36";
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(UserAgent), value);
                }
                catch {}
            }
        }
        
        public static string WebGlVendor
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalStringValue(nameof(WebGlVendor));
                }
                catch {}

                return "Google Inc. (Intel)";
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(WebGlVendor), value);
                }
                catch {}
            }
        }
        
        public static string WebGlRenderer
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalStringValue(nameof(WebGlRenderer));
                }
                catch {}

                return "ANGLE (Intel, Intel(R) Iris(R) Xe Graphics Direct3D11 vs_5_0 ps_5_0, D3D11)";
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(WebGlRenderer), value);
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
                    return RegistryService.GetLocalStringValue(nameof(OsPlatform));
                }
                catch {}

                return "Win32";
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(OsPlatform), value);
                }
                catch {}
            }
        }

        #endregion

        #region Other
        
        public static string OffTime
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalStringValue(nameof(OffTime));
                }
                catch {}

                return "";
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(OffTime), value);
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
                    return RegistryService.GetLocalBoolValue(nameof(UseLiker));
                }
                catch {}

                return false;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(UseLiker), value);
                }
                catch {}
            }
        }
        
        public static int LikeChance
        {
            get
            {
                try
                {
                    return RegistryService.GetLocalIntValue(nameof(LikeChance));
                }
                catch {}

                return 0;
            }

            set
            {
                try
                {
                    RegistryService.SetLocalValue(nameof(LikeChance), value);
                }
                catch {}
            }
        }

        #endregion
        
        public static bool IsChromeBrowserInstalled
        {
            get
            {
                try
                {
                    return RegistryService.IsChromeBrowserInstalled();
                }
                catch {}

                return false;
            }
        }

        public static string PythonExecutablePath
        {
            get
            {
                try
                {
                    var path = RegistryService.GetPythonExecutablePath();
                    if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path) || File.Exists(path) == false)
                    {
                        throw new Exception("Python not found");
                    }

                    return path;
                }
                catch {}

                return "";
            }
        }
        
        public static bool IsPythonInstalled
        {
            get
            {
                try
                {
                    var fileName = PythonExecutablePath;
                    return string.IsNullOrEmpty(fileName) == false;
                }
                catch {}

                return false;
            }
        }
    }
}