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
			this.BuildTools();

			// Events
			this.DeleteEvent += (o, args) => this.OnClose();
			this.Shown += (sender, e) => this.OnShow();

			// Polish
			this.WindowPosition = Gtk.WindowPosition.Center;
			this.Add( this.vbMain );
			this.Resize( 640, 480 );

			this.SetGeometryHints(
				this,
				new Gdk.Geometry() {
					MinWidth = 320,
					MinHeight = 200
				},
				Gdk.WindowHints.MinSize
			); 
		}

		private void BuildActions() {
			this.actQuit = new Gtk.Action( "Quit", "Quit", "Quit", Gtk.Stock.Quit );
			this.actQuit.Activated += (o, evt) => this.OnClose();

			this.actAbout = new Gtk.Action( "About...", "About...", "About...", Gtk.Stock.About );
			this.actAbout.Activated += (o, evt) => this.OnAbout();

			this.actViewFrames = new Gtk.Action( "ViewFrames", "Frames", "View frames", null );
			this.actViewFrames.Activated += (obj, evt) => this.OnViewFrames();

			this.actViewBoxes = new Gtk.Action( "ViewBoxes", "Boxes", "View boxes", null );
			this.actViewBoxes.Activated += (obj, evt) => this.OnViewBoxes();

			this.actViewNotebook = new Gtk.Action( "ViewNotebook", "Notebook", "View notebook", null );
			this.actViewNotebook.Activated += (obj, evt) => this.OnViewNotebook();
		}

		private void BuildMenu() {
			var menuBar = new Gtk.MenuBar();
			var miFile = new Gtk.MenuItem( "File" );
			var mFile = new Gtk.Menu();
			var miHelp = new Gtk.MenuItem( "Help" );
			var mHelp = new Gtk.Menu();
			var miView = new Gtk.MenuItem( "View" );
			var mView = new Gtk.Menu();

			miFile.Submenu = mFile;
			mFile.Append( this.actQuit.CreateMenuItem() );
			miHelp.Submenu = mHelp;
			mHelp.Append( this.actAbout.CreateMenuItem() );
			miView.Submenu = mView;
			mView.Append( this.actViewBoxes.CreateMenuItem() );
			mView.Append( this.actViewFrames.CreateMenuItem() );
			mView.Append( this.actViewNotebook.CreateMenuItem() );

			menuBar.Append( miFile );
			menuBar.Append( miView );
			menuBar.Append( miHelp );
			this.vbMain.PackStart( menuBar, false, false, 5 );
		}

		private void BuildTools() {
			var hbBox = new Gtk.HBox( false, 5 );
			var vbBox = new Gtk.VBox( false, 5 );

			// Widgets
			this.edText = new Gtk.TextView();
			this.edText.Editable = false;

			var btBoxes = new Gtk.Button( new Gtk.Label( this.actViewBoxes.Label ) );
			btBoxes.Clicked += (sender, e) => this.actViewBoxes.Activate();

			var btFrames = new Gtk.Button( new Gtk.Label( this.actViewFrames.Label ) );
			btFrames.Clicked += (sender, e) => this.actViewFrames.Activate();

			var btNotebook = new Gtk.Button( new Gtk.Label( this.actViewNotebook.Label ) );
			btNotebook.Clicked += (sender, e) => this.actViewNotebook.Activate();

			// Packing everything
			vbBox.PackStart( btBoxes, false, false, 5 );
			vbBox.PackStart( btFrames, false, false, 5 );
			vbBox.PackStart( btNotebook, false, false, 5 );
			hbBox.PackStart( this.edText, true, true, 5 );
			hbBox.PackStart( vbBox, false, false, 5 );
			this.vbMain.PackStart( hbBox, true, true, 5 );
		}

		private Gtk.Action actQuit;
		private Gtk.Action actAbout;
		private Gtk.Action actViewFrames;
		private Gtk.Action actViewBoxes;
		private Gtk.Action actViewNotebook;

		private Gtk.VBox vbMain;
		private Gtk.TextView edText;
	}
}
