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
    public class MainViewModel : BaseViewModel
    {

        private ObservableCollection<PersonFull> _personenFull;
        private PersonFull _selectedPersonFull;
        private bool _isPersonFullSelected;
        public PersonViewModel PersonViewModel { get; }



        public ObservableCollection<PersonFull> PersonenFull
        {
            get => _personenFull;
            set
            {
                _personenFull = value;
                OnPropertyChanged(nameof(PersonenFull));
            }
        }

        public PersonFull SelectedPersonFull
        {
            get => _selectedPersonFull;
            set
            {
                _selectedPersonFull = value;
                IsPersonFullSelected = _selectedPersonFull != null;
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

        public MainViewModel()
        {
            LoadPersonFullsAsync();
            PersonViewModel = new PersonViewModel();

        }

        private async Task LoadPersonFullsAsync()
        {
            using (var context = new AppDbContext())
            {
                var persons = await context.Personen.ToListAsync();
                var personenFullList = persons.Select(person =>
                {
                    var klasse = context.Klassen.Find(person.Klasse_Id);
                    var schule = context.Schulen.Find(klasse.Schule_Id);
                    return new PersonFull(klasse, person, schule);
                }).ToList();

                PersonenFull = new ObservableCollection<PersonFull>(personenFullList);
            }
        }

        public async Task FilterPersonsByNameAsync(string name)
        {
            var filteredPersons = await PersonViewModel.FilterByName(name);

            var personenFullList = filteredPersons.Select(person =>
            {
                var klasse = person.Klasse ?? new Klasse();  // Fallback to an empty Klasse if it's null
                var schule = klasse.Schule ?? new Schule();  // Fallback to an empty Schule if it's null
                return new PersonFull(klasse, person, schule);
            }).ToList();

            PersonenFull = new ObservableCollection<PersonFull>(personenFullList);
            OnPropertyChanged(nameof(PersonenFull));
        }

    }
}
