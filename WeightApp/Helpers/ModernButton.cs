using System.Drawing.Drawing2D;

namespace WeightApp.Helpers
{
    /// <summary>
    /// Custom button with modern, flat design and gradient hover effect
    /// </summary>
    public class ModernButton : Button
    {
        private bool _isHovered = false;
        private Color _normalColor = ColorTranslator.FromHtml("#022ff5");
        private Color _hoverColor = ColorTranslator.FromHtml("#0341ff");
        private Color _textColor = Color.White;
        private int _borderRadius = 8;

        public Color NormalColor
        {
            get => _normalColor;
            set { _normalColor = value; Invalidate(); }
        }

        public Color HoverColor
        {
            get => _hoverColor;
            set { _hoverColor = value; Invalidate(); }
        }

        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        public ModernButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            ForeColor = _textColor;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded rectangle
            GraphicsPath path = GetRoundedRectangle(ClientRectangle, _borderRadius);

            // Fill with color
            Color fillColor = _isHovered ? _hoverColor : _normalColor;
            using (SolidBrush brush = new SolidBrush(fillColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Draw text
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle,
                ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
