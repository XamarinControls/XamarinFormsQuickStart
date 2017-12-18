using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class MasterPageViewModel : BaseViewModel, IMasterPageViewModel
    {
        public MasterPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {

        }
    }
}
