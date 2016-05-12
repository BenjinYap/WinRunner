

using System;
using WinRunner.Resources;
namespace WinRunner.Models.ShortcutProperties {
	public class UrlProperty:ShortcutProperty {

		protected override void Validate () {
			//check length
			if (base.Value.Length <= 0) {
				base.Errors [General.Required] = true;
			} else {
				base.Errors [General.Required] = false;

				Uri uri = null;

				//if not empty, check if valid url
				if (Uri.TryCreate (base.Value, UriKind.Absolute, out uri)) {
					base.Errors [General.Invalid] = false;
				} else {
					base.Errors [General.Invalid] = true;
				}
			}
		}
	}
}
