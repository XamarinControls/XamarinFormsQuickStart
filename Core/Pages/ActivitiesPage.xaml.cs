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
    public partial class ActivitiesPage : ContentPage, IActivitiesPage, IViewFor<ActivitiesPageViewModel>
    {
        IActivitiesPageViewModel vm;
        public ActivitiesPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IActivitiesPageViewModel>();
            this.BindingContext = vm;
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<ActivitiesPage, ActivitiesPageViewModel>(x => x.ViewModel, default(ActivitiesPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ActivitiesPageViewModel)value; }
        }
        public ActivitiesPageViewModel ViewModel
        {
            get { return (ActivitiesPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}