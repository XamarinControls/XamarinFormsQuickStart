using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class ResyncPageViewModel : BaseViewModel, IResyncPageViewModel
    {
        public ResyncPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {

        }
    }
}
