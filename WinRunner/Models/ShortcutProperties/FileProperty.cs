

using System.IO;
using WinRunner.Resources;
namespace WinRunner.Models.ShortcutProperties {
	public class FileProperty:ShortcutProperty {

		protected override void Validate () {
			//check length
			if (base.Value.Length <= 0) {
				base.Errors [General.Required] = true;
			} else {
				base.Errors [General.Required] = false;

				//if not empty, check if file exists
				if (File.Exists (base.Value) == false) {
					base.Errors [General.DoesNotExist] = true;
				} else {
					base.Errors [General.DoesNotExist] = false;
				}
			}
		}
	}
}
