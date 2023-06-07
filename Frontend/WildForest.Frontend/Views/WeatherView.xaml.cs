using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WildForest.Frontend.Views;

public partial class WeatherView : System.Windows.Controls.UserControl
{
    public WeatherView() => InitializeComponent();

    private void CreatePDFClick(object sender, RoutedEventArgs e)
    {
        string pdfFileName = Path.GetTempPath() + "PDFFile.pdf";
        string imagePath = Path.GetTempPath() + "window.png";

        SaveAsPng(GetImage(MainGrid), imagePath);
        CreatePdfFromImage(imagePath, pdfFileName);
    }

    private RenderTargetBitmap GetImage(UIElement view)
    {
        Size size = new(view.RenderSize.Width, view.RenderSize.Height);

        if (size.IsEmpty)
            return null!;

        RenderTargetBitmap result = new((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
        var drawVisual = new DrawingVisual();

        using (var context = drawVisual.RenderOpen())
        {
            context.DrawRectangle(new VisualBrush(view), null, new Rect(0, 0, (int)size.Width, (int)size.Height));
            context.Close();
        }

        result.Render(drawVisual);
        return result;
    }

    private void SaveAsPng(RenderTargetBitmap src, string targetFile)
    {
        PngBitmapEncoder encoder = new();
        encoder.Frames.Add(BitmapFrame.Create(src));

        try
        {
            using (var stream = File.Create(targetFile))
            {
                encoder.Save(stream);
            }
        }
        catch (IOException) { }
    }

    private void CreatePdfFromImage(string imageFile, string pdfFile)
    {
        using (var ms = new MemoryStream())
        {
            try
            {
                var document = new Document(PageSize.LETTER.Rotate(), 0, 0, 0, 0);
                PdfWriter.GetInstance(document, new FileStream(pdfFile, FileMode.Create));
                PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();

                FileStream fs = new(imageFile, FileMode.Open);
                var image = Image.GetInstance(fs);
                image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                document.Add(image);
                document.Close();

                Process.Start("explorer.exe", pdfFile);
            }
            catch (IOException)
            {
                MessageBox.Show("You have already generated pdf file!", "Wild forest", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}