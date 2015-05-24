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
//
			 this.ViewModel.WhenAnyValue(c => c.SearchResults)
				.Select(c=> CreateSection(c))
				.BindTo<StandardDefectViewModel,FilterStandardDefectCell>(TableView);


			//hack for not change
			this.ViewModel.SearchResults.CountChanged.Subscribe (d =>{

				//TableView.Source = new ReactiveTableViewSource<StandardDefectViewModel>(TableView,ViewModel.SearchResults,new Foundation.NSString(cellKey),44,cell=>{});
				this.ViewModel.WhenAnyValue(c => c.SearchResults)
					.Select(c=> CreateSection(c))
					.BindTo<StandardDefectViewModel,FilterStandardDefectCell>(TableView);
				
			});
				


			var tvd = new UITableViewDelegateRx ();
			TableView.Delegate = tvd;

			tvd.RowSelectedObs.Subscribe (c => {
				var rowIndex = c.Item2.Row;
				var sectionIndex = c.Item2.Section;


				//ViewModel.SelectedItem = ViewModel.SectionResult[sectionIndex][rowIndex];
				//UI Deselceted Row
				c.Item1.DeselectRow(c.Item2,true);
			});


			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

		}

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
