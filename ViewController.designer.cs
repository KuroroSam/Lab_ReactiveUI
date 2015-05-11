// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace testXS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel infoLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton Login { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField PasswordText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField UserNameText { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (infoLabel != null) {
				infoLabel.Dispose ();
				infoLabel = null;
			}
			if (Login != null) {
				Login.Dispose ();
				Login = null;
			}
			if (PasswordText != null) {
				PasswordText.Dispose ();
				PasswordText = null;
			}
			if (UserNameText != null) {
				UserNameText.Dispose ();
				UserNameText = null;
			}
		}
	}
}
