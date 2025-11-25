using System.Windows.Input;
using Services;
using Model;
using System.Diagnostics;

namespace ViewModel
{
    // All the code in this file is included in all platforms.
    public class ConnexionViewModel
    {
        public ICommand ConnectButton {  get; }
        public Utilisateur utilisateur { get; set; } = new Utilisateur(null, null, null, null);

        public ConnexionViewModel()
        {
            ConnectButton = new Command(GoToCatalogue);
        }

        public async void GoToCatalogue()
        {
            AuthentificationService auth = new();

            if (auth.Authentifier(utilisateur.Email, utilisateur.MotDePasse))
            //if (true)
            {
                await Shell.Current.GoToAsync("Catalogue");
            } else
            {
                await Shell.Current.DisplayAlert("Erreur", "Mauvaises informations de connexion", "ok");
            }
        }
    }
}
