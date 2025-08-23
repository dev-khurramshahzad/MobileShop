namespace MobileShop.Views.AdminPages;

public partial class AdminHome : ContentPage
{
	public AdminHome()
	{
		InitializeComponent();
	}

    private async void ManageCat_Tapped(object sender, TappedEventArgs e)
    {
		await Navigation.PushAsync(new ManageCategory());
    }

    private void CatAdd_Tapped(object sender, TappedEventArgs e)
    {

    }
}