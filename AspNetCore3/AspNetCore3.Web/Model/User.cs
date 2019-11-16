using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3.Web.Model
{
    public class User
    {
        [Column(TypeName = "nvarchar(150)")]
        public string UserID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string AccessKey { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
