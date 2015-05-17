using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using ReactiveUI.Cocoa;
using ReactiveUI.Events;
using Core.ViewModels;
using UIKit.Rx;
using System.Linq;

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
			var s = new ReactiveTableViewSource<DrawingViewModel> (TableView,ViewModel.SearchResults,new Foundation.NSString("DrawingViewCell"),44,cell=>{
				
				cell.Accessory = UITableViewCellAccessory.Checkmark;

			});

			TableView.Source = s;

			//Test Binding
			//ViewModel.SearchResults.First ().Count = 10;

			//Old way
			var tvd = new UITableViewDelegateRx ();
			TableView.Delegate = tvd;

			tvd.RowSelectedObs.Subscribe (c => {
				var index = c.Item2.Row;
				ViewModel.SelectedDrawing = ViewModel.SearchResults.ElementAt(index);
			});


			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

		}

	

		public FilterDrawingView (IntPtr handle) : base (handle)
		{
		}
	}
}
