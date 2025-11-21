using ViewModel;

namespace View;

public partial class Livre : ContentPage
{
	public Livre()
	{
		InitializeComponent();
		BindingContext = new LivreViewModel();
	}
}