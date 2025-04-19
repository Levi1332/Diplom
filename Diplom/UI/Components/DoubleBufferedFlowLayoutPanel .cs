using System.Windows.Forms;

public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
{
    public DoubleBufferedFlowLayoutPanel()
    {
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;

        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.OptimizedDoubleBuffer |
                      ControlStyles.UserPaint |
                      ControlStyles.SupportsTransparentBackColor, true);

        this.UpdateStyles(); 
    }

    protected override void WndProc(ref Message m)
    {
        const int WM_ERASEBKGND = 0x14;
        if (m.Msg == WM_ERASEBKGND)
            return;

        base.WndProc(ref m);
    }
}
