using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot
{
    public partial class ScreenshotPreviewDialog : Form
    {
        private Bitmap _bitmap;
        public Bitmap Template = null;

        public ScreenshotPreviewDialog()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Bitmap bitmap, bool allowSelection = false)
        {
            PictureBoxOutput.Image = _bitmap = bitmap;
            PictureBoxOutput.SelectionRegion = new RectangleF(0, 0, 0, 0);
            PictureBoxOutput.SelectionMode = allowSelection 
                ? Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle 
                : Cyotek.Windows.Forms.ImageBoxSelectionMode.None;

            PanelMatchSelection.Visible = allowSelection;

            return ShowDialog();
        }

        private void ScreenshotPreviewDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void PictureBoxOutput_Selected(object sender, EventArgs e)
        {
            PictureBoxMatchSelection.Image = _bitmap.Clone(PictureBoxOutput.SelectionRegion, System.Drawing.Imaging.PixelFormat.DontCare);
        }

        private void ButtonSaveSelection_Click(object sender, EventArgs e)
        {
            Template = new Bitmap(PictureBoxMatchSelection.Image);
            DialogResult = DialogResult.OK;
        }
    }
}
