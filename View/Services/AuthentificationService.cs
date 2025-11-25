using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Services
{
    public class AuthentificationService
    {
        private readonly string cheminBiblio = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "bibliotheque.xml");
        private readonly string cheminUser = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "IsAdmin.xml");
        public bool Authentifier(string Email, string MotDePasse)
        {
            var doc = XDocument.Load(cheminBiblio);
            
            XElement Compte = doc.Descendants("Compte").FirstOrDefault(x => (string)x.Element("Email") == Email);
            
            if (Compte == null) return false;

            if (MotDePasse == (string)Compte.Element("MotDePasse"))
            {
                XDocument docUser = new();
                var element = new XElement("Email", Email);
                docUser.Add(element);
                docUser.Save(cheminUser);

                return true;
            }

            return false;
        }
    }
}
