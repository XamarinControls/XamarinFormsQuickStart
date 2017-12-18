using Autofac;
using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.GoogleAnalytics;

namespace Target.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage, ILogoutPage
    {
        public LogoutPage()
        {
            InitializeComponent();
            logoutButton.Clicked += HandleLogoutClicked;
            cancelButton.Clicked += HandleCancelClicked;
        }
        async void HandleLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
            MessagingCenter.Send<ILogoutPage>(this, "LogMeOut");
        }
        async void HandleCancelClicked(object sender, EventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(0));
            MessagingCenter.Send<ILogoutPage>(this, "GoHome");
        }
        protected override void OnAppearing()
        {
            // Cannot be depended on in Android when navigating back to page
            base.OnAppearing();
            GoogleAnalytics.Current.Tracker.SendView("LogoutPage");
        }
        protected override void OnDisappearing()
        {
            // Not guaranteed to occur, Cannot be depended apon. 
            cancelButton.Clicked -= HandleCancelClicked;
            logoutButton.Clicked -= HandleLogoutClicked;
            base.OnDisappearing();
        }
    }
}