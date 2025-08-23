using Firebase.Database.Query;
using MobileShop.Helpers;
using MobileShop.Models;
using System.Threading.Tasks;

namespace MobileShop.Views.AdminPages;

public partial class ManageCategory : ContentPage
{
	FirebaseHelper firebaseHelper = new FirebaseHelper();

    public ManageCategory()
	{
		InitializeComponent();
		LoadData();

    }

	async void LoadData()
	{
		List<Category> cats = new List<Category>();

		var firebaseData = await firebaseHelper.FirebaseDatabase.Child("Categories").OnceAsync<Category>();
		foreach (var item in firebaseData)
		{
			cats.Add(item.Object);
		}

        DataList.ItemsSource = cats;
    }

    private  async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		try
		{
            var item = e.Item as Category;
            var selection = await DisplayActionSheet("Select Action", "Cancel", "Delete", "View", "Edit");
            if (selection == "Delete")
            {
                bool confirm = await DisplayAlert("Confirm", $"Are you sure you want to delete {item.CatName}?", "Yes", "No");
                if (confirm)
                {
                    await firebaseHelper.FirebaseDatabase.Child("Categories").Child(item.CatID).DeleteAsync();
                    await DisplayAlert("Success", $"{item.CatName} has been deleted successfully.", "OK");
                    LoadData();
                }
            }
            else if (selection == "View")
            {
                await Navigation.PushAsync( new ViewCategory(item));
            }
            else if (selection == "Edit")
            {
                await Navigation.PushAsync(new EditCategory(item));   
               
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Message", $"Something went wrong.\nError:{ex.Message}", "OK");
        }
    }
}