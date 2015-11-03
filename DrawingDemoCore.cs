using System;
using System.Text;

namespace gtksharp {
	public partial class DrawingDemo {
		private void OnExposeDrawingArea()
		{
			using ( var canvas = Gdk.CairoHelper.Create( this.drawingArea.GdkWindow ) ) {
				// Axis
				canvas.MoveTo( 0, 10 );
				canvas.ShowText( Encoding.UTF8.GetBytes( "y".ToCharArray() ) );
				canvas.LineWidth = 4;
				canvas.MoveTo( 10, 10 );
				canvas.LineTo( 10, 110 );
				canvas.LineTo( 110, 110 );
				canvas.MoveTo( 110, 120 );
				canvas.ShowText( Encoding.UTF8.GetBytes( "x".ToCharArray() ) );
				canvas.Stroke();

				// Data
				canvas.LineWidth = 2;
				canvas.SetSourceRGBA( 255, 0, 0, 255 );
				canvas.MoveTo( 12, 108 );
				canvas.LineTo( 50, 60 );
				canvas.LineTo( 70, 20 );
				canvas.LineTo( 110, 108 );
				canvas.Stroke();

				// Clean
				canvas.GetTarget().Dispose();
			}
		}
	}
}

