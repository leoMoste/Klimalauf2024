using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BarCoding;
using Kreislauf.Data;
using MvvM.ViewModel;

namespace UI_Layer
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class BarcodeSession : Window
    {

        // Dictionary to store the barcode and its last scanned time
        private Dictionary<string, DateTime> barcodeLastScanned = new Dictionary<string, DateTime>();
        private const int MIN_SCAN_INTERVAL_MINUTES = 7; // Minimum time interval in minutes


        public BarcodeSession()
        {
            InitializeComponent();
            formatbox.ItemsSource = Enum.GetNames(typeof(BarCoding.BarCoding.OurFormats));
        }

        private void formatbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(formatbox.SelectedItem != null)
            {
                submiter.IsEnabled = true;
                submiter.Focus();

            }
        }

        private async void submiter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int round = 0;
                Kreislauf.Models.Person person = null;

                BarCoding.BarCoding.OurFormats format = (BarCoding.BarCoding.OurFormats)Enum.Parse(typeof(BarCoding.BarCoding.OurFormats), (string)formatbox.SelectedItem);

                if (BarCoding.BarCoding.IsValidCode(submiter.Text, format))
                {
                    string scannedBarcode = submiter.Text;

                    // Check if the barcode was scanned recently
                    if (barcodeLastScanned.ContainsKey(scannedBarcode) && (DateTime.Now - barcodeLastScanned[scannedBarcode]).TotalMinutes < MIN_SCAN_INTERVAL_MINUTES)
                    {
                        // Calculate the remaining time
                        TimeSpan remainingTime = TimeSpan.FromMinutes(MIN_SCAN_INTERVAL_MINUTES) - (DateTime.Now - barcodeLastScanned[scannedBarcode]);

                        // Get remaining minutes and seconds
                        int minutesLeft = (int)remainingTime.TotalMinutes;
                        int secondsLeft = remainingTime.Seconds;

                        // Display a warning message in the label with minutes and seconds
                        warningLabel.Content = $"Ihr Barcode wurde vor Kurzem gelesen.\nSie können ihn nach {minutesLeft} Minuten {secondsLeft} Sekunden \nerneut scannen.";
                    }
                    else
                    {
                        using (AppDbContext context = new AppDbContext())
                        {
                            var barcode = context.Barcodes.FirstOrDefault(x => x.Value == scannedBarcode);
                            if (barcode != null)
                            {
                                barcode.RundenAnzahl++;
                                round = barcode.RundenAnzahl.GetValueOrDefault();
                                context.Update(barcode);
                                person = context.Personen.FirstOrDefault(x => x.Id == barcode.PersonId);
                                submiter.Text = "";

                                // Update the last scanned time
                                barcodeLastScanned[scannedBarcode] = DateTime.Now;

                                // Clear the warning label if the barcode was successfully processed
                                warningLabel.Content = string.Empty;
                            }
                            context.SaveChanges();
                        }
                        logger.Items.Add($"{person} Runden: {round}");
                    }
                }
            }

        }
    }
}
