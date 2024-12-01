using Kreislauf.Data;
using Kreislauf.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kreislauf
{
    public class CsvToDB
    {
        List<PersonFull> personfulllist;
        AppDbContext dbContext;
        public CsvToDB(List<PersonFull> people, AppDbContext db)
        {
            this.personfulllist = people;
            this.dbContext = db;
        }
        public void Save()
        {
            //TO-DO
            try
            {
                //Schule

                //Klasse

                //Person
                foreach (PersonFull full in personfulllist)
                {
                    var person = dbContext.Personen.FirstOrDefault(pp => pp.Nachname == full.Person.Nachname && pp.Vorname == full.Person.Vorname && pp.Lebensalter == full.Person.Lebensalter);

                    if (person != null)
                    {
                        full.Person = person;
                    }
                    else
                    {
                        dbContext.Add(full.Person);
                    }
                }
                dbContext.SaveChanges();


            }

            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        throw new Exception($"Inner inner exception: {ex.InnerException.InnerException.Message}");
                    }
                    throw new Exception($"Inner exception: {ex.InnerException.Message}");

                }
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }
        public void Save(Schule importschule)
        {
            

            try
            {

                //Schule
                var schule = dbContext.Schulen.FirstOrDefault(sch => sch.Name == importschule.Name && sch.Stadt == importschule.Stadt);
                if (schule == null)
                {
                    dbContext.Add(importschule);
                    dbContext.SaveChanges();

                }
                //Get DB id
                schule = dbContext.Schulen.FirstOrDefault(sch => sch.Name == importschule.Name && sch.Stadt == importschule.Stadt);





                //Add klasse to DB
                List<Klasse> filteredklass = getDistinctKlass();
                
                for(int i = 0; i < filteredklass.Count; i++)
                {
                    var klasse = dbContext.Klassen.FirstOrDefault(x => x.Name == filteredklass[i].Name);
                    if(klasse == null)
                    {
                        filteredklass[i].Schule_Id = schule.Id;
                        dbContext.Add(filteredklass[i]);
                    }
                }
                dbContext.SaveChanges();

                for(int i = 0; i < filteredklass.Count; i++)
                {
                    var klasse = dbContext.Klassen.FirstOrDefault(x => x.Name == filteredklass[i].Name);
                    filteredklass[i] = klasse;
                }
                
                //Klasse, id problem

               
                
                
                
                
                //Person create
                foreach (PersonFull full in personfulllist)
                {
                    var person = dbContext.Personen.FirstOrDefault(pp => pp.Nachname == full.Person.Nachname && pp.Vorname == full.Person.Vorname && pp.Lebensalter == full.Person.Lebensalter);

                    if (person == null)
                    {
                        full.Person.Klasse_Id = filteredklass[filteredklass.FindIndex(x => x.Name == full.Klasse.Name)].Id;
                        dbContext.Add(full.Person);
                    }
                    
                }
                dbContext.SaveChanges();
                //Person update
                foreach (PersonFull full in personfulllist)
                {
                    int k_id = filteredklass[filteredklass.FindIndex(x => x.Name == full.Klasse.Name)].Id;
                    var person = dbContext.Personen.FirstOrDefault(pp => pp.Nachname == full.Person.Nachname && pp.Vorname == full.Person.Vorname && pp.Lebensalter == full.Person.Lebensalter);
                    person.Klasse_Id = k_id;
                    dbContext.Update(person);
                }
                dbContext.SaveChanges();


            }

           
            
            
            
            
            
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        throw new Exception($"Inner inner exception: {ex.InnerException.InnerException.Message}");
                    }
                    throw new Exception($"Inner exception: {ex.InnerException.Message}");

                }
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }
        private List<Klasse> getDistinctKlass()
        {
            List<Klasse> kl = new List<Klasse>();
            foreach(PersonFull pp in personfulllist)
            {
                kl.Add(pp.Klasse);
            }
            kl = kl.GroupBy(x => x.Name).Select(x => x.First()).ToList();
                
            return kl;
        }
        








    }
}
