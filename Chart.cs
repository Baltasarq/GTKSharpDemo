// GTKSharpDemo (c) 2015-17 MIT License <baltasarq@gmail.com>

namespace GTKSharpDemo {
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Draws a simple chart.
    /// </summary>
    public class Chart: Gtk.DrawingArea {
        public enum ChartType { Lines, Bars };
        
        public Chart(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.values = new List<int>();
            this.FrameWidth = 50;
            this.Type = ChartType.Lines;
            this.AxisLineWidth = 10;
            this.DataLineWidth = 4;
            this.LegendsFontSize = 14;
            this.AxisLineColor = new Cairo.Color( 0, 0, 0 );
            this.DataLineColor = new Cairo.Color( 255, 0, 0 );
        
            this.SetSizeRequest( width, height );
            this.ExposeEvent += (o, args)  => this.OnExposeDrawingArea();
        }
        
        /// <summary>
        /// Redraws the chart
        /// </summary>
        private void OnExposeDrawingArea()
        {
            using (var canvas = Gdk.CairoHelper.Create( this.GdkWindow )) {
                this.DrawAxis( canvas );
                this.DrawData( canvas );
                this.DrawLegends( canvas );
                canvas.Stroke();

                // Clean
                canvas.GetTarget().Dispose();
            }
        }
        
        private void DrawLine(Cairo.Context canvas, Gdk.Point p1, Gdk.Point p2)
        {
            this.DrawLine( canvas, p1.X, p1.Y, p2.X, p2.Y );
        }
        
        private void DrawLine(Cairo.Context canvas,
                                int x1, int y1, int x2, int y2)
        {
            canvas.MoveTo( x1, y1 );
            canvas.LineTo( x2, y2 );
        }
        
        private void DrawString(Cairo.Context canvas, Gdk.Point pos, string text)
        {
            canvas.MoveTo( pos.X, pos.Y );
            canvas.ShowText( text );
        }
        
        private void DrawLegends(Cairo.Context canvas)
        {
            canvas.SetFontSize( this.LegendsFontSize );
            
            this.DrawString(
                    canvas,
                    new Gdk.Point( 
                        this.FrameWidth,
                        this.FramedEndPosition.Y + ( this.FrameWidth / 2 ) ),
                    this.LegendX );

            this.DrawString(
                    canvas,
                    new Gdk.Point( 
                        this.FramedOrgPosition.X - ( this.FrameWidth / 2 ),
                        this.FrameWidth ),
                    this.LegendY );

            canvas.Stroke();
        } 
        
        private void DrawData(Cairo.Context canvas)
        {
            int numValues = this.values.Count;
            var p = this.DataOrgPosition;
            int xGap = this.GraphWidth / ( numValues + 1 );
            int baseLine = this.DataOrgPosition.Y;

            canvas.LineWidth = this.DataLineWidth;
            canvas.SetSourceColor( this.DataLineColor );
            
            this.NormalizeData();
            for(int i = 0; i < numValues; ++i) {
                string tag = this.values[ i ].ToString();
                var nextPoint = new Gdk.Point(
                    p.X + xGap, baseLine - this.normalizedData[ i ]
                );
                
                if ( this.Type == ChartType.Bars ) {
                    p = new Gdk.Point( nextPoint.X, baseLine );
                }
                
                this.DrawLine( canvas, p, nextPoint );
                this.DrawString( canvas,
                                 new Gdk.Point( nextPoint.X,
                                                nextPoint.Y ),
                                 tag );
                p = nextPoint;
            }
            
            canvas.Stroke();
        }
        
        private void DrawAxis(Cairo.Context canvas)
        {
            canvas.LineWidth = this.AxisLineWidth;
            canvas.SetSourceColor( this.AxisLineColor );
        
            // Y axis
            this.DrawLine( canvas, this.FramedOrgPosition,
                               new Gdk.Point(
                                        this.FramedOrgPosition.X,
                                        this.FramedEndPosition.Y ) );
                                        
            // X axis
            this.DrawLine( canvas, new Gdk.Point(
                                        this.FramedOrgPosition.X,
                                        this.FramedEndPosition.Y ),
                               this.FramedEndPosition ); 
                               
            canvas.Stroke();                              
        }

        private void NormalizeData()
        {
            int maxHeight = this.DataOrgPosition.Y - this.FrameWidth;
            int maxValue = this.values.Max();

            this.normalizedData = this.values.ToArray();

            for(int i = 0; i < this.normalizedData.Length; ++i) {
                this.normalizedData[ i ] =
                                    ( this.values[ i ] * maxHeight ) / maxValue;
            }
            
            return;
        }
        
        /// <summary>
        /// Gets or sets the values used as data.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<int> Values {
            get {
                return this.values.ToArray();
            }
            set {
                this.values.Clear();
                this.values.AddRange( value );
            }
        }
        
        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Gdk.Point"/>.</value>
        public Gdk.Point DataOrgPosition {
            get {
                int margin = (int) ( this.AxisLineWidth * 2 );
                
                return new Gdk.Point(
                    this.FramedOrgPosition.X + margin,
                    this.FramedEndPosition.Y - margin );
            }
        }
        
        /// <summary>
        /// Gets or sets the width of the frame around the chart.
        /// </summary>
        /// <value>The width of the frame.</value>
        public int FrameWidth {
            get; set;
        }
        
        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Gdk.Point"/>.</value>
        public Gdk.Point FramedOrgPosition {
            get {
                return new Gdk.Point( this.FrameWidth, this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the framed end.
        /// </summary>
        /// <value>The end <see cref="Gdk.Point"/>.</value>
        public Gdk.Point FramedEndPosition {
            get {
                return new Gdk.Point( this.Width - this.FrameWidth,
                                  this.Height - this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the width of the graph.
        /// </summary>
        /// <value>The width of the graph.</value>
        public int GraphWidth {
            get {
                return this.Width - ( this.FrameWidth * 2 );
            }
        }

        /// <summary>
        /// Gets or sets the legend for the x axis.
        /// </summary>
        /// <value>The legend for axis x.</value>
        public string LegendX {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the legend for the y axis.
        /// </summary>
        /// <value>The legend for axis y.</value>
        public string LegendY {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the type of the chart.
        /// </summary>
        /// <value>The <see cref="ChartType"/>.</value>
        public ChartType Type {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the width of the axis bars.
        /// </summary>
        /// <value>The width of the axis.</value>
        public int AxisLineWidth {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the color of the axis bars.
        /// </summary>
        /// <value>The <see cref="Cairo.Color"/>  of the axis lines.</value>
        public Cairo.Color AxisLineColor {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the width of the data lines/bars.
        /// </summary>
        /// <value>The width of the data lines/bars.</value>
        public int DataLineWidth {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the color of the data lines/bars.
        /// </summary>
        /// <value>The <see cref="Cairo.Color"/>  of the data lines/bars.</value>
        public Cairo.Color DataLineColor {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the size of the legends font.
        /// </summary>
        /// <value>The size of the legends font.</value>
        public double LegendsFontSize {
            get; set;
        }
        
        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height {
            private set; get;
        }
        
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width {
            private set; get;
        }

        private List<int> values;
        private int[] normalizedData;
    }
}
