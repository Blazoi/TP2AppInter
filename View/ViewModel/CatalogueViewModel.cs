using Model;
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

        private readonly string chemin = "C:\\Users\\jackj\\OneDrive\\Desktop\\TP2AppInteractives\\View\\ViewModel\\bibliotheque.xml";
        public List<Livre> Livres { get; } = new();
        public ICommand GoToLivre { get; }

        public CatalogueViewModel()
        {
            ChargerLivres();
            GoToLivre = new Command<Livre>(ChoisirLivreCommand);
        }

        public void ChargerLivres()
        {
            XmlDocument doc = new();
            doc.Load(chemin);
            var Nodes = doc.GetElementsByTagName("Livre");

            string? Titre;
            string? Auteur;
            string? ISBN;
            string? MaisonEdition;
            DateOnly DatePublication;
            string? Description;
            float MoyenneEvaluation;
            int NmbEvaluation;

            foreach ( XmlNode livre in Nodes )
            {
                Titre = livre["Titre"]?.InnerText;
                Auteur = livre["Auteur"]?.InnerText;
                ISBN = livre["ISBN"]?.InnerText;
                MaisonEdition = livre["MaisonEdition"]?.InnerText;
                DatePublication = DateOnly.Parse(livre["DatePublication"].InnerText);
                Description = livre["Description"].InnerText;
                MoyenneEvaluation = float.Parse(livre["MoyenneEvaluation"].InnerText);
                NmbEvaluation = int.Parse(livre["NombreEvaluations"].InnerText);

                Livre nouveauLivre = new Livre(Titre, Auteur, ISBN, MaisonEdition, DatePublication, Description, MoyenneEvaluation, NmbEvaluation);
                Livres.Add(nouveauLivre);

            }

        }

        public async void ChoisirLivreCommand(Livre livre)
        {
            XmlDocument doc = new();
            XmlElement root = doc.CreateElement("Livre");

            XmlElement titre = doc.CreateElement("Titre");
            titre.InnerText = livre.Titre;
            XmlElement auteur = doc.CreateElement("Auteur");
            auteur.InnerText = livre.Auteur;
            XmlElement isbn = doc.CreateElement("ISBN");
            isbn.InnerText = livre.ISBN;
            XmlElement maisonedition = doc.CreateElement("MaisonEdition");
            maisonedition.InnerText = livre.MaisonEdition;
            XmlElement datepublication = doc.CreateElement("DatePublication");
            datepublication.InnerText = livre.DatePublication.ToString();
            XmlElement description = doc.CreateElement("Description");
            description.InnerText = livre.Description;
            XmlElement moyenneevaluation = doc.CreateElement("MoyenneEvaluation");
            moyenneevaluation.InnerText = livre.MoyenneEvaluation.ToString();
            XmlElement nmbevaluation = doc.CreateElement("NmbEvaluation");
            nmbevaluation.InnerText = livre.NmbEvaluation.ToString();

            root.AppendChild(titre);
            root.AppendChild(auteur);
            root.AppendChild(isbn);
            root.AppendChild(maisonedition);
            root.AppendChild(datepublication);
            root.AppendChild(description);
            root.AppendChild(moyenneevaluation);
            root.AppendChild(nmbevaluation);
            
            doc.AppendChild(root);

            doc.Save("C:\\Users\\jackj\\OneDrive\\Desktop\\TP2AppInteractives\\View\\ViewModel\\LivreChoisi.xml");

            await Shell.Current.GoToAsync("Livre");
        }
    }
}
