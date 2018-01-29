using System;
using System.Collections.Generic;
using System.Text;

namespace NewsReader
{
    public enum AppOrientation
    {
        Portrait,
        Landscape,
    }

    interface ISystemConfiguration
    {
        void OnOrientationChanged(AppOrientation orientation);
    }
}
