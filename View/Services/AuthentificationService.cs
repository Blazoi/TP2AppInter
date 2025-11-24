using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Services
{
    public class AuthentificationService
    {
        private readonly string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bibliotheque.xml");
        public bool Authentifier(string Email, string MotDePasse)
        {
            var doc = XDocument.Load(chemin);
            
            XElement Compte = doc.Descendants("Compte").FirstOrDefault(x => (string)x.Element("Email") == Email);
            
            if (Compte == null) return false;

            if (MotDePasse == (string)Compte.Element("MotDePasse") && Email == "admin@exemple.com")
            {
                return true;
            }

            return false;
        }
    }
}
