using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using WIA;

namespace Enrollment.DesktopScanner.Services
{
    public class ScannerService : IScannerService
    {
        public Image Scan()
        {
            Image result = null;

            var image = ScanInternal();

            if (image != null)
            {

                // ReSharper disable once UseIndexedProperty
                var imageBytes = (byte[])image.FileData.get_BinaryData();
                var ms = new MemoryStream(imageBytes);
                result = Image.FromStream(ms);
            }

            return result;
        }

        private ImageFile ScanInternal()
        {
            ImageFile image;

            try
            {
                var dialog = new CommonDialogClass();

                image = dialog.ShowAcquireImage(
                    WiaDeviceType.ScannerDeviceType,
                    WiaImageIntent.ColorIntent,
                    WiaImageBias.MaximizeQuality,
                    WIA.FormatID.wiaFormatJPEG,
                    false,
                    true,
                    false);

                return image;
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2145320939)
                {
                    throw new ScannerNotFoundException();
                }
                else
                {
                    throw new ScannerException("COM Exception", ex);
                }
            }
        }

    }
}