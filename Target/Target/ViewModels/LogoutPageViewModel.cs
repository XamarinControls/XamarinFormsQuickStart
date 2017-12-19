using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Target.Interfaces;

namespace Target.ViewModels
{
    public class LogoutPageViewModel : BaseViewModel, ILogoutPageViewModel
    {
        public LogoutPageViewModel()
        {
            Greeting = $"Leaving {Constants.AppName}?";
        }
    }
}
