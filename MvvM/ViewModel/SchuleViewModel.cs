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
    public class SchuleViewModel : BaseViewModel
    {
        private readonly AppDbContext _context; // Kontext für die Datenbankverbindung
        private Schule _selectedSchule;


        public ObservableCollection<Schule> Schulen { get; set; }


        public Schule SelectedSchule
        {
            get => _selectedSchule;
            set
            {
                _selectedSchule = value;
                OnPropertyChanged(nameof(SelectedSchule)); // Benachrichtigt die View über die Änderung
            }
        }


        public SchuleViewModel() 
        {
            _context = new AppDbContext(); // Initialisiert den Datenbankkontext
            Schulen = new ObservableCollection<Schule>(_context.Schulen.ToList()); // Lädt die Personen aus der Datenbank

        }

        private async void GetAllSchulenAsync()
        {
            List<Schule> schulen = await _context.Schulen.ToListAsync();
            Schulen = new ObservableCollection<Schule>(schulen);
        }


        public async Task AddSchule(Schule newSchule)
        {
            _context.Schulen.Add(newSchule); // Fügt die neue Person dem Datenbankkontext hinzu
            await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank
            Schulen.Add(newSchule); // Fügt die neue Person der ObservableCollection hinzu
        }


        public async Task UpdateSchule(Schule updatedSchule)
        {
            Schule schule = await _context.Schulen.FirstOrDefaultAsync(sh => sh.Id == updatedSchule.Id); // Sucht die Person in der Datenbank
            if (schule != null)
            {
                // Aktualisiert die Personendaten
                schule.Name = updatedSchule.Name;
                schule.Stadt = updatedSchule.Stadt;
                

                await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank

                // Aktualisiert die ObservableCollection
                int index = Schulen.IndexOf(schule);
                if (index >= 0)
                {
                    Schulen[index] = schule;
                }

            }
        }


        public async Task DeletePersonAsync(int schuleId)
        {
            Schule schule = await _context.Schulen.FirstOrDefaultAsync(sh => sh.Id == schuleId); // Sucht die Person in der Datenbank

            if (schule != null)
            {
                _context.Schulen.Remove(schule);
                await _context.SaveChangesAsync();
                Schulen.Remove(schule);
            }
        }


        public async Task<Schule> GetSchuleAsync(int schuleId)
        {
            return await _context.Schulen.FirstOrDefaultAsync(sh => sh.Id == schuleId);
        }


    }
}
