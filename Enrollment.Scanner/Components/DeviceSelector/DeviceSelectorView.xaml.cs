using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using ReactiveUI;

namespace Enrollment.Scanner.Components.DeviceSelector
{
    /// <summary>
    /// Interaction logic for DeviceSelectorView.xaml
    /// </summary>
    internal partial class DeviceSelectorView: IViewFor<DeviceSelectorViewModel>
    {
        public DeviceSelectorView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await ViewModel.EnumerateDevices();
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (DeviceSelectorViewModel) value; }
        }

        public DeviceSelectorViewModel ViewModel { get; set; }
    }
}
