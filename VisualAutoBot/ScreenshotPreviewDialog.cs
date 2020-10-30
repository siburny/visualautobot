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
        public ScreenshotPreviewDialog()
        {
            InitializeComponent();
        }

        public void ShowDialog(Bitmap bitmap)
        {
            PictureBoxOutput.Image = bitmap;

            base.ShowDialog();
        }

        private void ScreenshotPreviewDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {

            }
        }

        private void ScreenshotPreviewDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
