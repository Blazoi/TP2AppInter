using ViewModel;

namespace View;

public partial class Catalogue : ContentPage
{
	public Catalogue()
	{
		InitializeComponent();
		BindingContext = new CatalogueViewModel();
	}
}