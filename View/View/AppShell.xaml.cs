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
        }
    }
}
