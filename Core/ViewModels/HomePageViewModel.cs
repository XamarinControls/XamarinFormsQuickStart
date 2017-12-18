using ReactiveUI;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class HomePageViewModel : BaseViewModel, IHomePageViewModel
    {
        private string sourceImg;
        public string SourceImg
        {
            get
            {
                return sourceImg;
            }
            set { this.RaiseAndSetIfChanged(ref sourceImg, value); }
        }

        public HomePageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {
            sourceImg = "resource://Core.Resources.ic_home_black_36px.svg";
        }
    }
}
