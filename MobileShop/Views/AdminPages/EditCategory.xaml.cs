using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using MobileShop.Helpers;
using MobileShop.Models;
using System;

namespace MobileShop.Views.AdminPages;

public partial class EditCategory : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    FileResult photo = null;
    Category CatItem = null;

    public EditCategory(Category? item)
    {
        InitializeComponent();
        BindingContext = item;
        CatItem = item;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var choice = await DisplayActionSheet("Select Image From", "Close", "", "Camera", "Gallery");
            if (choice == "Camera")
            {
                photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    ImagePlaceholder.Source = ImageSource.FromStream(() => stream);
                }
            }
            else if (choice == "Gallery")
            {
                photo = await MediaPicker.PickPhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    ImagePlaceholder.Source = ImageSource.FromStream(() => stream);
                }
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Message", $"Something went wrong.\nError:{ex.Message}", "OK");
        }
    }

    private async void btnAddCat_Clicked(object sender, EventArgs e)
    {
        

        try
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrEmpty(txtCategoryDescription.Text))
            {
                await DisplayAlert("Message", "Please Fill all fields ", "OK");
                return;
            }

            var cat = new Category
            {
                CatID = CatItem.CatID,
                CatName = txtCategoryName.Text,
                Details = txtCategoryDescription.Text,
                Status = "Active"
            };

            IndProgress.IsVisible = true;

            var PostedFileUrl =CatItem.Image ;
            if (photo == null)
            {
                await DisplayAlert("Message", "Image is not selected you can upload image by tapping on camera icon or continue without image", "OK");
                IndProgress.IsVisible = false;
                
            }
            else
            {
                 PostedFileUrl = await firebaseHelper.FirebaseStorage.Child("Categories").Child(cat.CatID + ".jpg").PutAsync(await photo.OpenReadAsync());

            }


            cat.Image = PostedFileUrl;

            await firebaseHelper.FirebaseDatabase.Child("Categories").Child(cat.CatID).PutAsync(cat);

            IndProgress.IsVisible = false;

            await DisplayAlert("Message", "Category Updated Successfully", "OK");
            App.Current.MainPage = new ManageCategory();
        }
        catch (Exception ex)
        {
            IndProgress.IsVisible = false;
            await DisplayAlert("Message", $"Something went wrong.\nError:{ex.Message}", "OK");
        }


    }

    private void btnBack_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage = new ManageCategory();
    }
}