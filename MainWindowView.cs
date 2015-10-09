using System;

namespace gtksharp {
	public partial class MainWindow: Gtk.Window {
		public MainWindow()
			:base( Gtk.WindowType.Toplevel )
		{
			this.Build();
		}

		private void Build() {
			this.vbMain = new Gtk.VBox( false, 5 );

			this.BuildActions();
			this.BuildMenu();

			// Events
			this.DeleteEvent += (o, args) => this.OnClose();

			// Polish
			this.WindowPosition = Gtk.WindowPosition.Center;
			this.Add( vbMain );
			this.Resize( 320, 200 );
		}

		private void BuildActions() {
			this.actQuit = new Gtk.Action( "Quit", "Quit", "Quit", Gtk.Stock.Quit );
			this.actQuit.Activated += (o, evt) => this.OnClose();

			this.actAbout = new Gtk.Action( "About...", "About", "About", Gtk.Stock.About );
			this.actAbout.Activated += (o, evt) => this.OnAbout();
		}

		private void BuildMenu() {
			var menuBar = new Gtk.MenuBar();
			var miFile = new Gtk.MenuItem( "File" );
			var mFile = new Gtk.Menu();
			var miHelp = new Gtk.MenuItem( "Help" );
			var mHelp = new Gtk.Menu();

			miFile.Submenu = mFile;
			mFile.Append( this.actQuit.CreateMenuItem() );

			miHelp.Submenu = mHelp;
			mHelp.Append( this.actAbout.CreateMenuItem() );

			menuBar.Append( miFile );
			menuBar.Append( miHelp );
			this.vbMain.PackStart( menuBar, false, false, 5 );
		}

		private Gtk.VBox vbMain;
		private Gtk.Action actQuit;
		private Gtk.Action actAbout;
	}
}
