

using System.Windows;
using System.Windows.Controls;
namespace WinRunner.Views.Help.Documents {
	public class HelpDocument:UserControl {
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register ("Title", typeof (string), typeof (HelpDocument));

		public string Title {
			get { return (string) GetValue (TitleProperty); }
			set { SetValue (TitleProperty, value); }
		}
	}
}
