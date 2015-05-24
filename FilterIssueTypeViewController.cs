using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ReactiveUI;
using UIKit.Rx;
using Core.ViewModels;
using System.Linq;
using Splat;
using Core.Repository;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.ComponentModel;

namespace testXS
{
	partial class FilterIssueTypeViewController : ReactiveViewController,IViewFor<FilterViewModel<StandardDefectViewModel, int>>,IEnableLogger
	{
		public FilterIssueTypeViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var top = this.TopLayoutGuide;
			const string cellKey = @"FilterStandardDefect";
			TableView.RegisterClassForCellReuse(typeof(FilterStandardDefectCell),cellKey);

			//hack for not change
			//hack for hidden the tableView section implementation in view
			this.ViewModel.Search.Execute (null);
			this.ViewModel.SearchResults.Changed.Subscribe (d =>{
				
				this.ViewModel.WhenAnyValue(c => c.SearchResults)
					.Select(c=> CreateSection(c))
					.BindTo<StandardDefectViewModel,FilterStandardDefectCell>(TableView);

				var tvdX = new UITableViewDelegateRx ();
				TableView.Delegate = tvdX;

				tvdX.RowSelectedObs.Subscribe (c => {
					var rowIndex = c.Item2.Row;
					var sectionIndex = c.Item2.Section;

					var cell = c.Item1.CellAt(c.Item2);
					var vm = (cell as FilterStandardDefectCell).ViewModel;
					ViewModel.SelectedItem = vm;
					//UI Deselceted Row
					c.Item1.DeselectRow(c.Item2,true);
				});
			});

			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

		}

//		private static ILookup<String,StandardDefectViewModel> CreateSection(List<StandardDefectViewModel> list)
//		{
//			var lookup = list.ToLookup (g => g.TypeTitle);
//
//			return lookup;
//		}

		private static ReactiveList<TableSectionInformation<StandardDefectViewModel,FilterStandardDefectCell>> CreateSection(ReactiveList<StandardDefectViewModel> list)
		{
			var group  = list.GroupBy(g=> g.TypeTitle);

			var sectionList = new ReactiveList<TableSectionInformation<StandardDefectViewModel,FilterStandardDefectCell>> ();

			foreach (var item in group) {

				var sectionItem = item.Select (g => g);
				var reactiveList = new ReactiveList<StandardDefectViewModel> (sectionItem);
				var section = new TableSectionInformation<StandardDefectViewModel,FilterStandardDefectCell>(reactiveList,new Foundation.NSString (@"FilterStandardDefect"), 44.0f);
				section.Header = new TableSectionHeader (item.Key);
			
				sectionList.Add (section);
			}

			return sectionList;
		}
	

		FilterViewModel<StandardDefectViewModel, int> _ViewModel;
		public FilterViewModel<StandardDefectViewModel, int> ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (FilterViewModel<StandardDefectViewModel, int>)value; }
		}
	}
}
