﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosBridgeUtility
{
    public interface IROSWebTeleopController
    {
        void teleopTarget(String request);
    }
}
