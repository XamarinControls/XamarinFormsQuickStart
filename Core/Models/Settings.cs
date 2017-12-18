using Core.Interfaces;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [JsonObject]
    public class Settings : ISettings
    {
        public bool IsManualFont { get; set; }
        public int FontSize { get; set; }
        public bool ShowConnectionErrors { get; set; }
    }
}
