

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
namespace WinRunner.Models {
	public class RegistryAppList:ObservableCollection <RegistryApp> {

		public string Serialize () {
			List <string> lines = new List <string> ();
			
			foreach (RegistryApp app in this.Items) {
				lines.Add (string.Format ("{0}|{1}", app.Name, app.Path));
			}

			return string.Join ("\n", lines);
		}

		public List <RegistryApp> Deserialize (string serial) {
			List <RegistryApp> apps = new List <RegistryApp> ();
			string [] lines = serial.Split ('\n');

			foreach (string line in lines) {
				Regex regex = new Regex (@"^([^|]+)\|([^|]+)$");
				Match match = regex.Match (line);
				
				if (match.Success == false) {
					continue;
				}

				string name = match.Groups [1].Value;
				string path = match.Groups [2].Value;
				apps.Add (new RegistryApp (name, path));
			}

			return apps;
		}
	}
}
