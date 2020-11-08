using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class ScreenshotTreeNode : BaseTreeNode
    {
        public Bitmap Screenshot = null;

        public ScreenshotTreeNode()
        {
            NodeText = "Screenshot";

            MenuItem previewMenu = new MenuItem("Preview");
            previewMenu.Click += PreviewMenu_Click;

            ContextMenu = new ContextMenu(new MenuItem[] { previewMenu });
        }

        private void PreviewMenu_Click(object sender, EventArgs e)
        {
            string name = GetVariable("WindowName").ToString();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Can't find gamw window name.");
                return;
            }


            Bitmap bitmap = ScreenUtilities.CaptureScreenWindow(name);
            if (bitmap == default)
            {
                MessageBox.Show($"Cannot find game window: {name}");
                return;
            }

            ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();
            preview.ShowDialog(bitmap);
        }

        public override void Save(Dictionary<string, object> _data)
        {
            base.Save(_data);
        }

        public override void Refresh()
        {
            Text = NodeText;
        }

        public override void Execute()
        {
            string name = GetVariable("WindowName").ToString();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Can't find gamw window name.");
                return;
            }


            Bitmap bitmap = ScreenUtilities.CaptureScreenWindow(name);
            if (bitmap == default)
            {
                MessageBox.Show($"Cannot find game window: {name}");
                return;
            }

            SetVariable("Screenshot", bitmap);
            Screenshot = new Bitmap(bitmap);
        }
    }
}
