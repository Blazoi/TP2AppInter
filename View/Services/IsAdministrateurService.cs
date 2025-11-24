using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public class IsAdministrateurService
    {
        public IsAdministrateurService() { }
        public bool IsAdmin(string Email)
        {
            if (Email == "admin@exemple.com") return true;
            return false;
        }
    }
}
