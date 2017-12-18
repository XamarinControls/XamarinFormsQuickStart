using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public abstract class MustInitialize<T>
    {
        public MustInitialize(T parameters) { }
    }
}
