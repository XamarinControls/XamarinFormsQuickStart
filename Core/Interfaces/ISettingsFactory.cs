using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ISettingsFactory
    {
        string KeyName { get; }
        Settings GetSettings();
        void SaveSettings(Settings settings);
        void SetDefaults();
    }
}
