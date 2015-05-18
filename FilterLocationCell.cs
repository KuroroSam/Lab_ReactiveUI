
using System;

using Foundation;
using UIKit;
using ReactiveUI;
using Core.ViewModels;
namespace testXS
{
	

	partial class FilterLocationCell :  ReactiveTableViewCell, IViewFor<LocationViewModel>
	{
		public FilterLocationCell (IntPtr handle) : base (handle)
		{
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public FilterLocationCell(UITableViewCellStyle style, string cellId)
			: base( UITableViewCellStyle.Value1, cellId) { 

			this.OneWayBind(this.ViewModel, x => x.Title, x => x.TextLabel.Text);
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Text);
			this.OneWayBind (this.ViewModel, x => x.Selected,x=>x.Accessory,null,null,new CheckMarkConverter());
			//this.Accessory = UITableViewCellAccessory.Checkmark;

		}

		public LocationViewModel _viewModel;

		public LocationViewModel ViewModel
		{
			get { return _viewModel; }
			set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ((IViewFor<LocationViewModel>)this).ViewModel; }
			set { ((IViewFor<LocationViewModel>)this).ViewModel = (LocationViewModel)value; }
		}
	}
}
