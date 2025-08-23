using MobileShop.Views.AdminPages;
using SQLite;

namespace MobileShop
{
   
    public partial class App : Application
    {
        public static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "dbShop.db");
        public static SQLiteConnection db = new SQLiteConnection(dbPath);
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Colors.DarkBlue,
                BarTextColor = Colors.White
            };
        }
    }
}
