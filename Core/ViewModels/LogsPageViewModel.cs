using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class LogsPageViewModel : BaseViewModel, ILogsPageViewModel
    {
        public LogsPageViewModel(ISettingsService iSettingsService, ISettingsFactory settingsFactory)
            : base(iSettingsService, settingsFactory)
        {

        }
    }
}
