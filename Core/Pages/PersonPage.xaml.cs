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
    public partial class PersonPage : ContentPage, IPersonPage, IViewFor<PersonPageViewModel>
    {
        IPersonPageViewModel vm;
        public PersonPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IPersonPageViewModel>();
            this.BindingContext = vm;
            var activitiesPage = (Page)App.Container.Resolve<IActivitiesPage>();
            btnActivities.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(activitiesPage);
            };
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<PersonPage, PersonPageViewModel>(x => x.ViewModel, default(PersonPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PersonPageViewModel)value; }
        }
        public PersonPageViewModel ViewModel
        {
            get { return (PersonPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}