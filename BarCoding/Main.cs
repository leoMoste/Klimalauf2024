using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BarCoding.BarCoding;

namespace BarCoding
{
    class Program
    {
        static void Main(string[] args)
        {

            Bitmap bm = genCode("11", OurFormats.EAN_13);

            bm.Save("test.png");
            
            
            

        }
    }
}
