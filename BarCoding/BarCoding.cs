using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Windows.Compatibility;

namespace BarCoding
{
    public static class BarCoding
    {
        


        public enum OurFormats
        {
            EAN_13 = 0x80,
            EAN_8 = 0x40,
            QR_CODE = 0x800,
        }




        public static Bitmap genCode(string code, OurFormats format)
        {

            var bw = new BarcodeWriter
            {
                Format = (ZXing.BarcodeFormat)format, //EAN13 = MAX 12,EAN8 MAX 7,QR

                Options = new ZXing.Common.EncodingOptions()
                {
                    Width = 400,
                    Height = 400,
                },
            };
            if (format != OurFormats.QR_CODE)
            {

                bw.Renderer = new AlternateBitmapRenderer();
                switch (format)
                {
                    case OurFormats.EAN_13:
                        code = code.PadLeft(code.Length + (12 - code.Length), '0');
                        break;
                    case OurFormats.EAN_8:
                        code = code.PadLeft(code.Length + (7 - code.Length), '0');
                        break;


                }
            }
            Bitmap bm = null;
            try
            {
                bm = bw.Write(code);
            }
            catch (Exception e)
            {
                throw e;
            }

            return bm;
        }
        public static Bitmap genCode(string code, OurFormats format, int width, int height)
        {

            var bw = new BarcodeWriter
            {
                Format = (ZXing.BarcodeFormat)format, //EAN13,EAN8,QR,

                Options = new ZXing.Common.EncodingOptions()
                {
                    Width = width,
                    Height = height,
                },
            };
            if (format != OurFormats.QR_CODE)
            {

                bw.Renderer = new AlternateBitmapRenderer();
                switch (format)
                {
                    case OurFormats.EAN_13:
                        code = code.PadLeft(code.Length + (12 - code.Length), '0');
                        break;
                    case OurFormats.EAN_8:
                        code = code.PadLeft(code.Length + (7 - code.Length), '0');
                        break;


                }
            }
            Bitmap bm = bw.Write(code);

            return bm;
        }
        public static string genOnlycode(string code, OurFormats format)
        {
            return deCode(genCode(code, format));
        } 
        public static IEnumerable<Bitmap> genCodes(IEnumerable<string> values, OurFormats format)
        {
            var bw = new BarcodeWriter
            {
                Format = (ZXing.BarcodeFormat)format, //EAN13,EAN8,QR,

                Options = new ZXing.Common.EncodingOptions()
                {
                    Width = 400,
                    Height = 400,
                },
            };
            if (format != OurFormats.QR_CODE)
            {

                bw.Renderer = new AlternateBitmapRenderer();
                for (int i = 0; i < values.Count(); i++)
                {
                    switch (format)
                    {
                        case OurFormats.EAN_13:
                            values.ToList()[i] = values.ToList()[i].PadLeft(values.ToList()[i].Length + (12 - values.ToList()[i].Length), '0');
                            break;
                        case OurFormats.EAN_8:
                            values.ToList()[i] = values.ToList()[i].PadLeft(values.ToList()[i].Length + (7 - values.ToList()[i].Length), '0');
                            break;


                    }
                }

            }
            List<Bitmap> codes = new List<Bitmap>();
            foreach (string code in values)
            {
                codes.Add(bw.Write(code));
            }
            return codes;
        }
        public static string deCode(Bitmap code)
        {
            var br = new BarcodeReader();
            br.Options = new ZXing.Common.DecodingOptions
            {
                PureBarcode = true,

            };
            return br.Decode(code).ToString();
        }
        public static bool IsValidCode(string val, OurFormats format)
        {
            bool result = true;
            try
            {
                genCode(val, format);
            }
            catch { 
                result = false;
            }
            return result;
        }


    }
}
