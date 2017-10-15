using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Database;
using System.Diagnostics;
using Java.Util;
using Android.Runtime;
using GoogleGson;
using Newtonsoft.Json;

namespace WeddingSharePhoto.Data
{
    public class Users
    {
        public bool matsubara;
        public bool tetodon;
    }
}

namespace WeddingSharePhoto
{
    class ValueEventListener<T> : Java.Lang.Object, IValueEventListener
    {
        public void OnCancelled(DatabaseError error)
        {
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists() && snapshot.HasChildren)
            {
                HashMap dataHashMap = snapshot.Value.JavaCast<HashMap>();
                Gson gson = new GsonBuilder().Create();
                string chatItemDaataString = gson.ToJson(dataHashMap);
                System.Diagnostics.Debug.WriteLine(chatItemDaataString);

                try
                {
                    Data.Users items = JsonConvert.DeserializeObject<Data.Users>(chatItemDaataString);
                }
                catch
                {

                }
            }
        }
    }

    [Activity(Label = "WeddingSharePhoto", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate 
            {
                button.Text = string.Format("{0} clicks!", count++);
            };

            FirebaseDatabase.Instance.GetReference("users").AddValueEventListener(new ValueEventListener<Data.Users>());
        }
    }
}

