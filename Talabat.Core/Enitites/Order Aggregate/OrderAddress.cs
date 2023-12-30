using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enitites.Order_Aggregate
{

    public class OrderAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public OrderAddress(string firstName, string lastName, string country, string city, string street)
        {
            FirstName= firstName;
            LastName= lastName;
            Country= country;
            City= city;
            Street= street;
        }
        public OrderAddress()
        {
            
        }

    }
}
