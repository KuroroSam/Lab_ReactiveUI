
using System;

using Foundation;
using UIKit;
using ReactiveUI;
using Core.ViewModels;

namespace testXS
{
	/*
	 * MT4112 The registrar found an invalid type `*`. Registering generic types with ObjectiveC is not supported, and may lead to random behavior and/or crashes (for backwards compatibility with older versions of Xamarin.iOS it is possible to ignore this error by passing --unsupported--enable-generics-in-registrar as an additional mtouch argument in the project's iOS Build options page. See 
	 */
	partial class FilterStandardDefectCell :  ReactiveTableViewCell, IViewFor<StandardDefectViewModel>
	{
		public FilterStandardDefectCell (IntPtr handle) : base (handle)
		{
		}

		[Export("initWithStyle:reuseIdentifier:")]
		public FilterStandardDefectCell(UITableViewCellStyle style, string cellId)
			: base( UITableViewCellStyle.Value1, cellId) { 

			this.OneWayBind(this.ViewModel, x => x.Title, x => x.TextLabel.Text);
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Text);
			this.OneWayBind(this.ViewModel, x => x.Count, x => x.DetailTextLabel.Hidden,null, null, new HiddenConverter());
			this.OneWayBind (this.ViewModel, x => x.Selected,x=>x.Accessory,null,null,new CheckMarkConverter());
			//this.Accessory = UITableViewCellAccessory.Checkmark;

		}

		public StandardDefectViewModel _viewModel;

		public StandardDefectViewModel ViewModel
		{
			get { return _viewModel; }
			set { this.RaiseAndSetIfChanged(ref _viewModel, value); }
		}

		object IViewFor.ViewModel
		{
			get { return ((IViewFor<StandardDefectViewModel>)this).ViewModel; }
			set { ((IViewFor<StandardDefectViewModel>)this).ViewModel = (StandardDefectViewModel)value; }
		}
}


}

