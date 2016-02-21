

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
namespace WinRunner.Views {
	public class UserPreferences {
		private Dictionary <string, string> configs = new Dictionary <string,string> ();

		public string DocumentsPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + @"\WinRunner\";

		private string configPath;

		public UserPreferences () {
			this.configPath = this.DocumentsPath + "config.txt";
		}

		public void SetConfig (string config, object value) {
			this.configs [config] = value.ToString ();
		}

		public string GetConfig (string config) {
			if (this.configs.ContainsKey (config)) {
				return this.configs [config];
			} else {
				return null;
			}
		}

		public void Save () {
			FileInfo fileInfo = new FileInfo (this.configPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.SerializeConfigs ());
		}

		public void Load () {
			FileInfo fileInfo = new FileInfo (this.configPath);
			
			if (fileInfo.Exists) {
				this.DeserializeConfigs (File.ReadAllText (fileInfo.FullName));
			}
		}

		private string SerializeConfigs () {
			string serial = "";

			foreach (KeyValuePair <string, string> pair in this.configs) {
				serial += string.Format ("{0}={1}\n", pair.Key, pair.Value);
			}

			return serial;
		}

		private Dictionary <string, string> DeserializeConfigs (string serial) {
			Dictionary <string, string> configs = new Dictionary <string, string> ();
			string [] lines = serial.Split ('\n');

			foreach (string line in lines) {
				Match match = Regex.Match (line, "^(.+)=(.*)$");

				if (match.Success) {
					this.configs [match.Groups [1].Value] = match.Groups [2].Value;
				}
			}

			return configs;
		}

		public const string WindowLeft = "left";
		public const string WindowTop= "top";
		public const string WindowWidth = "width";
		public const string WindowHeight = "height";
	}
}
