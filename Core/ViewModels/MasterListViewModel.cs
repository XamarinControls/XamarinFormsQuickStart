using ReactiveUI;
using Core.Interfaces;
using Core.Models;
using Core.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using Xamarin.Forms;

namespace Core.ViewModels
{
    public class MasterListViewModel : BaseViewModel, IMasterListViewModel
    {
        private readonly ISettingsFactory _settingsFactory;

        private ReactiveList<MasterPageItem> _items;

        public ReactiveList<MasterPageItem> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ReactiveList<MasterPageItem>();
                }
                return _items;
            }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }
        //private ObservableCollection<MasterPageItem> _items;
        //public ObservableCollection<MasterPageItem> Items
        //{
        //    get { return _items; }
        //    set { this.RaiseAndSetIfChanged(ref _items, value); }
        //}
        //public ObservableCollection<MasterPageItem> Items { get; } =
        //    new ObservableCollection<MasterPageItem>();
        public MasterListViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {
            _settingsFactory = settingsFactory;
            MessagingCenter.Subscribe<ISettingsPage>(this, "mSettingsFontChanged", (sender) =>
            {
                var fireandforget2 = Task.Run(async () => await RunAsync());
            });
            var fireandforget = Task.Run(async () => await RunAsync());
        }
        private async Task RunAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1000)).ConfigureAwait(false);
            var size = _settingsFactory.GetSettings().FontSize;
            _items = _items ?? new ReactiveList<MasterPageItem>();
            Device.BeginInvokeOnMainThread(() =>
            {
                _items.Clear();
                _items.Add(new MasterPageItem()
                {
                    Title = "HOME",
                    IconSource = "resource://Core.Resources.ic_home_black_36px.svg",
                    TargetType = typeof(HomePage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "ORGANIZATION",
                    IconSource = "resource://Core.Resources.ic_public_black_24px.svg",
                    TargetType = typeof(OrganizationPage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "PERSON",
                    IconSource = "resource://Core.Resources.ic_person_black_24px.svg",
                    TargetType = typeof(PersonPage),
                    FontSize = size
                });
                _items.Add(new MasterPageItem()
                {
                    Title = "ACTIVITIES",
                    IconSource = "resource://Core.Resources.ic_event_note_black_24px.svg",
                    TargetType = typeof(ActivitiesPage),
                    FontSize = size
                });
                if (App.loggedIn)
                {
                    _items.Add(new MasterPageItem()
                    {
                        Title = "LOGOUT",
                        IconSource = "resource://Core.Resources.ic_vpn_key_black_24px.svg",
                        TargetType = typeof(LogoutPage),
                        FontSize = size
                    });
                }
                else
                {
                    _items.Add(new MasterPageItem()
                    {
                        Title = "LOGIN",
                        IconSource = "resource://Core.Resources.ic_vpn_key_black_24px.svg",
                        TargetType = typeof(LoginPage),
                        FontSize = size
                    });
                }
                _items.Add(new MasterPageItem()
                {
                    Title = "SETTINGS",
                    IconSource = "resource://Core.Resources.ic_settings_black_24px.svg",
                    TargetType = typeof(SettingsPage),
                    FontSize = size
                });
            });
        }
    }
}
