using ViewModel;

namespace View;

public partial class FavorisPage : ContentPage
{
	public FavorisPage()
	{
		InitializeComponent();
		BindingContext = new FavorisViewModel();
	}
}