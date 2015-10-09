using System;

namespace gtksharp {
	public partial class MainWindow {
		private void OnClose() {
			Gtk.Application.Quit();
		}

		private void OnAbout() {
			var dlg = new Gtk.AboutDialog();

			dlg.TransientFor = this;
			dlg.WindowPosition = Gtk.WindowPosition.CenterOnParent;
			dlg.Title = "About...";
			dlg.Authors = new string[]{ "Baltasar" };
			dlg.ProgramName = "Gtk# demo";
			dlg.Version     = "v1.0";
			dlg.Comments    = "Gtk# Demo for basic features";
			dlg.License     = "MIT License";
			dlg.Copyright   = "(c) baltasar 2015";
			dlg.Website     = "http://github.com/Baltasarq/GTKSharpDemo/";

			dlg.Run();
			dlg.Destroy();
		}
	}
}

