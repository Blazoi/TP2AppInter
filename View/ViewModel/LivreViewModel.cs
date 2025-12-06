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
using System.Xml;
using System.Xml.Linq;

namespace ViewModel
{

    public class LivreViewModel : INotifyPropertyChanged
    {
        private readonly string cheminLivreChoisi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LivreChoisi.xml");
        
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AjouterNoteCommand { get; }
        private int note;
        public int NoteaAjouter
        {
            get => note;
            set
            {
                if (value <= 5 && value >= 0)
                {
                    note = value;
                    OnPropertyChanged();
                } else
                {
                    note = -1;
                }
            }
        }
        public bool EvalExiste = false;
        public int AncienneNote;

        public Livre Livre { get; set; } = new(null,null,null,null, new DateOnly(1000,10,10),null,0,0);
        public LivreViewModel()
        {
            ChargerLivre();
            AjouterNoteCommand = new Command(AjouterNote);
        }

        public void ChargerLivre()
        {
            XElement livre = XDocument.Load(cheminLivreChoisi).Root;

            Livre.Titre = (string) livre.Element("Titre");
            Livre.Auteur = (string) livre.Element("Auteur");
            Livre.ISBN = (string) livre.Element("ISBN");
            Livre.MaisonEdition = (string) livre.Element("MaisonEdition");
            Livre.DatePublication = DateOnly.Parse( (string) livre.Element("DatePublication"));
            Livre.Description = (string) livre.Element("Description");
            Livre.MoyenneEvaluation = (double) livre.Element("MoyenneEvaluation");
            Livre.NmbEvaluation = (int) livre.Element("NombreEvaluations");
        }

        public async void AjouterNote()
        {
            double ancienneMoyenne = Livre.MoyenneEvaluation;
            int nombre = Livre.NmbEvaluation;

            if (note == -1)
            {
                await Shell.Current.DisplayAlert("Erreur", "Entrez une valeur de 1 à 5.", "ok");
                return;
            }

            if (!EvalExiste)
            {
                double nouvelleMoyenne = Math.Round(((ancienneMoyenne * nombre) + note) / (nombre + 1), 1);
                Livre.MoyenneEvaluation = nouvelleMoyenne;
                Livre.NmbEvaluation++;

                AncienneNote = note;
                EvalExiste = true;
            } else
            {
                double nouvelleMoyenne = Math.Round(((ancienneMoyenne * nombre) - AncienneNote + note) / nombre, 1);
                Livre.MoyenneEvaluation = nouvelleMoyenne;

                AncienneNote = note;
            }
                OnPropertyChanged(nameof(Livre));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
