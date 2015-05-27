using System;
using UIKit;
using ReactiveUI.Cocoa;
using ReactiveUI;
using UIKit.Rx;
using Core.ViewModels;
using System.Reactive.Linq;
using CoreAnimation;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;
using System.Drawing;
using Splat;

namespace testXS
{
	partial class PlanViewController : ReactiveViewController, IViewFor<PlanViewModel>,IEnableLogger
	{
		public PlanViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ViewModel = new PlanViewModel ();


			var plotImage = UIImage.FromBundle ("plot");
			var plot = new UIImageView (plotImage);
			plot.Frame = new Rectangle (150,100,50, 50); 
			plot.UserInteractionEnabled = true;
			this.Add (plot);


			// Tap gesture
			var tapGesture = new UITapGestureRecognizer (tap => { 
				var view = tap.View;
				var loc = tap.LocationInView (view);


				System.Diagnostics.Debug.WriteLine (loc.ToString ());
			});
			tapGesture.ShouldReceiveTouch += (recognizer, touch) => {
				if (touch.View is UIImageView)
				{
					return true;
				}
				return false;
			};
			this.View.AddGestureRecognizer(tapGesture);
			

		}

			
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == @"ShowFilter") {
				var controller = segue.DestinationViewController as UITabBarController;
				(controller.ViewControllers [0] as FilterLocationViewController).ViewModel = ViewModel.FilterLocationVM;
				(controller.ViewControllers [1] as FilterGroupViewController).ViewModel = ViewModel.FilterGroupVM;
				(controller.ViewControllers [4] as FilterIssueTypeViewController).ViewModel = ViewModel.FilterIssueVM;
			} else if (segue.Identifier == @"ShowFilter2"){
				

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
