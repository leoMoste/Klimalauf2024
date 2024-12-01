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
    public class KlasseViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private Klasse _selectedKlasse;

        public ObservableCollection<Klasse> KlasseList {get; set;}

        public Klasse SelectedKlasse
        {
            get => _selectedKlasse;
            set
            {
                _selectedKlasse = value;
                OnPropertyChanged(nameof(SelectedKlasse)); // Benachrichtigt die View über die Änderung
            }
        }

        public KlasseViewModel() 
        {
            _context = new AppDbContext();
            KlasseList = new ObservableCollection<Klasse>(_context.Klassen.ToList()); // Lädt die Personen aus der Datenbank
        }

        private async void GetAllKlassenAsync()
        {
            List<Klasse> klassen = await _context.Klassen.ToListAsync();
            KlasseList = new ObservableCollection<Klasse>(klassen);
        }


        public async Task AddKlasse(Klasse newKlasse)
        {
            _context.Klassen.Add(newKlasse); 
            await _context.SaveChangesAsync(); 
            KlasseList.Add(newKlasse); 
        }


        public async Task UpdateKlasse(Klasse updatedKlasse)
        {
            Klasse klasse = await _context.Klassen.FirstOrDefaultAsync(k => k.Id == updatedKlasse.Id); // Sucht die Person in der Datenbank
            if (klasse != null)
            {
                // Aktualisiert die Personendaten
                klasse.Name = updatedKlasse.Name;
                klasse.Schule_Id = updatedKlasse.Schule_Id;
         

                await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank

                // Aktualisiert die ObservableCollection
                int index = Klasse.IndexOf(klasse);
                if (index >= 0)
                {
                    KlasseList[index] = klasse;
                }

            }
        }


        public async Task DeleteKlasseAsync(int klasseId)
        {
            Klasse klasse = await _context.Klassen.FirstOrDefaultAsync(k => k.Id == klasseId);
            if (klasse != null)
            {
                _context.Klassen.Remove(klasse);
                await _context.SaveChangesAsync();
                KlasseList.Remove(klasse);
            }
        }


        public async Task<Klasse> GetKlasseAsync(int klasseId)
        {
            return await _context.Klassen.FirstOrDefaultAsync(k => k.Id == klasseId);
        }



    }
}
