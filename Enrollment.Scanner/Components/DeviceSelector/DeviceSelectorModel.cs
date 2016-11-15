using System;
using Enrollment.Scanner.Components.DeviceSelector.Model;

namespace Enrollment.Scanner.Components.DeviceSelector
{
    internal class DeviceSelectorModel
    {
        public DeviceInfo[] EnumerateDevices()
        {
            return new DeviceInfo[]
            {
                new DeviceInfo {Name = "one"}, 
            };
        }
    }
}
