using System.Drawing.Drawing2D;

namespace WeightApp.Helpers
{
    /// <summary>
    /// Custom panel with gradient background support
    /// </summary>
    public class GradientPanel : Panel
    {
        private Color _topColor = ColorTranslator.FromHtml("#022ff5");
        private Color _bottomColor = ColorTranslator.FromHtml("#ffffff");
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;

        public Color TopColor
        {
            get => _topColor;
            set { _topColor = value; Invalidate(); }
        }

        public Color BottomColor
        {
            get => _bottomColor;
            set { _bottomColor = value; Invalidate(); }
        }

        public LinearGradientMode GradientMode
        {
            get => _gradientMode;
            set { _gradientMode = value; Invalidate(); }
        }

        public GradientPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    ClientRectangle, _topColor, _bottomColor, _gradientMode))
                {
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }
            base.OnPaint(e);
        }
    }
}
