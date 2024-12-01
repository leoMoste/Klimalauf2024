using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace BarCoding
{
    internal static class PdfBuilder
    {
        public static void build(Bitmap bit)
        {
            //creates a document
            PdfDocument document = new PdfDocument();

            //creates a new page in the document
            PdfPage page = document.AddPage();

            //use gfx for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //create Bitmap 
            Bitmap bitmap = bit;

            MemoryStream strm = new MemoryStream();
            bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage image = XImage.FromStream(strm);

            gfx.DrawImage(image, 1, 1, 250, 150);

            string filename = "Barcode.pdf";

            document.Save(filename);
            //Process.Start(filename);
        }
        public static void build(Bitmap bit, double posX)
        {
            //creates a document
            PdfDocument document = new PdfDocument();

            //creates a new page in the document
            PdfPage page = document.AddPage();

            //use gfx for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //create Bitmap 
            Bitmap bitmap = bit;

            MemoryStream strm = new MemoryStream();
            bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage image = XImage.FromStream(strm);

            gfx.DrawImage(image, posX, 1, 250, 150);

            string filename = "Barcode.pdf";

            document.Save(filename);
            //Process.Start(filename);
        }
        public static void build(Bitmap bit, double posX, double posY)
        {
            //creates a document
            PdfDocument document = new PdfDocument();

            //creates a new page in the document
            PdfPage page = document.AddPage();

            //use gfx for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //create Bitmap 
            Bitmap bitmap = bit;

            MemoryStream strm = new MemoryStream();
            bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage image = XImage.FromStream(strm);

            gfx.DrawImage(image, posX, posY, 250, 150);

            string filename = "Barcode.pdf";

            document.Save(filename);
            //Process.Start(filename);
        }
        public static void build(Bitmap bit, double posX, double posY, double width, double height)
        {
            //creates a document
            PdfDocument document = new PdfDocument();

            //creates a new page in the document
            PdfPage page = document.AddPage();

            //use gfx for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            //create Bitmap 
            Bitmap bitmap = bit;

            MemoryStream strm = new MemoryStream();
            bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

            XImage image = XImage.FromStream(strm);

            gfx.DrawImage(image, posX, posY, width, height);

            string filename = "Barcode.pdf";

            document.Save(filename);
            //Process.Start(filename);
        }
    }
}
