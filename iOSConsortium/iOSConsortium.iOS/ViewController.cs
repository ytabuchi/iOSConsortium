using System;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using iOSConsortium.Models;
using System.Collections.Generic;
using SVProgressHUDBinding;

namespace iOSConsortium.iOS
{
    public partial class ViewController : UIViewController
    {
        private static HttpClient client = new HttpClient();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var speakers = new SpeakersModel();
            SpeakersTableView.RowHeight = 70;
            SpeakersTableView.RegisterNibForCellReuse(SpeakersTableViewCell.Nib, nameof(SpeakersTableViewCell));

            GetSpeakersButton.TouchUpInside += async (object sender, EventArgs e) =>
            {
                // クルクルを表示、ボタンを利用不可にします。
                GetSpeakersButton.Enabled = false;
                SVProgressHUD.Show();

                // vmのGetSpeakersメソッドを実行します。
                await speakers.GetSpeakersAsync();

                // Name、Title、UIImageのプロパティを持つTableItemのListにデータを移し替えます。
                // 移し替える前にImageUrlをUIImageに変換して格納します。
                var tableItems = new List<TableItem>();
                foreach (var x in speakers.Speakers)
                {
                    var image = await this.LoadImage(x.Avatar);
                    tableItems.Add(new TableItem(x.Name, x.Title, image));
                }

                // TableViewのSourceをCustomTableViewSourceでnewします。
                SpeakersTableView.Source = new SpeakersTableViewSource(tableItems);
                SpeakersTableView.ReloadData();

                // クルクルを非表示、ボタンを利用可にします。
                SVProgressHUD.Dismiss();
                GetSpeakersButton.Enabled = true;
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.        
        }

        private async Task<UIImage> LoadImage(string imageUrl)
        {
            // imageUrlからバイト配列を取得します。
            var imageBytes = await client.GetByteArrayAsync(imageUrl);
            // バイト配列のデータからUIImageを生成します。
            return UIImage.LoadFromData(NSData.FromArray(imageBytes));
        }
    }
}
