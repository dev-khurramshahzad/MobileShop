using MobileShop.Views;

namespace MobileShop;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    

    private void btnGetStarted_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Login();
    }
}