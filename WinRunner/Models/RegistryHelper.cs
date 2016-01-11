

using Microsoft.Win32;
namespace WinRunner.Models {
	public static class RegistryHelper {

		public static RegistryKey OpenAppPaths () {
			return Registry.CurrentUser.CreateSubKey (@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths");
		}
	}
}
