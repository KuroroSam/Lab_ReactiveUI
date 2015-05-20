
using System;

using Foundation;
using UIKit;
using ReactiveUI;
using Core.ViewModels;

namespace testXS
{
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

