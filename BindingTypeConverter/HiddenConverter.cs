
using System;

using Foundation;
using UIKit;
using ReactiveUI;
using Core.ViewModels;

namespace testXS
{
	
	public class HiddenConverter : IBindingTypeConverter
	{
		#region IBindingTypeConverter implementation

		public int GetAffinityForObjects(Type lhs, Type rhs)
		{
			// Too bad, I will never be called
			throw new NotImplementedException();
		}

		public bool TryConvert(object from, Type toType, object conversionHint, out object result)
		{
			var selected = (int)from;

			result = selected > 0 ? false : true;
			return true;
		}

		#endregion
	}
}

