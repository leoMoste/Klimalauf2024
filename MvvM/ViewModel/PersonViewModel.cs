using System.Collections.ObjectModel; 
using System.Linq; 
using Kreislauf.Data;
using Kreislauf.Models;
using Microsoft.EntityFrameworkCore; 
using MvvM.ViewModel;

namespace MvvM.ViewModel
{
    public class PersonViewModel : BaseViewModel
    {
        private readonly AppDbContext _context; // Kontext für die Datenbankverbindung
        private Person _selectedPerson; // Hält die aktuell ausgewählte Person


        public ObservableCollection<Person> Personen { get; set; }
        private ObservableCollection<Person> _allPersonen;



        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson)); // Benachrichtigt die View über die Änderung
            }
        }


        public PersonViewModel()
        {
            _context = new AppDbContext(); // Initialisiert den Datenbankkontext
            Personen = new ObservableCollection<Person>(_context.Personen.ToList()); // Lädt die Personen aus der Datenbank
            LoadPersonsAsync();
        }

        private async Task LoadPersonsAsync()
        {
            var persons = await _context.Personen.ToListAsync();
            _allPersonen = new ObservableCollection<Person>(persons);
            Personen = new ObservableCollection<Person>(persons);
        }

        private async void GetAllPersonsAsync()
        {
            var persons = await _context.Personen.ToListAsync();
            Personen = new ObservableCollection<Person>(persons);
        }


        public async Task AddPerson(Person newPerson)
        {
            _context.Personen.Add(newPerson); // Fügt die neue Person dem Datenbankkontext hinzu
            await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank
            Personen.Add(newPerson); // Fügt die neue Person der ObservableCollection hinzu
        }


        public async Task UpdatePerson(Person updatedPerson)
        {
            Person person = await _context.Personen.FirstOrDefaultAsync(p => p.Id == updatedPerson.Id); // Sucht die Person in der Datenbank
            if (person != null)
            {
                // Aktualisiert die Personendaten
                person.Vorname = updatedPerson.Vorname;
                person.Nachname = updatedPerson.Nachname;
                person.Lebensalter = updatedPerson.Lebensalter;
                person.Geschlecht = updatedPerson.Geschlecht;
                person.Klasse_Id = updatedPerson.Klasse_Id;
             

                await _context.SaveChangesAsync(); // Speichert die Änderungen in der Datenbank

                // Aktualisiert die ObservableCollection
                int index = Personen.IndexOf(person);
                if (index >= 0)
                {
                    Personen[index] = person;
                }

            }
        }


        public async Task DeletePersonAsync(int personId)
        {
            var person = await _context.Personen.FirstOrDefaultAsync(p => p.Id == personId);
            if (person != null)
            {
                _context.Personen.Remove(person);
                await _context.SaveChangesAsync();
                Personen.Remove(person);
            }
        }


        public async Task<Person> GetPersonAsync(int personId)
        {
            return await _context.Personen.FirstOrDefaultAsync(p => p.Id == personId);
        }

        //Filter für Barcodes(barcode vorhanden oder nicht)

        public async Task<ObservableCollection<Person>> FilterByName(string nachName)
        {
            if (string.IsNullOrWhiteSpace(nachName))
            {
                return new ObservableCollection<Person>(_allPersonen);
            }
            else
            {
                var filteredPersons = _allPersonen
                    .Where(p => p.Nachname.Contains(nachName, StringComparison.OrdinalIgnoreCase) || p.Vorname.Contains(nachName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return new ObservableCollection<Person>(filteredPersons);
            }
        }

    }

}