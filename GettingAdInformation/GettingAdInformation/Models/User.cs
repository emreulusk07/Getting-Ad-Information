using DocumentShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GettingAdInformation.Models
{
    public class User
    {
        public User()
        {
            FileClasses = new List<FileClass>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }

        public List<FileClass> FileClasses { get; set; }
    }
}
