using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Library Library { get; set; }
    }
}
