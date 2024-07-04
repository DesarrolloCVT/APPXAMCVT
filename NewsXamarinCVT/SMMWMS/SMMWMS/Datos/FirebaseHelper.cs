using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
namespace SMMWMS.Datos
{
  public class FirebaseHelper
    {
        FirebaseClient firebase=new FirebaseClient("https://testcvt-89971-default-rtdb.firebaseio.com/");

        public async Task<List<Person>> GetAllPersons()
        {

            return (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Select(item => new Person
              {
                  Name = item.Object.Name,
                  PersonId = item.Object.PersonId
              }).ToList();
        }
        public async Task<List<Cars>> GetCars()
        {

            return (await firebase
              .Child("Cars")
              .OnceAsync<Cars>()).Select(item => new Cars
              {
                  carid = item.Object.carid,
                  carName = item.Object.carName
              }).ToList();
        }
        public async Task AddPerson(int personId, string name)
        {

            await firebase
              .Child("Persons")
              .PostAsync(new Person() { PersonId = personId, Name = name });
        }
        public async Task AddCar(int personId, string name)
        {

            await firebase
              .Child("Cars")
              .PostAsync(new Cars() { carid = personId, carName = name });
        }
        public async Task<Person> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.PersonId == personId).FirstOrDefault();
        }
        public async Task<Cars> GetCars(int personId)
        {
            var allcars = await GetCars();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allcars.Where(a => a.carid == personId).FirstOrDefault();
        }


    }

}
