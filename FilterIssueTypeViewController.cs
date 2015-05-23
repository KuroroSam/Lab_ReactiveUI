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

namespace testXS
{
	partial class FilterIssueTypeViewController : ReactiveViewController,IViewFor<FilterSectionViewModel<StandardDefectViewModel, int,FilterStandardDefectCell>>,IEnableLogger
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

			ViewModel = new FilterSectionViewModel<StandardDefectViewModel,int,FilterStandardDefectCell> (new StandardDefectRepository ());

			this.ViewModel.WhenAnyValue (c => c.SectionList).BindTo<StandardDefectViewModel,FilterStandardDefectCell> (TableView);

			var tvd = new UITableViewDelegateRx ();
			TableView.Delegate = tvd;

			tvd.RowSelectedObs.Subscribe (c => {
				var rowIndex = c.Item2.Row;
				var sectionIndex = c.Item2.Section;


				ViewModel.SelectedItem = ViewModel.SectionResult[sectionIndex][rowIndex];
				//UI Deselceted Row
				c.Item1.DeselectRow(c.Item2,true);
			});


			this.Bind (ViewModel, vm => vm.SearchQuery, v => v.SearchBar.Text);

		}


	

		FilterSectionViewModel<StandardDefectViewModel, int,FilterStandardDefectCell> _ViewModel;
		public FilterSectionViewModel<StandardDefectViewModel, int,FilterStandardDefectCell> ViewModel {
			get { return _ViewModel; }
			set { this.RaiseAndSetIfChanged(ref _ViewModel, value); }
		}

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (FilterSectionViewModel<StandardDefectViewModel, int,FilterStandardDefectCell>)value; }
		}
	}
}
