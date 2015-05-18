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
				//ViewModel = new FilterGroupViewModel ();

				//config the table
			TableView.RegisterClassForCellReuse(typeof(FilterGroupCell),@"FilterGroupCell");
			var s = new ReactiveTableViewSource<GroupViewModel>(TableView,ViewModel.SearchResults,new Foundation.NSString("FilterGroupCell"),44,cell=>{

				//cell.Accessory = index.Row % 2 == 0 ?  UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

				});

				TableView.Source = s;

				//Test Binding
				//ViewModel.SearchResults.First ().Count = 10;

				//Old way
				var tvd = new UITableViewDelegateRx ();
				TableView.Delegate = tvd;

				tvd.RowSelectedObs.Subscribe (c => {
					var index = c.Item2.Row;
					ViewModel.SelectedItem = ViewModel.SearchResults.ElementAt(index);
				//UI Deselceted Row
				c.Item1.DeselectRow(c.Item2,true);
					//handle ui in this way?
					//ViewModel.SearchResults.ElementAt(index).Selected = !ViewModel.SearchResults.ElementAt(index).Selected;
				});
				
				this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

			}



			public FilterGroupViewController (IntPtr handle) : base (handle)
			{
			}
		}
}
