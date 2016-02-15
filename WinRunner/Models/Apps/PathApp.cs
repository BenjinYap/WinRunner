

using Microsoft.Win32;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Resources;
namespace WinRunner.Models.Apps {
	public class PathApp:App {

		private string path;
		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				this.ValidatePath ();
				base.OnPropertyChanged ();
				this.GetIconFromPath (value);
			}
		}

		private string oldPath;

		public PathApp ():base () {
			this.Path = "";
		}

		public PathApp (RegistryKey regKey):base (regKey) {
			this.Path = regKey.GetValue ("").ToString ();
		}

		public override void RememberProperties () {
			base.RememberProperties ();
			this.oldPath = this.Path;
		}

		public override void RevertProperties () {
			base.RevertProperties ();
			this.Path = this.oldPath;
		}

		public override void FlushToRegistry () {
			base.FlushToRegistry ();
			this.regKey.SetValue ("", this.Path);
		}

		private void ValidatePath ([CallerMemberName] string propertyName = null) {
			if (this.Path.Length <= 0) {
				base.AddError (Resource.PathRequired, propertyName);
			} else {
				base.RemoveError (Resource.PathRequired, propertyName);

				if (File.Exists (this.Path) == false) {
					base.AddError (Resource.PathDoesNotExist, propertyName);
				} else {
					base.RemoveError (Resource.PathDoesNotExist, propertyName);
				}
			}
		}

		private void GetIconFromPath (string path) {
			if (File.Exists (path)) {
				System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon (path);
				Bitmap bitmap = icon.ToBitmap ();

				using (MemoryStream memory = new MemoryStream ())
				{
					bitmap.Save (memory, ImageFormat.Bmp);
					memory.Position = 0;
					BitmapImage bitmapimage = new BitmapImage();
					bitmapimage.BeginInit ();
					bitmapimage.StreamSource = memory;
					bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapimage.EndInit ();
					
					this.Icon = bitmapimage;
				}
			}
		}
	}
}
