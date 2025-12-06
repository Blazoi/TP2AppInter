using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Model;

namespace ViewModel
{
    public class GestionComptesViewModel
    {
        private readonly string cheminBiblio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bibliotheque.xml");
        
        public ICommand GoToCatalogue { get; }

        public List<Utilisateur> ListeDeComptes { get; set; }

        public GestionComptesViewModel()
        {
            ChargerComptes();
            GoToCatalogue = new Command(CataloguePageCommand);
        }
        public async void CataloguePageCommand()
        {
            await Shell.Current.GoToAsync("Catalogue");
        }
        public void ChargerComptes()
        {
            XDocument doc = XDocument.Load(cheminBiblio);
            var comptes = doc.Descendants("Compte").Select( compte => new Utilisateur(
                (string) compte.Element("Email"),
                (string) compte.Element("MotDePasse"),
                (string) compte.Element("Nom"),
                (string) compte.Element("Prenom")) );

            ListeDeComptes = comptes.ToList();
        }
    }
}
