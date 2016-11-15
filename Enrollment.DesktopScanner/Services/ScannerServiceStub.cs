using System.Drawing;

namespace Enrollment.DesktopScanner.Services
{
    internal class ScannerServiceStub : IScannerService
    {
        public Image Scan()
        {
            var bitmap = new Bitmap(100, 100);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawLine(Pens.Blue, 0, 0, 100, 100);
            }

            return bitmap;
        }
    }
}