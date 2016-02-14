

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using WinRunner.Models;
using WinRunner.Models.Apps;
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
}
