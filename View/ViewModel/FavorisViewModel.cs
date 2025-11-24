using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace ViewModel
{
    public class FavorisViewModel
    {
        private readonly string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bibliotheque.xml");
        public List<Livre> LivresFavoris { get; } = new();
        public ICommand GoToLivre { get; }
        public ICommand GoToCatalogue { get; }

        public FavorisViewModel()
        {
            ChargerFavoris();
            GoToLivre = new Command(PageLivreCommand);
            GoToCatalogue = new Command(PageCatalogueCommand);
        }

        public void ChargerFavoris()
        {
            var doc = XDocument.Load(chemin);

            var livres = doc.Descendants("Livre").Where(livre => (double) livre.Element("MoyenneEvaluation") >= 4).Select(
                livre => new Livre(
                    (string)livre.Element("Titre"),
                    (string)livre.Element("Auteur"),
                    (string)livre.Element("ISBN"),
                    (string)livre.Element("MaisonEdition"),
                    DateOnly.Parse((string)livre.Element("DatePublication")),
                    (string)livre.Element("Description"),
                    (double)livre.Element("MoyenneEvaluation"),
                    (int)livre.Element("NombreEvaluations")
                ));

            foreach (Livre livre in livres)
            {
                LivresFavoris.Add(livre);
            }
        }

        public async void PageLivreCommand()
        {
            await Shell.Current.GoToAsync("Livre");
        }
        public async void PageCatalogueCommand()
        {
            await Shell.Current.GoToAsync("Catalogue");
        }
    }
}
