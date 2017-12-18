using Autofac;
using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage, IMasterPage, IViewFor<MasterPageViewModel>
    {
        NavigationPage _homePage = new NavigationPage(new HomePage());
        IMasterPageViewModel vm;
        public BehaviorSubject<bool> IsOpen
        {
            get;
            set;
        }
        public MasterPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IMasterPageViewModel>();
            this.BindingContext = vm;
            this.MasterBehavior = MasterBehavior.Popover;
            MessagingCenter.Subscribe<ILogoutPage>(this, "GoHome", (sender) =>
            {
                this.Detail = _homePage;
            });
            masterPage.ListView.ItemSelected += OnItemSelected;
            IsOpen = new BehaviorSubject<bool>(false);
            IsOpen.Subscribe(enabled => {
                if (enabled)
                {
                    IsPresented = true;
                }
                else IsPresented = false;
            });
            this.Detail = _homePage;
        }
        public static readonly BindableProperty ViewModelProperty =
           BindableProperty.Create<MasterPage, MasterPageViewModel>(x => x.ViewModel, default(MasterPageViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MasterPageViewModel)value; }
        }
        public MasterPageViewModel ViewModel
        {
            get { return (MasterPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            IsOpen.Dispose();
            MessagingCenter.Unsubscribe<ILogoutPage>(this, "GoHome");
            base.OnDisappearing();
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}