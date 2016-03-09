

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using WinRunner.Models;
using WinRunner.Models.Shortcuts;
namespace WinRunner.Views {
	public class ShortcutTypeToEnumConverter:IValueConverter {

		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return ShortcutType.Batch;
			} else if (value is Shortcut) {
				return ShortcutType.File;
			}

			return null;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}
}
