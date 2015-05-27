using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using ReactiveUI.Cocoa;
using ReactiveUI.Events;
using Core.ViewModels;
using UIKit.Rx;
using System.Linq;
using Foundation;
using System.Drawing;


namespace testXS
{
	[Register("DefectPlotView")]
	public partial class DefectPlotView : ReactiveView,IViewFor<DefectViewModel>
	{
		public DefectPlotView (IntPtr p) : base(p)
		{
			
		}

		public DefectPlotView()
		{
			//construct the ui
			//var plotImage = UIImage.FromBundle ("plot");
			var plot = new UIView(); //new UIImageView (plotImage);
			plot.Bounds = new Rectangle (0,0,50, 50);
			plot.BackgroundColor = UIColor.Red;
			plot.UserInteractionEnabled = true;
			this.Add (plot);

			//set up binding

		}

		public DefectViewModel _viewModel;

		public DefectViewModel ViewModel
		{
			get { return _viewModel; }
			set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ((IViewFor<DefectViewModel>)this).ViewModel; }
			set { ((IViewFor<DefectViewModel>)this).ViewModel = (DefectViewModel)value; }
		}
	}
}

