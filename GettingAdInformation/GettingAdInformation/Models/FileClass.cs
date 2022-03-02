using DocumentShare.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentShare.Models
{
    public class FileClass
    {
        public int Id { get; set; }
        //public string UserId { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
        //public AppIdentityUser User { get; set; }
    }
}
