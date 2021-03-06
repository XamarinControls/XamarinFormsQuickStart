﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Target.Interfaces
{
    public interface IDoubleToIntConverter
    {
        int GetAffinityForObjects(Type fromType, Type toType);
        bool TryConvert(object from, Type toType, object conversionHint, out object result);
    }
}
