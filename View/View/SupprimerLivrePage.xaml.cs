using ViewModel;

namespace View;

public partial class SupprimerLivrePage : ContentPage
{
	public SupprimerLivrePage()
	{
		InitializeComponent();
		BindingContext = new SupprimerLivreViewModel();
	}
}