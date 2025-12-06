using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ViewModel
{
    public class SupprimerLivreViewModel
    {
        private readonly string cheminBiblio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bibliotheque.xml");

        private string isbn;

        public event PropertyChangedEventHandler PropertyChanged;
        public string ISBN
        {
            get { return isbn; }
            set
            {
                isbn = value;
                OnPropertyChanged();
            }
        }


        public ICommand SupprimerLivre { get; }
        public ICommand GoToCatalogue { get; }
        public SupprimerLivreViewModel()
        {
            SupprimerLivre = new Command(SupprimerLivreCommand);
            GoToCatalogue = new Command(PageCatalogueCommand);
        }
        public void SupprimerLivreCommand()
        {
            var doc = XDocument.Load(cheminBiblio);

            var livreaSupprimer = doc.Descendants("Livre").FirstOrDefault(livre => (string)livre.Element("ISBN") == ISBN);
            if (livreaSupprimer != null)
            {
                livreaSupprimer.Remove();
                doc.Save(cheminBiblio);
            }
            else
            {
                ISBN = "Ce livre n'existe pas";
            }
        }
        public async void PageCatalogueCommand()
        {
            await Shell.Current.GoToAsync("Catalogue");
        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}