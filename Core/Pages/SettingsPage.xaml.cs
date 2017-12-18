using Autofac;
using Core.Interfaces;
using Core.Models;
using Core.Templates;
using Core.ViewModels;
using Genesis.Logging;
using Plugin.Toasts;
using ReactiveUI;
using ReactiveUI.XamForms;
using System;
using Xamarin.Forms;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Splat;
using Core.Converters;

namespace Core.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ReactiveContentPage<SettingsViewModel>, ISettingsPage
    {
        ISettingsViewModel settingsVM;
        Switch showConnectionErrors;
        Button LogsButton;
        Button SendButton;
        Label isManualFontLabel;
        Label fontSliderLabel;
        Slider fontSlider;
        Grid fontSliderGrid;
        Switch isManualFont;
        Settings settings;
        IBindingTypeConverter bindingTypeConverter;

        public SettingsPage()
        {
            var logger = LoggerService.GetLogger(this.GetType());
            bindingTypeConverter = (IBindingTypeConverter)App.Container.Resolve<IDoubleToIntConverter>();

            using (logger.Perf("Initialize component."))
            {
                InitializeComponent();
            }
            settings = App.Container.Resolve<ISettingsFactory>().GetSettings();
            settingsVM = App.Container.Resolve<ISettingsViewModel>();
            
            var fontSize = settings.FontSize;
            //this.BindingContext = settingsVM;
            //((SettingsViewModel)this.BindingContext).IsManualFontOn = true;
            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, getDevicePadding(), 10, 5);

            isManualFontLabel = new Label()
            {
                Text = "Enable Manual Font"
            };
            //isManualFontLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            isManualFont = new Switch()
            {
                HorizontalOptions = LayoutOptions.End
            };
            isManualFont.SetBinding(Switch.IsToggledProperty, new Binding("IsManualFontOn"));

            var isManualFontGrid = new TwoValueHorizontalGrid().Create();
            isManualFontGrid.Children.Add(isManualFontLabel, 0, 0);
            isManualFontGrid.Children.Add(isManualFont, 1, 0);

            fontSliderLabel = new Label
            {
                Text = $"Custom Font Size is {fontSize}",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            fontSliderLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));

            fontSlider = new Slider
            {
                Maximum = Constants.FontSizeMax,
                Minimum = 12,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //fontSlider.SetBinding(Slider.ScaleProperty, new Binding("FontSize"));
            fontSlider.SetBinding(Slider.ValueProperty, new Binding("FontSize"));
            fontSlider.SetBinding(Slider.IsEnabledProperty, new Binding("IsManualFontOn"));
            fontSliderGrid = new TwoValueHorizontalGrid().Create();
            fontSliderGrid.Children.Add(fontSliderLabel, 0, 0);
            fontSliderGrid.Children.Add(fontSlider, 1, 0);
            var showConnectionErrorsLabel = new Label()
            {
                Text = "Show Connection Errors"
            };
            showConnectionErrorsLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            showConnectionErrors = new Switch()
            {
                HorizontalOptions = LayoutOptions.End
            };
            showConnectionErrors.SetBinding(Switch.IsToggledProperty, new Binding("ShowConnectionErrors"));
            var showConnectionErrorsGrid = new TwoValueHorizontalGrid().Create();
            showConnectionErrorsGrid.Children.Add(showConnectionErrorsLabel, 0, 0);
            showConnectionErrorsGrid.Children.Add(showConnectionErrors, 1, 0);

            Label VersionTextLabel = new Label()
            {
                Text = "Version"
            };
            VersionTextLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            Label VersionValueLabel = new Label()
            {
                Text = App.Version,
                HorizontalOptions = LayoutOptions.End
            };
            VersionValueLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            var versionGrid = new TwoValueHorizontalGrid().Create();
            versionGrid.Children.Add(VersionTextLabel, 0, 0);
            versionGrid.Children.Add(VersionValueLabel, 1, 0);

            //var layout = new StackLayout()
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    Padding = new Thickness(15, 0, 10, 0)
            //};
            var LogsLabel = new Label()
            {
                Text = "Error Logs"
            };
            LogsLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));

            LogsButton = new ButtonAsLink()
            {
                Text = "View Logs...",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
            LogsButton.SetBinding(Button.FontSizeProperty, new Binding("FontSize"));

            var logsButtonGrid = new TwoValueHorizontalGrid().Create();
            logsButtonGrid.Children.Add(LogsLabel, 0, 0);
            logsButtonGrid.Children.Add(LogsButton, 1, 0);


            //var layoutSend = new StackLayout()
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    Padding = new Thickness(15, 0, 10, 0)
            //};
            var SendButtonLabel = new Label()
            {
                Text = "Analytics"
            };
            SendButtonLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            SendButton = new ButtonAsLink()
            {
                Text = "Send Logs...",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
            SendButton.SetBinding(Button.FontSizeProperty, new Binding("FontSize"));

            SendButton.Clicked += (sender, e) =>
            {
                ((SettingsViewModel)this.BindingContext).SendLogsClicked = 1;
            };
            SendButton.SetBinding(IsEnabledProperty, new Binding("SendEnabled"));

            var sendButtonGrid = new TwoValueHorizontalGrid().Create();
            sendButtonGrid.Children.Add(SendButtonLabel, 0, 0);
            sendButtonGrid.Children.Add(SendButton, 1, 0);

            ActivityIndicator activity = new ActivityIndicator()
            {
                BackgroundColor = Color.Transparent,
                IsRunning = true,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            activity.SetBinding(IsVisibleProperty, new Binding("IsWaiting"));
            LogsButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new LogsPage());
            };

            //layout.Children.Add(LogsButton);
            //layoutSend.Children.Add(SendButton);
            //layoutSend.Children.Add(activity);
            var tableSectionInterface = new TableSection()
            {
                Title = "Interface"
            };
            
            this.Content = new TableView
            {

                Root = new TableRoot()
                {
                    new TableSection("Interface"){                        
                        new ViewCell() {
                            View = isManualFontGrid
                        },
                        new ViewCell() {
                            View = fontSliderGrid
                        }
                    },
                    new TableSection("Diagnostics")
                    {
                        new ViewCell(){
                            View = showConnectionErrorsGrid
                        },
                        new ViewCell() {
                            View = logsButtonGrid
                        },
                        new ViewCell() {
                            View = sendButtonGrid
                        },
                        new ViewCell()
                        {
                            View = activity
                        }
                    },
                    new TableSection("System")
                    {
                       new ViewCell() {
                            View = versionGrid
                        }
                    }
                },
                Intent = TableIntent.Settings
            };

            this
                .WhenActivated(
                    disposables =>
                    {
                        using (logger.Perf("Activate."))
                        {
                            
                            this
                            //this.OneWayBind(ViewModel, vm => vm.PreAnswerVisability, v => v.UserAnswer.Visibility, vmToViewConverterOverride: new MyConvertor());
                                .OneWayBind(this.ViewModel, x => x.FontSize, x => x.isManualFontLabel.FontSize, vmToViewConverterOverride: bindingTypeConverter)
                                .DisposeWith(disposables);
                            this.fontSlider.Events().ValueChanged
                                .Throttle(TimeSpan.FromMilliseconds(300), RxApp.MainThreadScheduler)
                                .Do((x) =>
                                {
                                    var rounded = Math.Round(x.NewValue);
                                    fontSliderLabel.Text = $"Custom Font Size is {rounded}";
                                    MessagingCenter.Send<ISettingsPage>(this, "mSettingsFontChanged");
                                })
                                .InvokeCommand(this, x => x.ViewModel.FontSliderChanged)
                                .DisposeWith(disposables);
                            //this
                            //    .Bind(this.ViewModel, x => x.SelectedProgram, x => x.exerciseProgramsListView.SelectedItem)
                            //    .AddTo(disposables);
                            //this
                            //    .OneWayBind(this.ViewModel, x => x.Status, x => x.errorLabel.Text, x => GetErrorMessage(x))
                            //    .AddTo(disposables);
                            //this
                            //    .OneWayBind(this.ViewModel, x => x.ParseErrorMessage, x => x.errorDetailLabel.Text)
                            //    .AddTo(disposables);
                            //this
                            //    .OneWayBind(this.ViewModel, x => x.Programs, x => x.exerciseProgramsListView.ItemsSource)
                            //    .AddTo(disposables);
                            //this
                            //    .OneWayBind(this.ViewModel, x => x.Status, x => x.errorLayout.IsVisible, x => IsErrorStatus(x))
                            //    .AddTo(disposables);

                            //this
                            //    .WhenAnyValue(x => x.ViewModel.Status, IsLoadingStatus)
                            //    .DistinctUntilChanged()
                            //    .Select(isActive => Observable.Defer(() => this.AnimateActivityIndicatorOpacity(isActive)))
                            //    .Concat()
                            //    .SubscribeSafe()
                            //    .AddTo(disposables);

                            //this
                            //    .WhenAnyValue(x => x.ViewModel.Status, x => !IsLoadingStatus(x) && !IsErrorStatus(x))
                            //    .DistinctUntilChanged()
                            //    .Select(isVisible => Observable.Defer(() => this.AnimateListViewOpacity(isVisible)))
                            //    .Concat()
                            //    .SubscribeSafe()
                            //    .AddTo(disposables);
                        }
                    });
        }



        double getDevicePadding()
        {

            double topPadding;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    topPadding = 20;
                    break;
                default:
                    topPadding = 0;
                    break;
            }

            return topPadding;
        }

        void IsManualFont_OnChanged(object sender, ToggledEventArgs e)
        {
            ((SettingsViewModel)this.BindingContext).IsManualFontOnClicked = 1;
            
        }

        void showConnectionErrors_Toggled(Object sender, ToggledEventArgs e)
        {
            ((SettingsViewModel)this.BindingContext).ShowConnectionErrorsClicked = 1;
        }

        void OnFontSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var rounded = Math.Round(e.NewValue);
            //fontSliderLabel.Text = String.Format("Custom Font Size is {0:F1}", e.NewValue);
            fontSliderLabel.Text = $"Custom Font Size is {rounded}";
            //Settings.FontSize = (int)Math.Round(rounded);
            //((SettingsViewModel)this.BindingContext).FontSizeClicked = 1;
            MessagingCenter.Send<ISettingsPage>(this, "mSettingsFontChanged");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //var manualfont = Settings.IsManualFont;
            //((SettingsViewModel)this.BindingContext).IsManualFontOn = Settings.IsManualFont;
            //fontSlider.ValueChanged += OnFontSliderValueChanged;
            isManualFont.Toggled += IsManualFont_OnChanged;
            showConnectionErrors.Toggled += showConnectionErrors_Toggled;
        }

        protected override void OnDisappearing()
        {
            showConnectionErrors.Toggled -= showConnectionErrors_Toggled;
            isManualFont.Toggled -= IsManualFont_OnChanged;
            fontSlider.ValueChanged -= OnFontSliderValueChanged;            
            base.OnDisappearing();
            
        }

        private async void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            if (notificator != null)
            {
                var result = await notificator.Notify(options);
            }
        }
    }
}