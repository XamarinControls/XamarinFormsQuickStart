using Autofac;
using Plugin.Toasts;
using ReactiveUI;
using Core.Interfaces;
using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Core.Templates;
using FFImageLoading.Svg.Forms;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace Core.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterListPage : ContentPage, IMasterListPage, IViewFor<MasterListViewModel>
    {
        private ListView listView;
        
        public ListView ListView { get { return listView; } }
        IMasterListViewModel vm;

        public MasterListPage()
        {
            InitializeComponent();
            vm = App.Container.Resolve<IMasterListViewModel>();
            this.BindingContext = vm;


            // < ListView x: Name = "listView" CachingStrategy = "RecycleElement"
            //       HasUnevenRows = "false" SeparatorVisibility = "None"
            //       RowHeight = "80" SelectedItem = "{Binding SelectedItem, Mode=TwoWay}"
            //       ItemsSource = "{Binding Items}" BackgroundColor = "#313e4b" >

            //   < ListView.ItemTemplate >

            //       < DataTemplate >

            //           < local:ListViewCell />

            //        </ DataTemplate >

            //    </ ListView.ItemTemplate >

            //</ ListView >
            

            var dtemplate = new DataTemplate(() =>
            {
                //return new ListViewCell();
                //return new ViewCell()
                //{
                //    View = stacklayout
                //};
                var stacklayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(10, 10, 10, 10),
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };
                var lbl = new Label()
                {
                    TextColor = Color.White,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center
                };
                lbl.SetBinding(Label.TextProperty, new Binding("Title"));
                lbl.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
                var ffimg = new SvgCachedImage()
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 60,
                    HeightRequest = 60
                };
                //Source = "{Binding IconSource, Converter={StaticResource SvgImageSourceConverter}}" />
                //ffimg.Resources.Add("SvgImageSourceConverter", new  SvgImageSourceConverter());
                //ffimg.SetBinding(SvgCachedImage.)
                ffimg.SetBinding(SvgCachedImage.SourceProperty, new Binding("IconSource", BindingMode.Default, new SvgImageSourceConverter(), null));
                stacklayout.Children.Add(ffimg);
                stacklayout.Children.Add(lbl);
                return new ViewCell { View = stacklayout };
            });
            
            var fakeLabel = new Label();  // only used so the labels inside the listview template can refer to something to get their font size
            fakeLabel.SetBinding(Label.FontSizeProperty, new Binding("FontSize"));
            listView = new ListView()
            {
                HasUnevenRows = false,
                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 80,
                BackgroundColor = Color.FromHex("#313e4b"),
                ItemTemplate = dtemplate
            };
            listView.SetBinding(ListView.SelectedItemProperty, new Binding("SelectedItem"));
            listView.SetBinding(ListView.ItemsSourceProperty, new Binding("Items"));

            Content = listView;
            //var masterPageItems = new List<MasterPageItem>();
            //var imgPrefix = App.device == "uwp" ? "Assets/" : "";
            ////ShowToast(new NotificationOptions() { Description = device });

            //masterPageItems.Add(new MasterPageItem
            //{
            //    Title = "Home",
            //    IconSource = "ic_home_black_36dp.png",
            //    TargetType = typeof(HomePage)
            //});
        }
        


        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }
        private async void ShowToast(INotificationOptions options)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            if (notificator != null)
            {
                var result = await notificator.Notify(options);
            }


        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create<MasterListPage, MasterListViewModel>(x => x.ViewModel, default(MasterListViewModel));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MasterListViewModel)value; }
        }
        public MasterListViewModel ViewModel
        {
            get { return (MasterListViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}