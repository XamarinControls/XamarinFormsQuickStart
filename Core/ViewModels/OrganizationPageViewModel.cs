using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class OrganizationPageViewModel : BaseViewModel, IOrganizationPageViewModel
    {
        public OrganizationPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {

        }
    }
}
