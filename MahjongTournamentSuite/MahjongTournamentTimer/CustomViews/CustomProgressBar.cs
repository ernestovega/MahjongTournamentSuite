using System.Drawing;
using System.Windows.Forms;

namespace MahjongTournamentTimer
{
    public class CustomProgressBar : ProgressBar
    {
        public CustomProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum));
            //if (ProgressBarRenderer.IsSupported)
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(96, 96, 96)), 0, 0, rec.Width, rec.Height);
        }
    }
}
