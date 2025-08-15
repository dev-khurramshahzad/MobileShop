using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using MobileShop.Helpers;
using MobileShop.Models;
using System;

namespace MobileShop.Views.AdminPages;

public partial class ViewCategory : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    FileResult photo = null;
    Category CatItem = null;

    public ViewCategory(Category? item)
    {
        InitializeComponent();
        BindingContext = item;
        CatItem = item;
    }

    private void btnBack_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new ManageCategory();
    }

    private void BtnEdit_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new EditCategory(CatItem);

    }
}