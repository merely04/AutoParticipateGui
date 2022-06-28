using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using AutoParticipateGui.Annotations;
using AutoParticipateGui.Controllers;
using AutoParticipateGui.Types;
using Newtonsoft.Json;

namespace AutoParticipateGui.Models
{
    public class Settings : INotifyPropertyChanged
    {
        private string _cookie;

        private bool _useNotifications;
        private string _telegramBotToken;
        private long _telegramUserId;
        
        private bool _useProxy;
        private string _proxyLine;

        private int _participationTimeout;
        private int _threadTimeout;
        private int _verificationFailTimeout;
        private bool _waitFullPageLoad;
        private int _maxLoadingPageTimeout;

        private bool _loadImages;
        private ClickType _clickType;
        private string _userAgent;
        private string _webGlVendor;
        private string _webGlRenderer;
        private string _osPlatform;

        private string _offTime;
        private bool _useLiker;
        private int _likeChance;

        public Settings()
        {
            _cookie = SettingsController.Cookie;

            _useNotifications = SettingsController.UseNotifications;
            _telegramBotToken = SettingsController.TelegramBotToken;
            _telegramUserId = SettingsController.TelegramUserId;

            _useProxy = SettingsController.UseProxy;
            _proxyLine = SettingsController.ProxyLine;

            _participationTimeout = SettingsController.ParticipationTimeout;
            _threadTimeout = SettingsController.ThreadTimeout;
            _verificationFailTimeout = SettingsController.VerificationFailTimeout;
            _waitFullPageLoad = SettingsController.WaitFullPageLoad;
            _maxLoadingPageTimeout = SettingsController.MaxLoadingPageTimeout;

            _loadImages = SettingsController.LoadImages;
            _clickType = SettingsController.ClickType;
            _userAgent = SettingsController.UserAgent;
            _webGlVendor = SettingsController.WebGlVendor;
            _webGlRenderer = SettingsController.WebGlRenderer;
            _osPlatform = SettingsController.OsPlatform;

            _offTime = SettingsController.OffTime;
            _useLiker = SettingsController.UseLiker;
            _likeChance = SettingsController.LikeChance;
        }

        public string Cookie
        {
            get => _cookie;
            set
            {
                if (value == _cookie) return;
                _cookie = value;
                OnPropertyChanged(nameof(Cookie));

                SettingsController.Cookie = value;
            }
        }

        public bool UseNotifications
        {
            get => _useNotifications;
            set
            {
                if (value == _useNotifications) return;
                _useNotifications = value;
                OnPropertyChanged(nameof(UseNotifications));

                SettingsController.UseNotifications = value;
            }
        }

        public string TelegramBotToken
        {
            get => _telegramBotToken;
            set
            {
                if (value == _telegramBotToken) return;
                _telegramBotToken = value;
                OnPropertyChanged(nameof(TelegramBotToken));

                SettingsController.TelegramBotToken = value;
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

                SettingsController.TelegramUserId = value;
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

                SettingsController.UseProxy = value;
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

                SettingsController.ProxyLine = value;
            }
        }

        public int ParticipationTimeout
        {
            get => _participationTimeout;
            set
            {
                if (value == _participationTimeout) return;
                _participationTimeout = value;
                OnPropertyChanged(nameof(ParticipationTimeout));

                SettingsController.ParticipationTimeout = value;
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

                SettingsController.ThreadTimeout = value;
            }
        }

        public int VerificationFailTimeout
        {
            get => _verificationFailTimeout;
            set
            {
                if (value == _verificationFailTimeout) return;
                _verificationFailTimeout = value;
                OnPropertyChanged(nameof(VerificationFailTimeout));

                SettingsController.VerificationFailTimeout = value;
            }
        }

        public bool WaitFullPageLoad
        {
            get => _waitFullPageLoad;
            set
            {
                if (value == _waitFullPageLoad) return;
                _waitFullPageLoad = value;
                OnPropertyChanged(nameof(WaitFullPageLoad));

                SettingsController.WaitFullPageLoad = value;
            }
        }

