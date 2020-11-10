using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VisualAutoBot.Expressions;

namespace VisualAutoBot.ProgramNodes
{
    class MouseClickTreeNode : BaseTreeNode
    {
        public MouseClickTreeNode()
        {
            NodeText = "MouseClick";

            Parameters.Add("X", "MatchX");
            Parameters.Add("Y", "MatchY");

            MenuItem previewMenu = new MenuItem("View Coordinates");
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
            if (_data.ContainsKey("X"))
            {
                if (!Parser.CanParseDouble(_data["X"].ToString()))
                {
                    _data["X"] = "";
                }
            }

            if (_data.ContainsKey("Y"))
            {
                if (!Parser.CanParseDouble(_data["Y"].ToString()))
                {
                    _data["Y"] = "";
                }
            }

            base.Save(_data);
        }

        public override void Refresh()
        {
            if (string.IsNullOrEmpty(Parameters["X"].ToString()) || string.IsNullOrEmpty(Parameters["Y"].ToString()))
            {
                Text = NodeText;
            }
            else
            {
                Text = $"{NodeText} ({Parameters["X"]}, {Parameters["Y"]})";
            }
        }

        public override void Execute()
        {
            double x = Parser.ParseDouble(Parameters["X"].ToString()).EvalDouble(this),
                y = Parser.ParseDouble(Parameters["Y"].ToString()).EvalDouble(this);

            MouseControl.Click((int)x, (int)y);

            ToolTipText = $"Clicked at ({x}, {y})";
        }
    }
}
