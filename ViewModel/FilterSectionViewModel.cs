using System;
using System.Reactive.Linq;
using ReactiveUI;
using System.Threading.Tasks;
using Splat;
using System.Linq;
using System.Collections.Generic;
using Core.Repository;
using Core.ViewModels;
using UIKit;


namespace testXS
{
	public class FilterSectionViewModel<T,U,V> : FilterViewModel<T,U>  
		where T : StandardDefectViewModel, IFilterViewModel<U>
		where V : UITableViewCell
	{

		ReactiveList<TableSectionInformation<T,V>> sectionList;

		public ReactiveList<TableSectionInformation<T,V>> SectionList {
			get { 
				return sectionList;
			}
			set { this.RaiseAndSetIfChanged (ref sectionList, value); }
		}


		public List<List<T>> SectionResult { get; set; }

		public FilterSectionViewModel(IRepository<T> dbservice): base(dbservice)
		{
			this.SectionList = new ReactiveList<TableSectionInformation<T,V>>();
			this.SectionResult = new List<List<T>> ();

			Search.ObserveOn(RxApp.MainThreadScheduler).Subscribe(results =>
				{
					var group  = SearchResults.GroupBy(g=> g.TypeTitle);

					//var x = group.Select(g => new Dictionary<String,List<StandardDefectViewModel>> { g.Key, null}).ToList();

					SectionList.Clear();
					SectionResult.Clear();

					foreach (var item in group) {

						var sectionItem = item.Select (g => g);
						var reactiveList = new ReactiveList<T> (sectionItem);
						var section = new TableSectionInformation<T,V>(reactiveList,new Foundation.NSString (@"FilterStandardDefect"), 44.0f);
						section.Header = new TableSectionHeader (item.Key);
						SectionList.Add (section);

						SectionResult.Add(sectionItem.ToList());
					}
				});
		}
	}
}

