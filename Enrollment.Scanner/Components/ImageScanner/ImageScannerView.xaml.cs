using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;

namespace Enrollment.Scanner.Components.ImageScanner
{
    /// <summary>
    /// Interaction logic for ImageScannerView.xaml
    /// </summary>
    internal partial class ImageScannerView : IViewFor<ImageScannerViewModel>
    {
        public ImageScannerView()
        {
            InitializeComponent();

            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm =>
                {
                    DataContext = vm;
                });

            this.BindCommand(ViewModel, model => model.ScanImageCommand, x => x.ScanButton);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ImageScannerViewModel) value; }
        }

        public ImageScannerViewModel ViewModel { get; set; }
    }
}
