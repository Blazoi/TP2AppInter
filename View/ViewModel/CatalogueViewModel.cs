using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;

namespace ViewModel
{
    public class CatalogueViewModel
    {

        private readonly string cheminBiblio = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model","bibliotheque.xml");
        private readonly string cheminUser = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "IsAdmin.xml");
        private readonly string cheminLivreChoisi = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "LivreChoisi.xml");
        private readonly string cheminFavoris = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "Favoris.xml");

        public List<Livre> Livres { get; } = new();
        public bool IsAdmin { get; set; }
        public ICommand GoToLivre { get; }
        public ICommand GoToFavoris { get; }
        public ICommand GoToAjout { get; }
        public ICommand GoToSuppression { get; }
        public ICommand GoToComptes { get; }

        public CatalogueViewModel()
        {
            CheckIfAdmin();
            ChargerLivres();
            ChargerFavoris();
            GoToLivre = new Command<Livre>(ChoisirLivreCommand);
            GoToFavoris = new Command(PageFavorisCommand);
            GoToAjout = new Command(PageAjouterCommand);
            GoToSuppression = new Command(PageSupprimerCommand);
            GoToComptes = new Command(PageGestionCommand);
        }

        public void ChargerFavoris()
        {
            var docBiblio = XDocument.Load(cheminBiblio);
            var docFavoris = new XDocument(new XElement("Livres"));

            var livres = docBiblio.Descendants("Livre").Where(livre =>(double)livre.Element("MoyenneEvaluation") >= 4);

            foreach (var livre in livres)
            {
                docFavoris.Root.Add(new XElement(livre));
            }

            docFavoris.Save(cheminFavoris);
        }
        public void ChargerLivres()
        {
            var doc = XDocument.Load(cheminBiblio);

            var livres = doc.Descendants("Livre").Select(livre => new Livre(
                (string)livre.Element("Titre"),
                (string)livre.Element("Auteur"),
                (string)livre.Element("ISBN"),
                (string)livre.Element("MaisonEdition"),
                DateOnly.Parse( (string)livre.Element("DatePublication") ),
                (string)livre.Element("Description"),
                (double)livre.Element("MoyenneEvaluation"),
                (int)livre.Element("NombreEvaluations")
                ));

            foreach (Livre livre in livres)
            {
                Livres.Add(livre);
            }

        }

        public async void ChoisirLivreCommand(Livre livre)
        {

            XDocument doc = new XDocument(new XElement("Livre"));

            doc.Root.Add(new XElement("Titre", livre.Titre));
            doc.Root.Add(new XElement("Auteur", livre.Auteur));
            doc.Root.Add(new XElement("ISBN", livre.ISBN));
            doc.Root.Add(new XElement("MaisonEdition", livre.MaisonEdition));
            doc.Root.Add(new XElement("DatePublication", livre.DatePublication));
            doc.Root.Add(new XElement("Description", livre.Description));
            doc.Root.Add(new XElement("MoyenneEvaluation", livre.MoyenneEvaluation));
            doc.Root.Add(new XElement("NombreEvaluations", livre.NmbEvaluation));

            doc.Save(cheminLivreChoisi);

            await Shell.Current.GoToAsync("Livre");
        }

        public void CheckIfAdmin()
        {
            var doc = XDocument.Load(cheminUser);
            string email = (string) doc.Root;

            if (email == "admin@exemple.com") IsAdmin = true;
        }

        public async void PageFavorisCommand()
        {
            await Shell.Current.GoToAsync("FavorisPage");
        }
        public async void PageAjouterCommand()
        {
            await Shell.Current.GoToAsync("AjouterLivrePage");
        }
        public async void PageSupprimerCommand()
        {
            await Shell.Current.GoToAsync("SupprimerLivrePage");
        }
        public async void PageGestionCommand()
        {
            await Shell.Current.GoToAsync("GestionComptesPage");
        }
    }
}
