using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Model;

namespace ViewModel
{

    public class LivreViewModel
    {
        public ICommand AjouterNoteCommand { get; }
        public Livre Livre { get; set; } = new(null,null,null,null,null,null,null,null);
        public LivreViewModel()
        {
            ChargerLivre();
            AjouterNoteCommand = new Command(AjouterNote);
        }

        public void ChargerLivre()
        {
            XmlDocument doc = new();
            doc.Load("C:\\Users\\jackj\\OneDrive\\Desktop\\TP2AppInteractives\\View\\ViewModel\\LivreChoisi.xml");
            XmlElement livre = doc.DocumentElement;

            Livre.Titre = livre["Titre"].InnerText;
            Livre.Auteur = livre["Auteur"].InnerText;
            Livre.ISBN = livre["ISBN"].InnerText;
            Livre.MaisonEdition = livre["MaisonEdition"].InnerText;
            Livre.DatePublication = DateOnly.Parse(livre["DatePublication"].InnerText);
            Livre.Description = livre["Description"].InnerText;
            Livre.MoyenneEvaluation = float.Parse(livre["MoyenneEvaluation"].InnerText);
            Livre.NmbEvaluation = int.Parse(livre["NmbEvaluation"].InnerText);
        }

        public void AjouterNote()
        {

        }
    }

}
