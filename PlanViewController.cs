using System;
using UIKit;
using ReactiveUI.Cocoa;
using ReactiveUI;
using Core.ViewModels;
using System.Reactive.Linq;
using CoreAnimation;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace testXS
{
	partial class PlanViewController : ReactiveViewController, IViewFor<PlanViewModel>
	{
		public PlanViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ViewModel = new PlanViewModel ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == @"ShowFilter") {
				var controller = segue.DestinationViewController as UITabBarController;
				(controller.ViewControllers[1] as FilterGroupViewController).ViewModel = ViewModel.FilterGroup;
			}
		}

		PlanViewModel _ViewModel;
		public PlanViewModel ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (PlanViewModel)value; }
		}
	}
}
