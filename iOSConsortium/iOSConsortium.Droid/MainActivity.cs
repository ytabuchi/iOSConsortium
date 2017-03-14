using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using iOSConsortium.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Graphics;

namespace iOSConsortium.Droid
{
    [Activity(Label = "iOSConsortium.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private static HttpClient client = new HttpClient();
        private List<TableItem> tableItems = new List<TableItem>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var speakers = new SpeakersModel();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var listView = FindViewById<ListView>(Resource.Id.speakersListView);
            // CustomAdapterを作成して適用
            var speakersAdapter = new SpeakersListAdapter(this, tableItems);
            listView.Adapter = speakersAdapter;

            var progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            var getSpeakersButton = FindViewById<Button>(Resource.Id.getSpeakersButton);
            getSpeakersButton.Click += async (object sender, EventArgs e) =>
            {
                // クルクルを表示、ボタンを利用不可にします。
                getSpeakersButton.Enabled = false;
                progressBar.Visibility = ViewStates.Visible;

                // ModelのGetSpeakersメソッドを実行します。
                await speakers.GetSpeakersAsync();

                tableItems.Clear();
                foreach (var speaker in speakers.Speakers)
                {
                    var image = await this.GetImageBitmapFromUrl(speaker.Avatar);
                    tableItems.Add(new TableItem(speaker.Name, speaker.Title, image)); 
                }

                // ListAdapterに内容の変更を通知します。 
                speakersAdapter.NotifyDataSetChanged();

                // クルクルを非表示、ボタンを利用可にします。
                progressBar.Visibility = ViewStates.Invisible;
                getSpeakersButton.Enabled = true;
            };
        }

        private async Task<Bitmap> GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            var imageBytes = await client.GetByteArrayAsync(url);
            if (imageBytes != null && imageBytes.Length > 0)
            {
                imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            }

            return imageBitmap;
        }
    }
}
