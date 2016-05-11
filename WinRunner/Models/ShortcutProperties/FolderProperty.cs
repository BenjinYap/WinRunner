

using System.IO;
using WinRunner.Resources;
namespace WinRunner.Models.ShortcutProperties {
	public class FolderProperty:ShortcutProperty {

		protected override void Validate () {
			if (this.Value.Length <= 0) {
				base.Errors [General.Required] = true;
			} else {
				base.Errors [General.Required] = false;
				
				if (Directory.Exists (this.Value) == false) {
					base.Errors [General.DoesNotExist] = true;
				} else {
					base.Errors [General.DoesNotExist] = false;
				}
			}
		}
	}
}
