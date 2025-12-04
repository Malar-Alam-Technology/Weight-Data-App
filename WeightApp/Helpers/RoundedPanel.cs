using System.Drawing.Drawing2D;

namespace WeightApp.Helpers
{
    /// <summary>
    /// Custom panel with rounded corners and shadow effect
    /// </summary>
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 15;
        private Color _backgroundColor = Color.White;
        private bool _hasShadow = true;

        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        public Color BackgroundColor
        {
            get => _backgroundColor;
            set { _backgroundColor = value; Invalidate(); }
        }

        public bool HasShadow
        {
            get => _hasShadow;
            set { _hasShadow = value; Invalidate(); }
        }

        public RoundedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            // Draw shadow
            if (_hasShadow)
            {
                Rectangle shadowRect = new Rectangle(3, 3, Width - 1, Height - 1);
                using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, _borderRadius))
                {
                    using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                    {
                        e.Graphics.FillPath(shadowBrush, shadowPath);
                    }
                }
            }

            // Draw main panel
            using (GraphicsPath path = GetRoundedRectangle(rect, _borderRadius))
            {
                using (SolidBrush brush = new SolidBrush(_backgroundColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            base.OnPaint(e);
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
