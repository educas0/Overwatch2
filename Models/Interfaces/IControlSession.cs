﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Models.Interfaces
{
    public interface IControlSession
    {
        void AddItemSession<T>(String clave, T valor);
        T RecuperaItemSession<T>(String clave);
    }
}
