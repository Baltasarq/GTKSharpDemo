﻿using System;

namespace gtksharp {
	public partial class MainWindow {
		private void OnClose() {
			Gtk.Application.Quit();
		}

		private void OnShow() {
			this.edText.Buffer.Text = @"Gtk#
Is a graphic toolkit for user interfaces.
It is quiet capable but also quite fixed in what to do and appearance.

You can see in the source code how to create a main menu. More important,
you can check out how to set actions and reuse them though all your code.

Use View >> Boxes to see a dialog with boxes, and View >> Frames with frames.
The main difference is that frames have a title, but you can still put a box inside.
			";
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

		private void OnViewFrames() {
			var dlg = new Gtk.Dialog( "Frames", this, Gtk.DialogFlags.Modal );

			var frame1 = new Gtk.Frame( "<b>Frame1</b>" );
			( (Gtk.Label) frame1.LabelWidget ).UseMarkup = true;
			frame1.Add( new Gtk.Label( "This is frame1" ) );
			var frame2 = new Gtk.Frame( "<b>Frame2</b>" );
			( (Gtk.Label) frame2.LabelWidget ).UseMarkup = true;
			frame2.Add( new Gtk.Label( "This is frame2" ) );
			var frame3 = new Gtk.Frame( "<b>Frame3</b>" );
			( (Gtk.Label) frame3.LabelWidget ).UseMarkup = true;
			frame3.Add( new Gtk.Label( "This is frame3" ) );


			dlg.VBox.PackStart( frame1, true, true, 5 );
			dlg.VBox.PackStart( frame2, true, true, 5 );
			dlg.VBox.PackStart( frame3, true, true, 5 );

			dlg.SetGeometryHints(
				dlg,
				new Gdk.Geometry() {
					MinHeight = 200,
					MinWidth = 320
				},
				Gdk.WindowHints.MinSize
			);

			dlg.AddButton( "Ok", Gtk.ResponseType.Ok );
			dlg.ShowAll();
			dlg.Run();
			dlg.Destroy();
		}

		private void OnViewBoxes() {
			var dlg = new Gtk.Dialog( "Boxes", this, Gtk.DialogFlags.Modal );

			var hbBox1 = new Gtk.HBox( false, 5 );
			hbBox1.Add( new Gtk.Label( "This is hbox 1" ) );
			var hbBox2 = new Gtk.HBox( false, 5 );
			hbBox2.Add( new Gtk.Label( "This is hbox 2" ) );
			var hbBox3 = new Gtk.HBox( false, 5 );
			hbBox3.Add( new Gtk.Label( "This is hbox 3" ) );

			dlg.VBox.PackStart( hbBox1, true, true, 5 );
			dlg.VBox.PackStart( hbBox2, true, true, 5 );
			dlg.VBox.PackStart( hbBox3, true, true, 5 );

			dlg.SetGeometryHints(
				dlg,
				new Gdk.Geometry() {
					MinHeight = 200,
					MinWidth = 320
				},
				Gdk.WindowHints.MinSize
			);

			dlg.AddButton( "Ok", Gtk.ResponseType.Ok );
			dlg.ShowAll();
			dlg.Run();
			dlg.Destroy();
		}
	}
}

