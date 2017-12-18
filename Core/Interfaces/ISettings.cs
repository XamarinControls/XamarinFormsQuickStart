using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ISettings
    {
        bool IsManualFont { get; set; }
        int FontSize { get; set; }
        bool ShowConnectionErrors { get; set; }
    }
}
