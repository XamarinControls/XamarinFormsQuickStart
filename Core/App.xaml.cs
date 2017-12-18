using Akavache;
using Autofac;
using Core.Converters;
using Core.Factories;
using Core.Interfaces;
using Core.Models;
using Core.Pages;
using Core.Repositories;
using Core.Services;
using Core.ViewModels;
using ReactiveUI;
using Splat;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core
{
    public partial class App : Application
    {
        MasterDetailPage masterPage;
        Page loginPage;
        public static string Device { get; set; }
        public static string Version { get; set; }
        public static bool loggedIn { get; set; }
        public static IContainer Container { get; set; }

        public App()
        {
            InitializeComponent();
            //Locator.CurrentMutable.RegisterConstant(
            //    new DoubleToIntConverter(), typeof(IBindingTypeConverter));
            SetupIOC();
            
            MessagingCenter.Subscribe<ILoginViewModel, bool>(this, "LoginStatus", (sender, args) =>
            {
                loggedIn = args;
                if (loggedIn)
                {
                    loadMainPage();
                }
            });
            MessagingCenter.Subscribe<ILogoutPage>(this, "LogMeOut", (sender) =>
            {
                loggedIn = false;
                loadLoginPage();
            });
            masterPage = (MasterDetailPage)App.Container.Resolve<IMasterPage>();
            loginPage = (Page)App.Container.Resolve<ILoginPage>();
            //if (Convert.ToBoolean(ConfigurationManager.AppSettings["bypassLogin"]))
            if (Constants.BypassLogin)
            {
                loggedIn = true;
                loadMainPage();
            }
            else
            {
                loadLoginPage();
            }
        }
        //private async Task RunStuffAsync()
        //{
        //    //var sqlservice = App.Container.Resolve<ISQLService>();
        //    //await sqlservice.CreateDB();
        //}

        private async Task InitializeSettings()
        {
            var _settingsService = App.Container.Resolve<ISettingsService>();
            await _settingsService.CheckSettings(); // seeds the settings in SQLite if empty
            var settings = await _settingsService.GetSettings();
            var _settingsFactory = App.Container.Resolve<ISettingsFactory>();
            _settingsFactory.SaveSettings((Settings)settings);
        }
        private void loadMainPage()
        {
            MainPage = masterPage;
        }
        private void loadLoginPage()
        {
            MainPage = loginPage;
        }
        private void SetupIOC()
        {
            var dbpath = DependencyService.Get<IPlatformStuff>().GetLocalFilePath("TodoSQLite.db3");
            var builder = new ContainerBuilder();
            builder.RegisterType<MasterPage>().As<IMasterPage>();
            builder.RegisterType<LoginPage>().As<ILoginPage>();
            builder.RegisterType<OrganizationPage>().As<IOrganizationPage>();
            builder.RegisterType<PersonPage>().As<IPersonPage>();
            builder.RegisterType<SettingsPage>().As<ISettingsPage>();
            builder.RegisterType<ResyncPage>().As<IResyncPage>();
            builder.RegisterType<ActivitiesPage>().As<IActivitiesPage>();
            builder.RegisterType<HomePage>().As<IHomePage>();
            builder.RegisterType<SendLogs>().As<ISendLogs>().SingleInstance();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance().AutoActivate();
            builder.RegisterType<SettingsFactory>().As<ISettingsFactory>().SingleInstance();
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();
            builder.RegisterType<SettingsViewModel>().As<ISettingsViewModel>();
            builder.RegisterType<HomePageViewModel>().As<IHomePageViewModel>();
            builder.RegisterType<MasterListViewModel>().As<IMasterListViewModel>();
            builder.RegisterType<ActivitiesPageViewModel>().As<IActivitiesPageViewModel>();
            builder.RegisterType<LogsPageViewModel>().As<ILogsPageViewModel>();
            builder.RegisterType<MasterPageViewModel>().As<IMasterPageViewModel>();
            builder.RegisterType<OrganizationPageViewModel>().As<IOrganizationPageViewModel>();
            builder.RegisterType<PersonPageViewModel>().As<IPersonPageViewModel>();
            builder.RegisterType<ResyncPageViewModel>().As<IResyncPageViewModel>();
            builder.RegisterType<SQLiteRepository>().As<ISQLiteRepository>();           
            builder.RegisterType<DoubleToIntConverter>().As<IDoubleToIntConverter>();
            /*builder.RegisterGeneric(typeof(SQLiteRepository<>)).As(typeof(ISQLiteRepository<>)).InstancePerLifetimeScope()*/
            ;

            Container = builder.Build();
        }
        protected override async void OnStart()
        {
           await InitializeSettings();
        }

        protected override void OnSleep()
        {
            BlobCache.Shutdown().Wait();
            MessagingCenter.Unsubscribe<ILoginViewModel, bool>(this, "LoginStatus");
            MessagingCenter.Unsubscribe<ILogoutPage>(this, "LogMeOut");
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {

            // Handle when your app resumes
        }

    }
}