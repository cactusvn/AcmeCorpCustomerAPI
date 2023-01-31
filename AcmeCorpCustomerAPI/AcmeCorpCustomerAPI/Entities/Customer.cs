using System;
using System.Collections.Generic;

namespace AcmeCorpCustomerAPI.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime TS { get; set; }
        public bool Active { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
