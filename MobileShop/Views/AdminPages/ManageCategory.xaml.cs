using MobileShop.Helpers;
using MobileShop.Models;

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
}