using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShop.Helpers
{
    internal class FirebaseHelper
    {
        public FirebaseClient FirebaseDatabase = new FirebaseClient("https://mobileshop-5f433-default-rtdb.firebaseio.com/");
        public FirebaseStorage FirebaseStorage = new FirebaseStorage("mobileshop-5f433.firebasestorage.app");
    }
}
