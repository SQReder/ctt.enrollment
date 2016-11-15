using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Enrollment.Scanner.Components.DeviceSelector.Model;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Enrollment.Scanner.Components.DeviceSelector
{
    internal class DeviceSelectorViewModel: ReactiveObject
    {
        private readonly DeviceSelectorModel _deviceSelectorModel;

        [Reactive]
        public DeviceInfo[] Devices { get; set; }

        [Reactive]
        public ReactiveCommand<DeviceInfo[]> EnumerateDevicesCommand { get; set; }

        private readonly ObservableAsPropertyHelper<DeviceInfo[]> _deviceEnumerationResult;

        public DeviceSelectorViewModel(DeviceSelectorModel deviceSelectorModel)
        {
            _deviceSelectorModel = deviceSelectorModel;

            EnumerateDevicesCommand = ReactiveCommand.CreateAsyncTask(_ => EnumerateDevices());

            EnumerateDevicesCommand.ToProperty(this, x => x.Devices, out _deviceEnumerationResult);

            EnumerateDevicesCommand.ThrownExceptions
                .Subscribe(ex => { throw ex; });
        }


        public async Task<DeviceInfo[]> EnumerateDevices()
        {
            return _deviceSelectorModel.EnumerateDevices();
        }
    }
}
