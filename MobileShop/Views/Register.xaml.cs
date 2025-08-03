using System.Text.RegularExpressions;
using MobileShop.Models;
using SQLite;

namespace MobileShop.Views;

public partial class Register : ContentPage
{
    public Register()
    {
        InitializeComponent();
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        //Validation
        //Required Field
        if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
            string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
        {
            await DisplayAlert("Message", "Please fill all required fields.", "OK");
            return;
        }

        //Password Matching
        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            await DisplayAlert("Message", "Passwords do not match.", "OK");
        }



        //Email pattern
        if (!Regex.IsMatch(txtEmail.Text, "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$"))
        {
            await DisplayAlert("Message", "Email is not valid it must contain an @ sign.", "OK");
            return;
        }



        //Create Table
        App.db.CreateTable<User>();

        var u = App.db.Table<User>().FirstOrDefault(x => x.Email == txtEmail.Text);
        if (u != null)
        {
            await DisplayAlert("Message", "This email is already registered.", "OK");
            return;

        }


        //Strong Password
        if (!Regex.IsMatch(txtPassword.Text, "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"))
        {
            await DisplayAlert("Message", "Password must be at least 8 charecters long having at least 1 uppercase 1 lower case 1 number and one symbol.", "OK");
            return;
        }

        User user = new User()
        {
            FullName = txtName.Text,
            Email = txtEmail.Text,
            Password = txtPassword.Text,
            Phone = txtPhone.Text,
            Address = txtAddress.Text,
            UserType = "User",
            Details = "",
            Status = "Active"
        };





        //Insert
        App.db.Insert(user);

        await DisplayAlert("Message", "Account Registered Successfully.", "OK");
        App.Current.MainPage = new Login();


    }

    private void btnLogin_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new Login();
    }
}