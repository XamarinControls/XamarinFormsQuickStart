using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class PersonPageViewModel : BaseViewModel, IPersonPageViewModel
    {
        public PersonPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {

        }
    }
}
