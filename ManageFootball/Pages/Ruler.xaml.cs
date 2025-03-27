using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Xceed.Words.NET;

namespace ManageFootball.Pages
{
    public partial class Ruler : Page
    {
        private string pdfFilePath = @"Static/Docs/Ruler.pdf";

        public Ruler()
        {
            InitializeComponent();
            LoadPdf();
        }

        private void LoadPdf()
        {
            string fullPath = Path.GetFullPath(pdfFilePath);

            if (File.Exists(fullPath))
            {
                webViewer.Navigate(new Uri(fullPath));
            }
            else
            {
                MessageBox.Show("File PDF không tồn tại: " + fullPath);
            }
        }
    }
}