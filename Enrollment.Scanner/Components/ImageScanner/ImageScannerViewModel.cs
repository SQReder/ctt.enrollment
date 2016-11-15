using System;
using System.Drawing;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Enrollment.Scanner.Components.ImageScanner
{
    internal class ImageScannerViewModel: ReactiveObject
    {
        private readonly ImageScannerModel _imageScannerModel;

        public ReactiveCommand<Image> ScanImageCommand { get; set; }

        private ObservableAsPropertyHelper<Image> _scanImageResult;

        [Reactive]
        public Image ScannedImage { get; set; }

        public ImageScannerViewModel(ImageScannerModel imageScannerModel)
        {
            _imageScannerModel = imageScannerModel;

            ScanImageCommand = ReactiveCommand.CreateAsyncTask(_ => ScanImage());

            ScanImageCommand.ToProperty(this, x => x.ScannedImage, out _scanImageResult);

            ScanImageCommand.ThrownExceptions
                .Subscribe(ex => { throw ex; });
        }

        private async Task<Image> ScanImage()
        {
            var image = await _imageScannerModel.ScanImage();
            return image;
        }
    }
}