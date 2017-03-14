using System;
using Android.Graphics;
namespace iOSConsortium.Droid
{
    public class TableItem
    {
        public string Main { get; set; }
        public string Sub { get; set; }
        public Bitmap ImageBitmap { get; set; }

        public TableItem(string main, string sub, Bitmap imageBitmap)
        {
            this.Main = main;
            this.Sub = sub;
            this.ImageBitmap = imageBitmap;
        }
    }
}
