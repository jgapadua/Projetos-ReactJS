using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public string cpf_user { get; set; }

        public string name_user { get; set; }
        public string login_user { get; set; }
        public string password_user { get; set; }
        public DateTime datebirth_user { get; set; }

        public Boolean adm_user { get; set; }
    }
}