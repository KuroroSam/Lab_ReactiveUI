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



			var plot1 = new DefectPlotView ();
			plot1.Frame = new Rectangle (150, 140, 50, 50);
			plot1.ViewModel = new DefectViewModel ();
			plot1.ViewModel.GroupID = "AAA";
			this.Add (plot1);


			var plot2 = new DefectPlotView ();
			plot2.Frame = new Rectangle (250, 140, 50, 50);
			plot2.ViewModel = new DefectViewModel ();
			plot2.ViewModel.GroupID = "BBB";
			this.Add (plot2);


			var plot3 = new DefectPlotView ();
			plot3.Frame = new Rectangle (350, 140, 50, 50);
			plot3.ViewModel = new DefectViewModel ();
			plot3.ViewModel.GroupID = "CCC";
			this.Add (plot3);

			// Tap gesture
			var tapGesture = new UITapGestureRecognizer (tap => { 
				var view = tap.View;
				var loc = tap.LocationInView (view);
			
				//System.Diagnostics.Debug.WriteLine (loc.ToString ());
			});
			tapGesture.ShouldReceiveTouch += (recognizer, touch) => {
				System.Diagnostics.Debug.WriteLine ("Tap :ShouldReceiveTouch");

				if (touch.View is DefectPlotView) {
					this.SelectedDefectPlotView = touch.View as DefectPlotView;
					return true;
				}
				
				return false;
			};
			this.View.AddGestureRecognizer(tapGesture);

			#region unuserd
			//pan gesture
			var panGesture = new UIPanGestureRecognizer(pan=>{

				System.Diagnostics.Debug.WriteLine ("Pan Began");

					var translation = pan.TranslationInView(this.View);

					System.Diagnostics.Debug.WriteLine (pan.View.ToString ());

					this.SelectedDefectPlotView.Center = pan.LocationInView(this.View);

			});

			panGesture.ShouldReceiveTouch  += (recognizer, touch) => {
		
				System.Diagnostics.Debug.WriteLine ("Pan :ShouldReceiveTouch");

				if (touch.View is DefectPlotView) {
						this.SelectedDefectPlotView = touch.View as DefectPlotView;
						return true;
					}
					

				return false;
			};

			panGesture.ShouldRecognizeSimultaneously += (UIGestureRecognizer r, UIGestureRecognizer other) => {
				System.Diagnostics.Debug.WriteLine ("panGesture Sim");

				return true;
			};

			//this.View.AddGestureRecognizer (panGesture);

			#endregion

			var longPressGesture = new UILongPressGestureRecognizer (longpress => {
				if (longpress.State == UIGestureRecognizerState.Began)
				{
					System.Diagnostics.Debug.WriteLine ("LongPress Began");
					var pt = SelectedDefectPlotView.Center;

					UIView.Animate (0.2,
						() => {
							SelectedDefectPlotView.Center =   new CGPoint(pt.X,pt.Y - 10);
						},null
					);

				}
				else if (longpress.State == UIGestureRecognizerState.Changed)
				{
					System.Diagnostics.Debug.WriteLine ("LongPress Changed");

					this.SelectedDefectPlotView.Center = longpress.LocationInView(this.View);

				}
				else if (longpress.State == UIGestureRecognizerState.Ended)
				{
					var pt = SelectedDefectPlotView.Center;

					UIView.Animate (0.2,
						() => {
							SelectedDefectPlotView.Center =   new CGPoint(pt.X,pt.Y + 10);
						},null
					);
				}
			});

			longPressGesture.MinimumPressDuration = 1;
		
			longPressGesture.ShouldBegin += (recognizer) => {
				
				System.Diagnostics.Debug.WriteLine ("Long press :ShouldReceiveTouch");

				return true;
			};

			longPressGesture.ShouldRecognizeSimultaneously += (UIGestureRecognizer r, UIGestureRecognizer other) => {
				System.Diagnostics.Debug.WriteLine ("longPressGesture Sim");

				return true;
			};

			this.View.AddGestureRecognizer (longPressGesture);
		

		}

		public void inflate (){
			
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

		private DefectPlotView SelectedDefectPlotView {get;set;}

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
