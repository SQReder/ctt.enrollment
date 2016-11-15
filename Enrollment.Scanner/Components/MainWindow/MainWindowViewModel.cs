using Enrollment.Scanner.Components.DeviceSelector;
using ReactiveUI.Fody.Helpers;

namespace Enrollment.Scanner.Components.MainWindow
{
    internal class MainWindowViewModel
    {
        [Reactive]
        public DeviceSelectorViewModel DeviceSelectorViewModel { get; set; }

        public MainWindowViewModel(
            DeviceSelectorViewModel deviceSelectorViewModel
            )
        {
            DeviceSelectorViewModel = deviceSelectorViewModel;
        }
    }
}
