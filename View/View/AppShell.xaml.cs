namespace View
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ConnexionPage), typeof(ConnexionPage));
            Routing.RegisterRoute(nameof(Catalogue), typeof(Catalogue));
            Routing.RegisterRoute(nameof(Livre), typeof(Livre));
            Routing.RegisterRoute(nameof(FavorisPage), typeof(FavorisPage));
            Routing.RegisterRoute(nameof(AjouterLivrePage), typeof(AjouterLivrePage));
            Routing.RegisterRoute(nameof(SupprimerLivrePage), typeof(SupprimerLivrePage));
            Routing.RegisterRoute(nameof(GestionComptesPage), typeof(GestionComptesPage));
        }
    }
}
