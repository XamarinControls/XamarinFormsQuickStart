﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISettingsService
    {
        Task<Unit> CreateSetting(Settings settings);
        Task<ISettings> GetSettings();
        Task<Unit> ResetToDefaults();
        Task CheckSettings();
    }
}
