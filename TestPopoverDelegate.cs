using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using UIKit.Rx;
using Core.ViewModels;
using System.Linq;

namespace testXS
{
	public class UIPopoverPresentationControllerDelegateRx_Sam : UIPopoverPresentationControllerDelegate
	{
		UINavigationController _nav;

		public UIPopoverPresentationControllerDelegateRx_Sam (UINavigationController nav)
		{
			_nav = nav;	
		}

		public override UIViewController GetViewControllerForAdaptivePresentation (UIPresentationController controller, UIModalPresentationStyle style)
		{
			throw new System.NotImplementedException ();
		}	


	}
}

