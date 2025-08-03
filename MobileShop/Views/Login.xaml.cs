using System.Threading.Tasks;
using MobileShop.Models;

namespace MobileShop.Views;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    

    private void btnRegister_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Register();
    }

    private async void btnLogin_Clicked_1(object sender, EventArgs e)
    {
        var u = App.db.Table<User>().Where(x => x.Email == txtEmail.Text && x.Password == txtPassword.Text).FirstOrDefault();
        if (u != null)
        {
            await DisplayAlert("Message", "Login Successful", "OK");
            App.Current.MainPage = new HomePage();
        }
        else
        {
            await DisplayAlert("Message", "Login Failed! Email or Password Incorrect", "OK");
        }
    }
}