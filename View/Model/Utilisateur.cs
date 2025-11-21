using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Utilisateur
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Nom {  get; set; }
        public string Prenom { get; set; }

        public Utilisateur (string email, string mdp, string nom, string prenom)
        {
            Email = email;
            MotDePasse = mdp;
            Nom = nom;
            Prenom = prenom;
        }
    }
}
