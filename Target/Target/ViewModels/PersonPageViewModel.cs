using Target.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Target.ViewModels
{
    public class PersonPageViewModel : BaseViewModel, IPersonPageViewModel
    {
        public PersonPageViewModel()
        {
            Greeting = "Person Page";
        }
    }
}
