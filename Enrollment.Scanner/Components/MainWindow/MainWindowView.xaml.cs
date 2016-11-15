using System;
using ReactiveUI;

namespace Enrollment.Scanner.Components.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindowView : IViewFor<MainWindowViewModel>
    {
        public MainWindowView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => DataContext = vm);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel) value; }
        }

        public MainWindowViewModel ViewModel { get; set; }
    }
}
