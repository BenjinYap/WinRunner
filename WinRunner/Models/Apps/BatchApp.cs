

namespace WinRunner.Models.Apps {
	public class BatchApp:App {

		public override void FlushToRegistry () {
			base.FlushToRegistry ();
			//this.regKey.SetValue ("", this.Path);
		}
	}
}
