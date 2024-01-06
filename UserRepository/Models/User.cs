using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRepository.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Language { get; set; }
        public string Culture { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set;}
    }
}
