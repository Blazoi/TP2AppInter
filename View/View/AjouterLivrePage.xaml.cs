using ViewModel;

namespace View;

public partial class AjouterLivrePage : ContentPage
{
	public AjouterLivrePage()
	{
		InitializeComponent();
		BindingContext = new AjouterLivreViewModel();
	}
}