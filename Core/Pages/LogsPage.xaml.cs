using Autofac;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Pages
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogsPage : ContentPage, ILoginPage, IViewFor<LogsPageViewModel>
    {
        ILogsPageViewModel vm;
        public LogsPage ()
		{
            InitializeComponent();
            //var rawLogString = Settings.LogSetting;
            //var myList = JsonConvert.DeserializeObject<List<ErrorItem>>(rawLogString);
            //var listView = new ListView { ItemsSource = myList };
            vm = App.Container.Resolve<ILogsPageViewModel>();
            this.BindingContext = vm;

            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children = {
                    new Label {
                        Text = "Logs",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    //listView
                }
            };
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<LogsPage, LogsPageViewModel>(x => x.ViewModel, default(LogsPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LogsPageViewModel)value; }
        }
        public LogsPageViewModel ViewModel
        {
            get { return (LogsPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}