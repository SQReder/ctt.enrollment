using System.Drawing;

namespace Enrollment.DesktopScanner.Services
{
    public interface IScannerService
    {
        Image Scan();
    }
}