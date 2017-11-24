// GTKSharpDemo (c) 2015-17 MIT License <baltasarq@gmail.com>

namespace GTKSharpDemo {
	public class Ppal {
		public static void Main() {
			var wMain = new MainWindow();

			Gtk.Application.Init();
			wMain.ShowAll();
			Gtk.Application.Run();
		}
	}
}
