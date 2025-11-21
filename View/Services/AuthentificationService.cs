using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Services
{
    public class AuthentificationService
    {
        private readonly string chemin = "C:\\Users\\jackj\\OneDrive\\Desktop\\TP2AppInteractives\\View\\ViewModel\\bibliotheque.xml";
        public bool Authentifier(string Email, string MotDePasse)
        {
            var doc = XDocument.Load(chemin);
            
            XElement Compte = doc.Descendants("Compte").FirstOrDefault(x => (string)x.Element("Email") == Email);
            
            if (Compte == null) return false;

            if (MotDePasse == (string)Compte.Element("MotDePasse")) return true;

            return false;
        }
    }
}
