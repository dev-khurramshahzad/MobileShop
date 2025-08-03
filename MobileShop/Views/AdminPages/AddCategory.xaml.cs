using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using MobileShop.Helpers;
using MobileShop.Models;
using System;

namespace MobileShop.Views.AdminPages;

public partial class AddCategory : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    FileResult photo = null;

    public AddCategory()
    {
        InitializeComponent();
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
                CatID = Guid.NewGuid().ToString(),
                CatName = txtCategoryName.Text,
                Details = txtCategoryDescription.Text,
                Status = "Active"
            };

            IndProgress.IsVisible = true;

            var PostedFileUrl = "https://firebasestorage.googleapis.com/v0/b/mobileshop-5f433.firebasestorage.app/o/camera_icon.png?alt=media&token=b9b6cafd-f9bc-4b05-96ab-c75445a87cc9";
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

            await DisplayAlert("Message", "Category Added Successfully", "OK");
        }
        catch (Exception ex)
        {
            IndProgress.IsVisible = false;
            await DisplayAlert("Message", $"Something went wrong.\nError:{ex.Message}", "OK");
        }


    }
}