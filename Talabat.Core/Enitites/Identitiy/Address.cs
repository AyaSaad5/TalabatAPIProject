namespace Talabat.Core.Enitites.Identitiy
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        //foreign key
        public string AppUserId { get; set; }
        public AppUser User { get; set; } // [one]
    }
}