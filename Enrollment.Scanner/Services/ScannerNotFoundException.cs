using CommonDialog = Microsoft.Win32.CommonDialog;

namespace Enrollment.Scanner.Services
{
    public class ScannerNotFoundException : ScannerException
    {
        public ScannerNotFoundException()
            : base("Error retrieving a list of scanners. Is your scanner or multi-function printer turned on?")
        {
        }
    }
}
