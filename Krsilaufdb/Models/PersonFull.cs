using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kreislauf.Models
{
    public class PersonFull
    {



        private Klasse klasse;
        private Person person;
        private Schule schule;
       

        public Klasse Klasse { get => klasse; set => klasse = value; }
        public Person Person { get => person; set => person = value; }
        public Schule Schule { get => schule; set => schule = value; }
       


        public PersonFull() { }

        public PersonFull(Klasse klasse, Person person, Schule schule)
        {
            this.klasse = klasse;
            this.person = person;
            this.schule = schule;
        }

        public PersonFull(string vorname, string nachname, int alter, string schule, string klasse)
        {
            person = new Person();
            person.Vorname = vorname;
            person.Nachname = nachname;
            person.Lebensalter = alter;
            this.schule = new Schule();
            this.schule.Name = schule;
            this.klasse.Schule = this.schule;
            this.klasse.Name = klasse;
        }
        public PersonFull(string vorname, string nachname, int alter, string klasse)
        {
            person = new Person();
            person.Vorname = vorname;
            person.Nachname = nachname;
            person.Lebensalter = alter;
            this.klasse = new Klasse();
            this.klasse.Name = klasse;
        }
        public PersonFull(string vorname, string nachname, string? geburtsdatum, string klasse, string? geschlecht)
        {
            person = new Person();
            person.Vorname = vorname;
            person.Nachname = nachname;
            if(geburtsdatum != null && geburtsdatum != "null")
            {
                person.Lebensalter = DateTime.Now.Subtract(DateTime.Parse(geburtsdatum)).Days / 360;
            }
            if(geschlecht != null && geschlecht != "null")
            {
                if (geschlecht[0].ToString().ToLower() == "m")
                {
                    person.Geschlecht = true;
                }
                else
                {
                    person.Geschlecht = false;
                }
            }
            this.klasse = new Klasse();
            this.klasse.Name = klasse;
        }
    }
}
