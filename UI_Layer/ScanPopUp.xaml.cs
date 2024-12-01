using Kreislauf.Data;
using Kreislauf.Models;
using Microsoft.EntityFrameworkCore;
using ScottPlot.AxisLimitManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using BarCoding;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System.IO;

namespace UI_Layer
{
    /// <summary>
    /// Interaktionslogik für ScanPopUp.xaml
    /// </summary>
    public partial class ScanPopUp : Window
    {
        DispatcherTimer t;
        private readonly PersonFull _personFull;
        public ScanPopUp(PersonFull personFull)
        {
            InitializeComponent();

            if (personFull == null)
            {
                MessageBox.Show("PersonFull object is null. Cannot proceed.");
                this.Close();
                return;
            }

            _personFull = personFull;
        }


        async void WaitAndClose()
        {
            t = new DispatcherTimer();
            t.Interval = TimeSpan.FromSeconds(2);
            t.Tick += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object? sender, EventArgs e)
        {
            this.Close();
            t.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Barcode code = new Barcode();

            // Extract Person from PersonFull
            Person person = _personFull.Person;

            code.PersonId = person.Id;
            

            using (AppDbContext connection = new AppDbContext())
            {

                var barcode = connection.Barcodes.FirstOrDefault(bar => bar.PersonId == code.PersonId);
                //if eintrag == Null speichern sonst update

                if(barcode == null) 
                {
                    connection.Add(code);
                    connection.SaveChanges();
                }
                barcode = connection.Barcodes.FirstOrDefault(bar => bar.PersonId == code.PersonId);

                barcode.Value = BarCoding.BarCoding.genOnlycode(barcode.Id.ToString(), BarCoding.BarCoding.OurFormats.EAN_13);

                barcode.Type = BarCoding.BarCoding.OurFormats.EAN_13.ToString();

                connection.Update(barcode);
                connection.SaveChanges();


                //Barcode im Fenster anzeigen
                Bitmap barcodeBitmap = BarCoding.BarCoding.genCode(barcode.Id.ToString(), BarCoding.BarCoding.OurFormats.EAN_13) ;

                Bitmap showBitmap = new Bitmap(barcodeBitmap, new System.Drawing.Size((int)border.Width, (int)border.Height));

                ImageSource source = Convert(showBitmap);
               

                barcodePic.ImageSource = source; 
            }

            DataGoesHere.DataContext = person;
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;


            WaitAndClose();
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }


    }
}
