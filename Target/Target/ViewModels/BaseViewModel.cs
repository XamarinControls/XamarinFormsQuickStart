using Target.Interfaces;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Target.ViewModels
{

    public class BaseViewModel : ReactiveObject, ISupportsActivation
    {
        public ISettingsFactory _settingsFactory;
        public ISettingsService _settingsService;
        private string toastMessage;
        public string ToastMessage
        {
            get
            {
                return toastMessage;
            }
            set { this.RaiseAndSetIfChanged(ref toastMessage, value); }
        }
        private string greeting;
        public string Greeting
        {
            get
            {
                return greeting;
            }
            set { this.RaiseAndSetIfChanged(ref greeting, value); }
        }
        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set { this.RaiseAndSetIfChanged(ref fontSize, value); }
        }

        public ViewModelActivator Activator
        {
            get {
                return viewModelActivator;
            }
        }

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();
        public BaseViewModel()
        {
            _settingsService = _settingsService ?? App.Container.Resolve<ISettingsService>();
            _settingsFactory = _settingsFactory ?? App.Container.Resolve<ISettingsFactory>();
            var fireandforget = Task.Run(async () => await InitializeSettings());
            

        }
        private async Task InitializeSettings()
        {
            var settings = await _settingsService.GetSettings();
            FontSize = settings.FontSize;
            var x = FontSize;
        }
    }
}
