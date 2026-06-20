using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SandstoneControls
{
    public enum BackgroundSizeMode
    {
        Zoom,
        GrowOnlyZoom
    }
    public partial class BackgroundForm : Form
    {
        private BackgroundSizeMode m_sizeMode = BackgroundSizeMode.Zoom;

        public new BackgroundSizeMode BackgroundImageLayout { get => m_sizeMode; set { m_sizeMode = value; Invalidate(); } }

        public BackgroundForm() {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pe)
        {
            pe.Graphics.Clear(BackColor);
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            if (BackgroundImage is Image)
            {
                float Ratio = Math.Max((float)ClientSize.Width / BackgroundImage.Width, (float)ClientSize.Height / BackgroundImage.Height);
                float m_width;
                float m_height;
                if (m_sizeMode == BackgroundSizeMode.GrowOnlyZoom) {
                    m_width = Math.Max(BackgroundImage.Width, BackgroundImage.Width * Ratio);
                    m_height = Math.Max(BackgroundImage.Height, BackgroundImage.Height * Ratio);
                } else {
                    m_width = BackgroundImage.Width * Ratio;
                    m_height = BackgroundImage.Height * Ratio;
                }
                pe.Graphics.DrawImage(BackgroundImage, (ClientSize.Width - m_width) / 2f, (ClientSize.Height - m_height) / 2f, m_width, m_height);
            }
        }
    }
}
