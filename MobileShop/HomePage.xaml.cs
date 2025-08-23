using MobileShop.Views;
using MobileShop.Views.AdminPages;
using static SQLite.SQLite3;

namespace MobileShop;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    

    private async  void btnGetStarted_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        App.Current.MainPage = new AdminShell();
    }
}