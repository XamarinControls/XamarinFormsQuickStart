﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Target.Interfaces
{
    public interface IPlatformStuff
    {
        string GetLocalFilePath(string filename);
        string GetBaseUrl();
    }
}
