using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class ActivitiesPageViewModel : BaseViewModel, IActivitiesPageViewModel
    {
        public ActivitiesPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {
            
        }
    }
}
