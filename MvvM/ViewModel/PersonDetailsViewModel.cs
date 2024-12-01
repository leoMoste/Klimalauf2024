using Kreislauf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvM.ViewModel
{
    public class PersonDetailsViewModel: BaseViewModel
    {

        private readonly PersonFull _personFull;

        public PersonDetailsViewModel(PersonFull personFull)
        {
            _personFull = personFull;
        }

        public PersonFull PersonFull => _personFull;
    }
}
