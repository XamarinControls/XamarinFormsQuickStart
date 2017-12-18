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
    public partial class ResyncPage : ContentPage, IResyncPage, IViewFor<ResyncPageViewModel>
    {
        IResyncPageViewModel vm;
        public ResyncPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IResyncPageViewModel>();
            this.BindingContext = vm;
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<ResyncPage, ResyncPageViewModel>(x => x.ViewModel, default(ResyncPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ResyncPageViewModel)value; }
        }
        public ResyncPageViewModel ViewModel
        {
            get { return (ResyncPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}