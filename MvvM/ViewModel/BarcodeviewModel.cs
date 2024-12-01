using Kreislauf.Data;
using Kreislauf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvM.ViewModel
{
    public class BarcodeviewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private Barcode _selectedBarcode;

        public ObservableCollection<Barcode> Barcodes { get; set; }


        public Barcode SelectedBarcode
        {
            get => _selectedBarcode;
            set
            {
                _selectedBarcode = value;
                OnPropertyChanged(nameof(SelectedBarcode)); // Benachrichtigt die View über die Änderung
            }
        }


        public BarcodeviewModel() 
        {
            _context = new AppDbContext(); // Initialisiert den Datenbankkontext
            Barcodes = new ObservableCollection<Barcode>(_context.Barcodes.ToList()); // Lädt die Personen aus der Datenbank
        }


        private async void GetAllBarcodesAsync()
        {
            var barcodes = await _context.Barcodes.ToListAsync();
            Barcodes = new ObservableCollection<Barcode>(barcodes);
        }

        public async Task AddBarcode(Barcode newBarcode)
        {
            _context.Barcodes.Add(newBarcode); // Fügt die neue Person dem Datenbankkontext hinzu
            await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank
            Barcodes.Add(newBarcode); // Fügt die neue Person der ObservableCollection hinzu
        }

        public async Task UpdateBarcode(Barcode updatedBarcode)
        {
            Barcode barcode = await _context.Barcodes.FirstOrDefaultAsync(br => br.Id == updatedBarcode.Id); // Sucht die Person in der Datenbank
            if (barcode != null)
            {
                // Aktualisiert die Personendaten
                barcode.PersonId = updatedBarcode.PersonId;

               // barcode.QRCodeImage = updatedBarcode.QRCodeImage;

                barcode.Value = updatedBarcode.Value;
// 9fef628363e7c4a235fa3323a29dba9dc42d3b82
                barcode.Type = updatedBarcode.Type;
              
                await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank

                // Aktualisiert die ObservableCollection
                int index = Barcodes.IndexOf(barcode);
                if (index >= 0)
                {
                    Barcodes[index] = barcode;
                }

            }
        }

        public async Task DeleteBarcodeAsync(int barcodeId)
        {
            var barcode = await _context.Barcodes.FirstOrDefaultAsync(br => br.Id == barcodeId);
            if (barcode != null)
            {
                _context.Barcodes.Remove(barcode);
                await _context.SaveChangesAsync();
                Barcodes.Remove(barcode);
            }
        }


        public async Task<Barcode> GetBarcodeAsync(int barcodeId)
        {
            return await _context.Barcodes.FirstOrDefaultAsync(br => br.Id == barcodeId);
        }
        public async Task<Barcode> GetBarcodeAsyncByValue(string val)
        {
            return await _context.Barcodes.FirstOrDefaultAsync(br => br.Value == val);
        }



    }
}
