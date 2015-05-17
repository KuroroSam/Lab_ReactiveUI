
using System;

using Foundation;
using UIKit;
using ReactiveUI;
using Core.ViewModels;

namespace testXS
{
	partial class FilterGroupCell :  ReactiveTableViewCell, IViewFor<GroupViewModel>
	{
		public FilterGroupCell (IntPtr handle) : base (handle)
		{
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public FilterGroupCell(UITableViewCellStyle style, string cellId)
			: base( UITableViewCellStyle.Value1, cellId) { 

			this.OneWayBind(this.ViewModel, x => x.Title, x => x.TextLabel.Text);
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Text);
			this.OneWayBind (this.ViewModel, x => x.Selected,x=>x.Accessory,null,null,new CheckMarkConverter());
			//this.Accessory = UITableViewCellAccessory.Checkmark;

		}

		public GroupViewModel _viewModel;

		public GroupViewModel ViewModel
		{
			get { return _viewModel; }
			set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ((IViewFor<GroupViewModel>)this).ViewModel; }
			set { ((IViewFor<GroupViewModel>)this).ViewModel = (GroupViewModel)value; }
		}
}

	public class DueDateConverter : IBindingTypeConverter
	{
		#region IBindingTypeConverter implementation

		public int GetAffinityForObjects(Type lhs, Type rhs)
		{
			// Too bad, I will never be called
			throw new NotImplementedException();
		}

		public bool TryConvert(object from, Type toType, object conversionHint, out object result)
		{
			var dt = (DateTime)from;

			result = dt < DateTime.UtcNow ? "Date lies in the past" : "Date lies in the future";
			return true;
		}

		#endregion
	}
	public class CheckMarkConverter : IBindingTypeConverter
	{
		#region IBindingTypeConverter implementation

		public int GetAffinityForObjects(Type lhs, Type rhs)
		{
			// Too bad, I will never be called
			throw new NotImplementedException();
		}

		public bool TryConvert(object from, Type toType, object conversionHint, out object result)
		{
			var selected = (Boolean)from;

			result = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
			return true;
		}

		#endregion
	}
}

