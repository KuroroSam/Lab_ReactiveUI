using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace testXS
{
	partial class FilterLocationController : UIViewController
	{
		public FilterLocationController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			//old way
			DoneButton.Clicked += delegate {
				this.DismissViewController(true,null);
			};
		}
			
	}
}
