using System;
using Foundation;
using UIKit;

namespace iOSConsortium.iOS
{
    public partial class SpeakersTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString(nameof(SpeakersTableViewCell));
        public static readonly UINib Nib;

        static SpeakersTableViewCell()
        {
            Nib = UINib.FromName(Key, NSBundle.MainBundle);
        }

        protected SpeakersTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        /// <summary>
        /// データを流し込むためのUpdateメソッド
        /// </summary>
        /// <param name="item"></param>
        public void Update(TableItem item)
        {
            NameLabel.Text = item.Name;
            TitleLabel.Text = item.Title;
            AvatorImage.Image = item.Image;

            AvatorImage.Layer.CornerRadius = AvatorImage.Bounds.Height / 2;
            AvatorImage.Layer.BorderWidth = 2;
            AvatorImage.Layer.BorderColor = UIColor.FromRGB(0x34, 0x98, 0xdb).CGColor;
            AvatorImage.ClipsToBounds = true;
        }
    }
}