using Core.Interfaces;
using Plugin.Toasts;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Splat;
using Genesis.Logging;
using Genesis.Ensure;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive;

namespace Core.ViewModels
{
    public class SettingsViewModel : ReactiveObject, ISettingsViewModel, IRoutableViewModel, ISupportsActivation
    {
        private readonly ViewModelActivator activator;
        private readonly Genesis.Logging.ILogger logger;
        public string UrlPathSegment { get; }
        IScreen hostScreen;
        public IScreen HostScreen => hostScreen;
        ISendLogs _sendLogs;
        ISettingsFactory _settingsFactory;
        ISettingsService _ISettingsSErvice;
        ObservableAsPropertyHelper<bool> sendEnabled;
        public bool SendEnabled
        {
            get { return sendEnabled.Value; }
        }

        bool isManualFontOn;
        public bool IsManualFontOn
        {
            get { return isManualFontOn; }
            set { this.RaiseAndSetIfChanged(ref isManualFontOn, value); }
        }

        bool isSwitchOn;
        public bool IsSwitchOn
        {
            get { return isSwitchOn; }
            set { this.RaiseAndSetIfChanged(ref isSwitchOn, value); }
        }

        bool isWaiting;
        public bool IsWaiting
        {
            get { return isWaiting; }
            set { this.RaiseAndSetIfChanged(ref isWaiting, value); }
        }

        private bool showConnectionErrors;
        public bool ShowConnectionErrors
        {
            get { return showConnectionErrors; }
            set { this.RaiseAndSetIfChanged(ref showConnectionErrors, value); }
        }

        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set { this.RaiseAndSetIfChanged(ref fontSize, value); }
        }

        //public int FontSizeClicked
        //{ set { Task.Run(async () => await SetFontSize()); } }
        public int ShowConnectionErrorsClicked
        { set { Task.Run(async () => await SetShowConnectionErrors()); } }
        public int IsManualFontOnClicked
        {set{Task.Run(async () => await SetManualFont());}}        
        public int SendLogsClicked
        {
            set
            {
                //Task.Run(async () =>
                //{
                //    await sendLogs();
                //});
            }
        }
        private readonly ReactiveCommand<Unit, Unit> fontSliderChanged;
        public ReactiveCommand FontSliderChanged => this.fontSliderChanged;
        public SettingsViewModel(ISendLogs sendLogs, ISettingsService iSettingsService, ISettingsFactory settingsFactory, IScreen screen = null)
        {
            Ensure.ArgumentNotNull(sendLogs, nameof(sendLogs));
            Ensure.ArgumentNotNull(iSettingsService, nameof(iSettingsService));
            Ensure.ArgumentNotNull(settingsFactory, nameof(settingsFactory));
            Ensure.ArgumentNotNull(screen, nameof(screen));
            this.activator = new ViewModelActivator();
            _ISettingsSErvice = iSettingsService;
            _settingsFactory = settingsFactory;
            _sendLogs = sendLogs;
            this.logger = LoggerService.GetLogger(this.GetType());
            hostScreen = screen ?? Locator.Current.GetService<IScreen>();
            var fireandforget = Task.Run(async () => await InitializeSettings());
            this.fontSliderChanged = ReactiveCommand.CreateFromTask(async () =>  await SetFontSize());
            //FontSliderChanged = ReactiveCommand.CreateFromTask(async ()=> await SetFontSize());
            //this.WhenAnyValue(x => x.IsWaiting)
            //    .ToProperty(this, x => x.SendEnabled, out sendEnabled);
            //this.WhenActivated(registerDisposable =>
            //{
            //    using (this.logger.Perf("Activation"))
            //    {
            //        registerDisposable(this.WhenAnyValue(x => x.IsWaiting)
            //        .ToProperty(this, x => x.SendEnabled, out sendEnabled));
            //    }
            //});
            
            this
                    .WhenActivated(
                        disposables =>
                        {
                            using (this.logger.Perf("Activation"))
                            {
                                this.WhenAnyValue(x => x.IsWaiting)
                                    .ToProperty(this, x => x.SendEnabled, out sendEnabled)
                                    .DisposeWith(disposables);
                                //this.fontSliderChanged.DisposeWith(disposables);
                            }
                        });
            //this.WhenAnyValue(x => x.IsWaiting, (arg) => !arg)
            //    //.Select(x => x.Split(' ')[0])
            //    .ToProperty(this, x => x.SendEnabled, out sendEnabled);
        }
        private async Task SetManualFont()
        {
            var setting = _settingsFactory.GetSettings();
            setting.IsManualFont = IsManualFontOn;            
            var settings = await _ISettingsSErvice.CreateSetting(setting);
            if (!IsManualFontOn)
            {
                FontSize = 16;
            }            
        }
        private async Task SetFontSize()
        {
            var setting = _settingsFactory.GetSettings();
            var mydouble = (double)FontSize;
            var rounded = (int)Math.Round(mydouble);
            setting.FontSize = rounded;
            var settings = await _ISettingsSErvice.CreateSetting(setting);
        }
        private async Task SetShowConnectionErrors()
        {
            var setting = _settingsFactory.GetSettings();
            setting.ShowConnectionErrors = ShowConnectionErrors;
            var settings = await _ISettingsSErvice.CreateSetting(setting);
        }
        private async Task InitializeSettings()
        {
            var settings = await _ISettingsSErvice.GetSettings();
            IsManualFontOn = settings.IsManualFont;
            FontSize = settings.FontSize;
            ShowConnectionErrors = settings.ShowConnectionErrors;
            //this.WhenAnyValue(x => x.IsManualFontOn)
            //    .Skip(1)
            //    .Subscribe(async val =>
            //    {
            //          await SetManualFont();
            //    });
        }
        private async Task sendLogs()
        {
            //var errorList = JsonConvert.DeserializeObject<List<ErrorItem>>(Settings.LogSetting);
            //var errors = new Errors()
            //{
            //    ErrorItems = errorList
            //};
            //var returned = false;
            //if (errorList == null || errorList.Count() == 0)
            //{
            //    ShowToast(new NotificationOptions()
            //    {
            //        Title = "Oops!",
            //        Description = $"Nothing to send.",
            //        IsClickable = true,
            //        //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
            //        ClearFromHistory = true,
            //        //DelayUntil = DateTime.Now.AddSeconds(0)
            //    });
            //}
            //else
            //{
            //    IsWaiting = true;
            //    returned = await _sendLogs.Send(errors);
            //    if (returned)
            //    {
            //        IsWaiting = false;
            //        ShowToast(new NotificationOptions()
            //        {
            //            Title = "Success",
            //            Description = $"Logs Successfully sent.",
            //            IsClickable = true,
            //            //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
            //            ClearFromHistory = true,
            //            //DelayUntil = DateTime.Now.AddSeconds(0)
            //        });
            //    }
            //    else
            //    {
            //        IsWaiting = false;
            //        ShowToast(new NotificationOptions()
            //        {
            //            Title = "Error",
            //            Description = $"Error Sending Logs.",
            //            IsClickable = true,
            //            //WindowsOptions = new WindowsOptions() { LogoUri = "icon.png" },
            //            ClearFromHistory = true,
            //            //DelayUntil = DateTime.Now.AddSeconds(0)
            //        });
            //    }
            //}


        }

        private async void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            if (notificator != null)
            {
                var result = await notificator.Notify(options);
            }
        }
        public ViewModelActivator Activator => activator;

        //ViewModelActivator ISupportsActivation.Activator => _viewModelActivator;
    }
}
