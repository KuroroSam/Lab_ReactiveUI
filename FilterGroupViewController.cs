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
				ViewModel = new FilterGroupViewModel ();

				//config the table
			TableView.RegisterClassForCellReuse(typeof(FilterGroupCell),@"FilterGroupCell");
			var s = new ReactiveTableViewSource<GroupViewModel>(TableView,ViewModel.SearchResults,new Foundation.NSString("FilterGroupCell"),44,cell=>{

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
					ViewModel.SelectedGroup = ViewModel.SearchResults.ElementAt(index);
				});


				this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

			}



			public FilterGroupViewController (IntPtr handle) : base (handle)
			{
			}
		}
}
