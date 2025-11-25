using ViewModel;

namespace View;

public partial class GestionComptesPage : ContentPage
{
	public GestionComptesPage()
	{
		InitializeComponent();
		BindingContext = new GestionComptesViewModel();
	}
}