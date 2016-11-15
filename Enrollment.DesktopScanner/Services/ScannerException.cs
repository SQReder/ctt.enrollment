using System;

namespace Enrollment.DesktopScanner.Services
{
    public class ScannerException : ApplicationException
    {
        public ScannerException()
            : base()
        { }

        public ScannerException(string message)
            : base(message)
        { }

        public ScannerException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}