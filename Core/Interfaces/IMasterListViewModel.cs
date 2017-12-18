using Core.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMasterListViewModel
    {
        ReactiveList<MasterPageItem> Items
        {
            get;
            set;
        }
    }
}
