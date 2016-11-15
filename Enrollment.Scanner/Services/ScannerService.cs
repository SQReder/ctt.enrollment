using System.Runtime.InteropServices;
using WIA;

namespace Enrollment.Scanner.Services
{
    public class ScannerService
    {
        public ImageFile Scan()
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