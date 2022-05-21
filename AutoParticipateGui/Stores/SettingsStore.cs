using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using AutoParticipateGui.Annotations;
using AutoParticipateGui.Services;
using Newtonsoft.Json;

namespace AutoParticipateGui.Stores
{
    public class SettingsStore: INotifyPropertyChanged
    {
        private bool _useLiker;
        
        private bool _useNotification;
        private string _telegramBotToken;
        private long _telegramUserId;
        
        private bool _useProxy;
        private string _proxyLine;
        private string _proxyFileName;

        private string _cookie;
        private string _userAgent;
        private string _browserVendor;
        private string _osRenderer;
        private string _osPlatform;

        private int _timeout;
        private int _threadTimeout;
        private int _loadPageTimeout;
        private bool _waitPage;
        
        public SettingsStore()
        {
            _useLiker = RegistryService.UseLiker;
            
            _useNotification = RegistryService.UseNotification;
            _telegramBotToken = RegistryService.TelegramBotToken;
            _telegramUserId = RegistryService.TelegramUserId;
            
            _useProxy = RegistryService.UseProxy;
            _proxyLine = RegistryService.ProxyLine;
            _proxyFileName = RegistryService.ProxyFileName;

            _cookie = RegistryService.Cookie;
            _userAgent = RegistryService.UserAgent;
            _browserVendor = RegistryService.BrowserVendor;
            _osRenderer = RegistryService.OsRenderer;
            _osPlatform = RegistryService.OsPlatform;

            _timeout = RegistryService.Timeout;
            _threadTimeout = RegistryService.ThreadTimeout;
            _loadPageTimeout = RegistryService.LoadPageTimeout;
            _waitPage = RegistryService.WaitPage;
        }

        public bool UseLiker
        {
            get => _useLiker;
            set
            {
                if (value == _useLiker) return;
                _useLiker = value;
                OnPropertyChanged(nameof(UseLiker));
                
                RegistryService.UseLiker = value;
            }
        }

        public bool UseNotification
        {
            get => _useNotification;
            set
            {
                if (value == _useNotification) return;
                _useNotification = value;
                OnPropertyChanged(nameof(UseNotification));

                RegistryService.UseNotification = value;
            }
        }

        public string TelegramBotToken
        {
            get => GetValueOrDefault(_telegramBotToken, "");
            set
            {
                if (value == _telegramBotToken) return;
                _telegramBotToken = value;
                OnPropertyChanged(nameof(TelegramBotToken));
                
                RegistryService.TelegramBotToken = value;
            }
        }

        public long TelegramUserId
        {
            get => _telegramUserId;
            set
            {
                if (value == _telegramUserId) return;
                _telegramUserId = value;
                OnPropertyChanged(nameof(TelegramUserId));
                
                RegistryService.TelegramUserId = value;
            }
        }

        public bool UseProxy
        {
            get => _useProxy;
            set
            {
                if (value == _useProxy) return;
                _useProxy = value;
                OnPropertyChanged(nameof(UseProxy));
                
                RegistryService.UseProxy = value;
            }
        }

        public string ProxyLine
        {
            get => _proxyLine;
            set
            {
                if (value == _proxyLine) return;
                _proxyLine = value;
                OnPropertyChanged(nameof(ProxyLine));
                
                RegistryService.ProxyLine = value;
            }
        }

        public string ProxyFileName
        {
            get => _proxyFileName;
            set
            {
                if (value == _proxyFileName) return;
                _proxyFileName = value;
                OnPropertyChanged(nameof(ProxyFileName));
                
                RegistryService.ProxyFileName = value;
            }
        }

        public string Cookie
        {
            get => GetValueOrDefault(_cookie, "");
            set
            {
                if (value == _cookie) return;
                _cookie = value;
                OnPropertyChanged(nameof(Cookie));
                
                RegistryService.Cookie = value;
            }
        }

        public string UserAgent
        {
            get => GetValueOrDefault(_userAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36");
            set
            {
                if (value == _userAgent) return;
                _userAgent = value;
                OnPropertyChanged(nameof(UserAgent));
                
                RegistryService.UserAgent = value;
            }
        }

        public string BrowserVendor
        {
            get => GetValueOrDefault(_browserVendor, "Google Inc. (Intel)");
            set
            {
                if (value == _browserVendor) return;
                _browserVendor = value;
                OnPropertyChanged(nameof(BrowserVendor));
                
                RegistryService.BrowserVendor = value;
            }
        }

        public string OsRenderer
        {
            get => GetValueOrDefault(_osRenderer, "ANGLE (Intel, Intel(R) Iris(R) Xe Graphics Direct3D11 vs_5_0 ps_5_0, D3D11)");
            set
            {
                if (value == _osRenderer) return;
                _osRenderer = value;
                OnPropertyChanged(nameof(OsRenderer));
                
                RegistryService.OsRenderer = value;
            }
        }

        public string OsPlatform
        {
            get => GetValueOrDefault(_osPlatform, "Win32");
            set
            {
                if (value == _osPlatform) return;
                _osPlatform = value;
                OnPropertyChanged(nameof(OsPlatform));
                
                RegistryService.OsPlatform = value;
            }
        }

        public int Timeout
        {
            get => _timeout;
            set
            {
                if (value == _timeout) return;
                _timeout = value;
                OnPropertyChanged(nameof(Timeout));
                
                RegistryService.Timeout = value;
            }
        }

        public int ThreadTimeout
        {
            get => _threadTimeout;
            set
            {
                if (value == _threadTimeout) return;
                _threadTimeout = value;
                OnPropertyChanged(nameof(ThreadTimeout));
                
                RegistryService.ThreadTimeout = value;
            }
        }
        
        public int LoadPageTimeout
        {
            get => _loadPageTimeout;
            set
            {
                if (value == _loadPageTimeout) return;
                _loadPageTimeout = value;
                OnPropertyChanged(nameof(LoadPageTimeout));
                
                RegistryService.LoadPageTimeout = value;
            }
        }
        
        public bool WaitPage
        {
            get => _waitPage;
            set
            {
                if (value == _waitPage) return;
                _waitPage = value;
                OnPropertyChanged(nameof(WaitPage));
                
                RegistryService.WaitPage = value;
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                account_info = new
                {
                    cookies = Cookie?.Trim()
                },
                bot_info = new
                {
                    bot_token = TelegramBotToken?.Trim(),
                    user_id = TelegramUserId,
                    logs = UseNotification ? 1 : 0
                },
                likes = new
                {
                    like_friends = UseLiker ? 1 : 0
                },
                other = new
                {
                    timeout = Timeout,
                    thread_timeout =ThreadTimeout,
                    loading_timeout = LoadPageTimeout,
                    wait_page = WaitPage ? "normal" : "eager",
                    use_proxy = UseProxy ? 1 : 0,
                    proxy = ProxyLine?.Trim(),
                    proxy_file = ProxyFileName?.Trim(),
                    webdriver_settings = new
                    {
                        useragent = UserAgent?.Trim(),
                        vendor = BrowserVendor?.Trim(),
                        renderer = OsRenderer?.Trim(),
                        platform = OsPlatform?.Trim()
                    }
                }
            });
        }

        protected virtual string GetValueOrDefault(string value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return defaultValue;
            return value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}