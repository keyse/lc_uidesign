using System;
using System.Linq;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace UIV2.classes
{

	public class FluidContentStateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			TileViewItemState tileState = (TileViewItemState)value;
			switch (tileState)
			{
				case TileViewItemState.Maximized:
					return FluidContentControlState.Large;
				case TileViewItemState.Minimized:
					return FluidContentControlState.Small;
				case TileViewItemState.Restored:
					return FluidContentControlState.Normal;
				default:
					return FluidContentControlState.Normal;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            var fluidState = (FluidContentControlState)value;
            switch (fluidState)
            {
                case FluidContentControlState.Small:
                    return TileViewItemState.Minimized;
                case FluidContentControlState.Normal:
                    return TileViewItemState.Restored;
                case FluidContentControlState.Large:
                    return TileViewItemState.Maximized;
                default:
                    return TileViewItemState.Restored;
            }
		}
	}
}
