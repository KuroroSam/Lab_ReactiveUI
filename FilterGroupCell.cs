
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
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Hidden,null, null, new HiddenConverter());
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


}

