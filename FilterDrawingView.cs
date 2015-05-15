using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using ReactiveUI.Cocoa;
using Core.ViewModels;

namespace testXS
{
	partial class FilterDrawingView : ReactiveTableViewController, IViewFor<DrawingFilterViewModel>
	{
	
		DrawingFilterViewModel _ViewModel;
		public DrawingFilterViewModel ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (DrawingFilterViewModel)value; }
		}

		public override void ViewDidLoad ()
		{
			base.LoadView ();
			ViewModel = new DrawingFilterViewModel ();

			//config the table
			TableView.RegisterClassForCellReuse(typeof(FilterDrawingCell),@"DrawingViewCell");

			TableView.Source = new ReactiveTableViewSource<DrawingViewModel> (TableView,ViewModel.SearchResults,new Foundation.NSString("DrawingViewCell"),44,null);

			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);
		}
			

		public FilterDrawingView (IntPtr handle) : base (handle)
		{
		}
	}
}
