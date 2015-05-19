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
	partial class FilterLocationViewController : ReactiveViewController,IViewFor<FilterViewModel<LocationViewModel, int>>
	{
		public FilterLocationViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var top = this.TopLayoutGuide;
			const string cellKey = @"FilterLocationCell";


			TableView.RegisterClassForCellReuse(typeof(FilterLocationCell),cellKey);
			TableView.Source = new ReactiveTableViewSource<LocationViewModel>(TableView,ViewModel.SearchResults,new Foundation.NSString(cellKey),44,cell=>{});


			var tvd = new UITableViewDelegateRx ();
			TableView.Delegate = tvd;

			tvd.RowSelectedObs.Subscribe (c => {
				var index = c.Item2.Row;
				ViewModel.SelectedItem = ViewModel.SearchResults.ElementAt(index);
				//UI Deselceted Row
				c.Item1.DeselectRow(c.Item2,true);

			});

			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

			//old way
			DoneButton.Clicked += (object sender, EventArgs e) => {
				this.DismissViewController(true,null);
			};
		}

		FilterViewModel<LocationViewModel, int> _ViewModel;
		public FilterViewModel<LocationViewModel, int> ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (FilterViewModel<LocationViewModel, int>)value; }
		}
	}
}
