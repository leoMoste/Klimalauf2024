using Kreislauf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvM.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        private PersonFull _personFull;

        public PersonFull PersonFull
        {
            get => _personFull;
            set
            {
                _personFull = value;
                OnPropertyChanged(nameof(PersonFull));
            }
        }

        public DetailViewModel(PersonFull personFull)
        {
            PersonFull = personFull;
        }
    }
}
