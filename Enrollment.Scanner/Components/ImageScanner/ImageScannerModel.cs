using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Enrollment.Scanner.Services;

namespace Enrollment.Scanner.Components.ImageScanner
{
    public class ImageScannerModel
    {
        private readonly ScannerService _scannerService;

        public ImageScannerModel(ScannerService scannerService)
        {
            _scannerService = scannerService;
        }

        public Task<Image> ScanImage()
        {
            Image result = null;

            var image = _scannerService.Scan();

            if (image != null)
            {

                // ReSharper disable once UseIndexedProperty
                var imageBytes = (byte[]) image.FileData.get_BinaryData();
                var ms = new MemoryStream(imageBytes);
                result = Image.FromStream(ms);
            }

            return Task.FromResult<Image>(result);
        }
    }
}