

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WinRunner.Models;
using WinRunner.Utils;
using WinRunner.Models.Shortcuts;
using WinRunner.Resources;

namespace WinRunner.Views {
	public class HasErrorsToBackgroundConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (System.Convert.ToBoolean (value)) {
				return Brushes.Pink;
			}

			return Brushes.Transparent;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}

	public class HasErrorsToEditTextConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (System.Convert.ToBoolean (value)) {
				return General.Fix;
			}

			return General.Edit;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException ();
		}
	}

	public class ShortcutTypeToEnumConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return ShortcutType.Batch;
			} else if (value is WebPageShortcut) {
				return ShortcutType.WebPage;
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

	public class ShortcutToTypeNameConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return General.Batch;
			} else if (value is FileShortcut) {
				return General.File;
			} else if (value is FolderShortcut) {
				return General.Folder;
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

	public class ShortcutToDeleteWindowTitleConverter:IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is BatchShortcut) {
				return General.DeleteBatchShortcut;
			} else if (value is FileShortcut) {
				return General.DeleteFileShortcut;
			} else if (value is FolderShortcut) {
				return General.DeleteFolderShortcut;
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
