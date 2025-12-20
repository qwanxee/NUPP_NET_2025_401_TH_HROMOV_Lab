using System;

namespace ECommerce.Common
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public Customer(string name, string email)
        {
            Id = Guid.NewGuid();
            FullName = name;
            Email = email;
        }
    }
}