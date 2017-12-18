using Autofac;
using Core.Interfaces;
using Core.ViewModels;
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
    public partial class OrganizationPage : ContentPage, IOrganizationPage, IViewFor<OrganizationPageViewModel>
    {
        IOrganizationPageViewModel vm;
        public OrganizationPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IOrganizationPageViewModel>();
            this.BindingContext = vm;
            var personPage = (Page)App.Container.Resolve<IPersonPage>();
            btnPerson.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(personPage);
            };
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<OrganizationPage, OrganizationPageViewModel>(x => x.ViewModel, default(OrganizationPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (OrganizationPageViewModel)value; }
        }
        public OrganizationPageViewModel ViewModel
        {
            get { return (OrganizationPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}