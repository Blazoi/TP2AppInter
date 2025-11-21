using ViewModel;

namespace View;

public partial class ConnexionPage : ContentPage
{
	public ConnexionPage()
	{
		InitializeComponent();
		BindingContext = new ConnexionViewModel();
	}
}