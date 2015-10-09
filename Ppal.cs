using System;

namespace gtksharp {
	public class Ppal {
		public static void Main(string[] args) {
			var wMain = new MainWindow();

			Gtk.Application.Init();
			wMain.ShowAll();
			Gtk.Application.Run();
		}
	}
}
