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
		partial class FilterGroupViewController : ReactiveTableViewController, IViewFor<FilterGroupViewModel>
		{

			FilterGroupViewModel _ViewModel;
			public FilterGroupViewModel ViewModel {
				get { return _ViewModel; }
				set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
			}

			object IViewFor.ViewModel {
				get { return ViewModel; }
				set { ViewModel = (FilterGroupViewModel)value; }
			}

			public override void ViewDidLoad ()
			{
				base.LoadView ();
				TableView.RegisterClassForCellReuse(typeof(FilterGroupCell),@"FilterGroupCell");
				TableView.Source = new ReactiveTableViewSource<GroupViewModel>(TableView,ViewModel.SearchResults,new Foundation.NSString("FilterGroupCell"),44,cell=>{});

				
				var tvd = new UITableViewDelegateRx ();
				TableView.Delegate = tvd;

				tvd.RowSelectedObs.Subscribe (c => {
					var index = c.Item2.Row;
					ViewModel.SelectedItem = ViewModel.SearchResults.ElementAt(index);
				//UI Deselceted Row
				c.Item1.DeselectRow(c.Item2,true);
		
				});
				
				this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

			//UI Staff

			}

			public FilterGroupViewController (IntPtr handle) : base (handle)
			{
			}
		}
}
