using Aiesec.Data.Model.BusinessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec.Data.DataSeed
{
    public static class ModelBuilderExtensions
    {
        public static void CitySeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Sarajevo", Postcode = "71000" },
                new City { Id = 2, Name = "Mostar", Postcode = "88000" },
                new City { Id = 3, Name = "Zenica", Postcode = "72000" },
                new City { Id = 4, Name = "Banja Luka", Postcode = "78000" },
                new City { Id = 5, Name = "Sarajevo", Postcode = "71000" },
                new City { Id = 6, Name = "Tuzla", Postcode = "77000" }
            );
            
        }
    }
}
