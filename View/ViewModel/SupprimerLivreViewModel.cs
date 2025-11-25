using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ViewModel
{
    public class SupprimerLivreViewModel
    {
        private readonly string cheminBiblio = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName,
            "..", "Model", "bibliotheque.xml");
        public string ISBN { get; set; }
        public ICommand SupprimerLivre { get; }
        public ICommand GoToCatalogue { get; }
        public SupprimerLivreViewModel()
        {
            SupprimerLivre = new Command(SupprimerLivreCommand);
            GoToCatalogue = new Command(PageCatalogueCommand);
        }
        public async void SupprimerLivreCommand()
        {
            var doc = XDocument.Load(cheminBiblio);

            var livreaSupprimer = doc.Descendants("Livre").FirstOrDefault(livre => (string)livre.Element("ISBN") == ISBN);
            if (livreaSupprimer != null)
            {
                livreaSupprimer.Remove();
                doc.Save(cheminBiblio);
            } else
            {
                await Shell.Current.DisplayAlert("Erreur", "Cet ISBN n'existe pas.", "ok");
            }
        }
        public async void PageCatalogueCommand()
        {
            await Shell.Current.GoToAsync("Catalogue");
        }
    }
}