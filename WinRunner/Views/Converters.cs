

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using WinRunner.Models;
using WinRunner.Models.Apps;
using WinRunner.Utils;
namespace WinRunner.Views {
	public class AppTypeToEnumConverter:IValueConverter {

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchApp) {
				return AppType.Batch;
			} else if (value is App) {
				return AppType.Path;
			}

			return null;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}

	public class GreaterThanZeroToBoolConverter:IValueConverter {

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			return int.Parse (value.ToString ()) > 0;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}
}
