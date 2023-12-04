using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_App.Models
{
    public class ContactDetail
    {
        public string ID { get; set; } 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string School { get; set; }  
        public string Course { get; set; }  
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }

        public ContactDetail(string id, string lastname, string firstname, string school, string course, string email, string phone, string type)
        {
            ID = id;
            LastName = lastname;
            FirstName = firstname;
            School = school; 
            Course = course;
            Email = email;
            Phone = phone;  
            Type = type;
        }
        
        
    }
}
