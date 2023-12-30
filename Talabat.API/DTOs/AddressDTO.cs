using System.ComponentModel.DataAnnotations;
using Talabat.Core.Enitites.Identitiy;

namespace Talabat.API.DTOs
{
    public class AddressDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Street { get; set; }
      
    }
}
