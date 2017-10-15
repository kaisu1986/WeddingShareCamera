using Android.App;
using Android.Widget;
using Android.OS;
using Firebase.Database;
using System;
using Java.Util;
using Android.Runtime;

namespace WeddingSharePhoto
{
    public class UsersValueEventListener : Java.Lang.Object, IValueEventListener
    {
        public void OnCancelled(DatabaseError error)
        {
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Exists() && snapshot.HasChildren)
            {
                HashMap dataHashMap = snapshot.Value.JavaCast<HashMap>();
            }
        }
    }

    [Activity(Label = "WeddingSharePhoto", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        DatabaseReference mDatabase;
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

            mDatabase = FirebaseDatabase.Instance.Reference;

            var users = mDatabase.Child("users");
            users.AddValueEventListener(new UsersValueEventListener());
        }
    }
}

