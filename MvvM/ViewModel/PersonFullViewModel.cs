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
    public class PersonFullViewModel: BaseViewModel
    {
        private readonly AppDbContext _context;
        private PersonFull _selectedPersonFull;
        private bool _isPersonFullSelected;

        public ObservableCollection<PersonFull> PersonenFull { get; set; }


        public PersonFull SelectedPersonFull
        {
            get => _selectedPersonFull;
            set
            {
                _selectedPersonFull = value;
                OnPropertyChanged(nameof(SelectedPersonFull));
            }
        }

        public bool IsPersonFullSelected
        {
            get => _isPersonFullSelected;
            set
            {
                _isPersonFullSelected = value;
                OnPropertyChanged(nameof(IsPersonFullSelected));
            }
        }


        public PersonFullViewModel()
        {
            _context = new AppDbContext();
            PersonenFull = new ObservableCollection<PersonFull>();
            LoadPersonFullsAsync();
        }

        private async Task LoadPersonFullsAsync()
        {
            // Example fetching method; replace with your actual method
            var persons = await _context.Personen.ToListAsync();
            foreach (var person in persons)
            {
                var klasse = await _context.Klassen.FindAsync(person.Klasse_Id);
                var schule = await _context.Schulen.FindAsync(klasse.Schule_Id);
                

                var personFull = new PersonFull(klasse, person, schule);
                PersonenFull.Add(personFull);
            }
        }

        public async Task AddPersonFullAsync(PersonFull newPersonFull)
        {
            _context.Personen.Add(newPersonFull.Person); // Add the Person
            _context.Klassen.Add(newPersonFull.Klasse);  // Add the Klasse
            _context.Schulen.Add(newPersonFull.Schule);  // Add the Schule

            await _context.SaveChangesAsync(); // Save changes to the database
            PersonenFull.Add(newPersonFull); // Update ObservableCollection
        }

        public async Task UpdatePersonFullAsync(PersonFull updatedPersonFull)
        {
            _context.Personen.Update(updatedPersonFull.Person); // Update the Person
            _context.Klassen.Update(updatedPersonFull.Klasse);  // Update the Klasse
            _context.Schulen.Update(updatedPersonFull.Schule);  // Update the Schule

            await _context.SaveChangesAsync(); // Save changes to the database
                                               // Update ObservableCollection if needed
        }


    }
}
