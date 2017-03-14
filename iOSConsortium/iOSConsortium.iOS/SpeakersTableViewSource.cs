using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using iOSConsortium.Models;
namespace iOSConsortium.iOS
{
    public class SpeakersTableViewSource : UITableViewSource
    {
        private List<TableItem> Items { get; set; } = new List<TableItem>();

        public SpeakersTableViewSource(List<TableItem> items)
        {
            this.Items = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (SpeakersTableViewCell)tableView.DequeueReusableCell(nameof(SpeakersTableViewCell), indexPath);
            var item = Items[indexPath.Row];
            cell.Update(item);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return Items.Count;
        }
    }
}
