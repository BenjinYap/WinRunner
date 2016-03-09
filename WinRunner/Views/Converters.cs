

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using WinRunner.Models;
using WinRunner.Models.Shortcuts;
using WinRunner.Resources;
namespace WinRunner.Views {
	public class ShortcutTypeToEnumConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return ShortcutType.Batch;
			} else if (value is FileShortcut) {
				return ShortcutType.File;
			} else if (value is FolderShortcut) {
				return ShortcutType.Folder;
			}

			return null;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}

	public class ShortcutToEditWindowTitleConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return General.EditBatchShortcut;
			} else if (value is FileShortcut) {
				return General.EditFileShortcut;
			} else if (value is FolderShortcut) {
				return General.EditFolderShortcut;
			}

			return null;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}
}
