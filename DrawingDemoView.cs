using System;

namespace gtksharp {
	public partial class DrawingDemo: Gtk.Dialog {
		public DrawingDemo(Gtk.Window parent)
		{
			this.TransientFor = parent;
			this.Title = "Drawing Demo";

			this.Build();
			this.ShowAll();
		}

		private void Build() {
			var swScroll = new Gtk.ScrolledWindow();

			// Drawing area
			this.drawingArea = new Gtk.DrawingArea();
			this.drawingArea.ExposeEvent += (o, args)  => this.OnExposeDrawingArea();

			// Layout
			swScroll.AddWithViewport( this.drawingArea );
			this.VBox.PackStart( swScroll, true, true, 5 );
			this.AddButton( Gtk.Stock.Close, Gtk.ResponseType.Close );

			// Polish
			this.WindowPosition = Gtk.WindowPosition.CenterOnParent;
			this.Resize( 320, 200 );
			this.SetGeometryHints(
				this,
				new Gdk.Geometry() {
					MinWidth = 320,
					MinHeight = 200
				},
				Gdk.WindowHints.MinSize
			);
		}

		private Gtk.DrawingArea drawingArea;
	}
}

