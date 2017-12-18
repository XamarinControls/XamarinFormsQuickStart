using Autofac;
using ReactiveUI;
using Core.Interfaces;
using Core.ViewModels;
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
    public partial class HomePage : ContentPage, IHomePage, IViewFor<HomePageViewModel>
    {
        IHomePageViewModel vm;
        public HomePage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IHomePageViewModel>();
            this.BindingContext = vm;
            var orgPage = (Page)App.Container.Resolve<IOrganizationPage>();
            var resyncPage = (Page)App.Container.Resolve<IResyncPage>();
            btnOrg.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(orgPage);
            };
            btnResync.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(resyncPage);
            };
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<HomePage, HomePageViewModel>(x => x.ViewModel, default(HomePageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HomePageViewModel)value; }
        }
        public HomePageViewModel ViewModel
        {
            get { return (HomePageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}