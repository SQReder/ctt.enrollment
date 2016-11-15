using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Enrollment.DesktopScanner.Services;

namespace Enrollment.DesktopScanner
{
    internal partial class MainForm : Form
    {
        private readonly IScannerService _scannerService;
        private Image _image;            
        private static string ApplicationDirectory => AppDomain.CurrentDomain.BaseDirectory;
        private static string OutputDirectory => Path.Combine(ApplicationDirectory, "./images");

        public MainForm(IScannerService scannerService)
        {
            InitializeComponent();

            _scannerService = scannerService;

            if (!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }
        }

        private string CurrentOutputFileName => Path.Combine(OutputDirectory, $"{CodeTextBox.Text}_{DateTimeOffset.UtcNow.Ticks}.jpg");

        private void button1_Click(object sender, EventArgs e)
        {
            _image = _scannerService.Scan();

            if (_image != null)
            {
                using (var fileStream = new FileStream(CurrentOutputFileName, FileMode.Create))
                {                    
                    _image.Save(fileStream, ImageFormat.Jpeg);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var path = ApplicationDirectory;
            System.Diagnostics.Process.Start(path);
        }
    }
}
