
using System;

using Foundation;
using UIKit;

namespace testXS
{
	public class zCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("zCell");

		public zCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

