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
	partial class TestLayoutViewController : ReactiveViewController, IViewFor<FilterViewModel<LocationViewModel, int>>
	{
		public TestLayoutViewController (IntPtr handle) : base (handle)
		{
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

		public override void ViewDidLoad ()
		{
			base.LoadView ();
			const string cellKey = @"FilterLocationCell";

			var topGuide = this.TopLayoutGuide;

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

			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearhBar.Text);

			//UI Staff
			//			DoneButton.Clicked += delegate {
			//				this.DismissViewController(true,null);
			//			};
		}
	}
}