        public int MaxLoadingPageTimeout
        {
            get => _maxLoadingPageTimeout;
            set
            {
                if (value == _maxLoadingPageTimeout) return;
                _maxLoadingPageTimeout = value;
                OnPropertyChanged(nameof(MaxLoadingPageTimeout));

                SettingsController.MaxLoadingPageTimeout = value;
            }
        }

        public string UserAgent
        {
            get => _userAgent;
            set
            {
                if (value == _userAgent) return;
                _userAgent = value;
                OnPropertyChanged(nameof(UserAgent));

                SettingsController.UserAgent = value;
            }
        }

        public string WebGlVendor
        {
            get => _webGlVendor;
            set
            {
                if (value == _webGlVendor) return;
                _webGlVendor = value;
                OnPropertyChanged(nameof(WebGlVendor));

                SettingsController.WebGlVendor = value;
            }
        }

        public string WebGlRenderer
        {
            get => _webGlRenderer;
            set
            {
                if (value == _webGlRenderer) return;
                _webGlRenderer = value;
                OnPropertyChanged(nameof(WebGlRenderer));

                SettingsController.WebGlRenderer = value;
            }
        }

        public string OsPlatform
        {
            get => _osPlatform;
            set
            {
                if (value == _osPlatform) return;
                _osPlatform = value;
                OnPropertyChanged(nameof(OsPlatform));

                SettingsController.OsPlatform = value;
            }
        }

        public bool LoadImages
        {
            get => _loadImages;
            set
            {
                if (value == _loadImages) return;
                _loadImages = value;
                OnPropertyChanged(nameof(LoadImages));

                SettingsController.LoadImages = value;
            }
        }

        public ClickType ClickType
        {
            get => _clickType;
            set
            {
                if (value == _clickType) return;
                _clickType = value;
                OnPropertyChanged(nameof(ClickType));

                SettingsController.ClickType = value;
            }
        }

        public string OffTime
        {
            get => _offTime;
            set
            {
                if (value == _offTime) return;
                _offTime = value;
                OnPropertyChanged(nameof(OffTime));

                SettingsController.OffTime = value;
            }
        }
        
        public bool UseLiker
        {
            get => _useLiker;
            set
            {
                if (value == _useLiker) return;
                _useLiker = value;
                OnPropertyChanged(nameof(UseLiker));

                SettingsController.UseLiker = value;
            }
        }
        
        public int LikeChance
        {
            get => _likeChance;
            set
            {
                if (value == _likeChance) return;
                _likeChance = value;
                OnPropertyChanged(nameof(LikeChance));

                SettingsController.LikeChance = value;
            }
        }
        
        public string ToJson()
        {
            var offTimeArray = OffTime.Split(new [] {", "}, StringSplitOptions.None);

            return JsonConvert.SerializeObject(new
            {
                account = new 
                {
                    cookie = Cookie
                },
                telegram_bot = new
                {
                    use = UseNotifications,
                    token = TelegramBotToken,
                    user_id = TelegramUserId
                },
                liker = new
                {
                    use = UseLiker,
                    chance = LikeChance
                },
                timeouts = new
                {
                    participation = ParticipationTimeout,
                    thread = ThreadTimeout,
                    wait_page = WaitFullPageLoad,
                    loading = MaxLoadingPageTimeout,
                    verification_fail = VerificationFailTimeout
                },
                proxy = new
                {
                    use = UseProxy,
                    line = ProxyLine
                },
                time_observer = new
                {
                    offtime = offTimeArray
                },
                browser = new
                {
                    load_images = LoadImages,
                    click_type = ClickType,
                    useragent = UserAgent,
                    vendor = WebGlVendor,
                    renderer = WebGlRenderer,
                    platform = OsPlatform
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}