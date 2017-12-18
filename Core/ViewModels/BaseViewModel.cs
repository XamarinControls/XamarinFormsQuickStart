using Core.Interfaces;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class BaseViewModel : ReactiveObject, ISupportsActivation
    {
        private readonly ISettingsFactory _settingsFactory;
        private readonly ISettingsService _ISettingsSErvice;

        public string UrlPathSegment
        {
            get;
            protected set;
        }

        public IScreen HostScreen
        {
            get;
            protected set;
        }
        public string AppName
        {
            get { return Constants.AppName; }
        }
        public string Greeting
        {
            get
            {
                return $"Welcome to {AppName}!";
            }
        }
        
        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set { this.RaiseAndSetIfChanged(ref fontSize, value); }
        }

        public int FontSizeSmall
        {
            get { return FontSize - Constants.FontSizeSmallSubtract; }
        }
        public int FontSizeLarge
        {
            get { return FontSize + Constants.FontSizeLargeAdd; }
        }
        public ViewModelActivator Activator
        {
            get { return viewModelActivator; }
        }

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();

        public BaseViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory, IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            _ISettingsSErvice = iSettingsService;
            _settingsFactory = settingsFactory;
            var fireandforget = Task.Run(async () => await InitializeSettings());
        }
        private async Task InitializeSettings()
        {
            var settings = await _ISettingsSErvice.GetSettings();
            FontSize = settings.FontSize;
        }

    }
}
