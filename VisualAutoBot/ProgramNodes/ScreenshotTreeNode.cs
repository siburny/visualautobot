using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class ScreenshotTreeNode : BaseTreeNode
    {
        public ScreenshotTreeNode()
        {
            NodeText = "Screenshot";

            Parameters.Add("Variable", "screenshot");

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


            IntPtr window = ScreenUtilities.GetWindowByName(name);
            if (window == default)
            {
                MessageBox.Show($"Cannot find game window: {name}");
                return;
            }

            Bitmap bitmap = ScreenUtilities.CaptureWindow(window);

            ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();
            preview.ShowDialog(bitmap);
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if (_data.ContainsKey("Variable"))
            {
                Refresh();
            }

            base.Save(_data);
        }

        public override void Refresh()
        {
            Text = $"{NodeText} ({Parameters["Variable"]})";
        }

        public override void Execute()
        {
            string name = GetVariable("WindowName").ToString();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Can't find gamw window name.");
                return;
            }


            IntPtr window = ScreenUtilities.GetWindowByName(name);
            if (window == default)
            {
                MessageBox.Show($"Cannot find game window: {name}");
                return;
            }

            Bitmap bitmap = ScreenUtilities.CaptureWindow(window);

            SetVariable(Parameters["Variable"].ToString(), bitmap);
        }
    }
}
