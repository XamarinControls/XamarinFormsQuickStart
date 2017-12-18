using Autofac;
using Core.Interfaces;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;

namespace Core.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, ILoginPage, IViewFor<LoginViewModel>
    {
        ILoginViewModel loginVM;
        public LoginPage()
        {
            InitializeComponent();
            loginVM = App.Container.Resolve<ILoginViewModel>();
            this.BindingContext = loginVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<LoginPage, LoginViewModel>(x => x.ViewModel, default(LoginViewModel));
        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }
        public LoginViewModel ViewModel
        {
            get { return (LoginViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}