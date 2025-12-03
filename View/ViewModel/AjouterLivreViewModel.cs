using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Model;

namespace ViewModel
{
    public class AjouterLivreViewModel
    {
        private readonly string cheminBiblio = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..",
            "Model", "bibliotheque.xml");
        public ICommand GoToCatalogue { get; }
        public ICommand AjouterLivre { get; }

        public string Titre { get; set; } = "";
        public string Auteur { get; set; } = "";
        public string ISBN { get; set; } = "";
        public string MaisonEdition { get; set; } = "";
        public string DatePublication { get; set; } = "1001 - 01 - 01";
        public string Description { get; set; } = "";

        public AjouterLivreViewModel()
        {
            GoToCatalogue = new Command(PageCatalogueCommand);
            AjouterLivre = new Command(AjouterLivreCommand);
        }
        public async void PageCatalogueCommand()
        {
            await Shell.Current.GoToAsync("Catalogue");
        }
        public void AjouterLivreCommand()
        {
            XDocument docBiblio = XDocument.Load(cheminBiblio);

            var nouveauLivre = new XElement("Livre",
                new XElement("Titre", Titre),
                new XElement("Auteur", Auteur),
                new XElement("ISBN", ISBN),
                new XElement("MaisonEdition", MaisonEdition),
                new XElement("DatePublication", DatePublication),
                new XElement("Description", Description),
                new XElement("MoyenneEvaluation", 0),
                new XElement("NombreEvaluations", 0));

            docBiblio.Root.Element("Livres").Add(nouveauLivre);

            Debug.WriteLine(nouveauLivre);

            docBiblio.Save(cheminBiblio);
        }
    }
}
