using HackYeahGWIZDapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppContext
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Animals.Any())
            {
                // Dodaj zwierzę "dog" do tabeli Animals, jeśli tabela jest pusta
                var animals = new List<Animal>()
                {
                   new Animal(){Name = "Pies"},
                   new Animal(){Name = "Jeżozwierz"},
                   new Animal(){Name = "Jeż"},
                   new Animal(){Name = "Żubr"},
                   new Animal(){Name = "Bóbr"},
                   new Animal(){Name = "Łoś"},
                   new Animal(){Name = "Lis"},
                   new Animal(){Name = "Wilk"},
                   new Animal(){Name = "Kuna"},
                   new Animal(){Name = "Koń"},
                   new Animal(){Name = "Wydra"},
                   new Animal(){Name = "Ryjówka"},
                   new Animal(){Name = "Zając"},
                   new Animal(){Name = "Tygrys"},
                   new Animal(){Name = "Sokół"},
                   new Animal(){Name = "Lampart"},
                   new Animal(){Name = "Kaczka"},
                   new Animal(){Name = "Kot"},
                   new Animal(){Name = "Sarna"},
                   new Animal(){Name = "Dzik"}

                };

                foreach (var animal in animals)
                {
                    context.Animals.Add(animal);
                }
                context.SaveChanges();
            }
        }
    }
}
