using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using ReactiveUI.Cocoa;
using Core.ViewModels;

namespace testXS
{
	partial class FilterDrawingCell :  ReactiveTableViewCell, IViewFor<DrawingViewModel>
	{
		public FilterDrawingCell (IntPtr handle) : base (handle)
		{
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public FilterDrawingCell(UITableViewCellStyle style, string cellId)
			: base( UITableViewCellStyle.Value1, cellId) { 
		
			this.OneWayBind(this.ViewModel, x => x.DrawingTitle, x => x.TextLabel.Text);
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Text);

		}

		public DrawingViewModel _viewModel;

		public DrawingViewModel ViewModel
		{
			get { return _viewModel; }
			set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ((IViewFor<DrawingViewModel>)this).ViewModel; }
			set { ((IViewFor<DrawingViewModel>)this).ViewModel = (DrawingViewModel)value; }
		}
	}
}
