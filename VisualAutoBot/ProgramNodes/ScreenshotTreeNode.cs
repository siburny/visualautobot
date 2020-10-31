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
        //bool saveScreenshot = false;
        //string outputPath = "output";
        ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();

        public ScreenshotTreeNode()
        {
            NodeText = "Screenshot";

            Parameters.Add("Variable", "screenshot");
            Parameters.Add("XY", "");
            Parameters.Add("WidthHeight", "");

            MenuItem previewMenu = new MenuItem("Preview");
            previewMenu.Click += PreviewMenu_Click;

            ContextMenu = new ContextMenu(new MenuItem[] { previewMenu });
        }

        private void PreviewMenu_Click(object sender, EventArgs e)
        {
            string name = GetVariable("WindowName").ToString();
            if(string.IsNullOrEmpty(name))
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

            preview.ShowDialog(bitmap);
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if (_data.ContainsKey("Variable"))
            {
                Refresh();
            }

            if (_data.ContainsKey("XY"))
            {
                string[] str = _data["XY"].ToString().Split(',');
                if (str.Length != 2)
                {
                    _data["XY"] = "";
                }
                else
                {
                    str[0] = str[0].Trim();
                    str[1] = str[1].Trim();

                    Regex regex = new Regex("[^0-9]");
                    if (regex.IsMatch(str[0]) || regex.IsMatch(str[1]))
                    {
                        _data["XY"] = "";
                    }
                }
            }

            if (_data.ContainsKey("WidthHeight"))
            {
                string[] str = _data["WidthHeight"].ToString().Split(',');
                if (str.Length != 2)
                {
                    _data["WidthHeight"] = "";
                }
                else
                {
                    str[0] = str[0].Trim();
                    str[1] = str[1].Trim();

                    Regex regex = new Regex("[^0-9]");
                    if (regex.IsMatch(str[0]) || regex.IsMatch(str[1]))
                    {
                        _data["WidthHeight"] = "";
                    }
                }
            }

            if (_data.ContainsKey("WidthHeight"))
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
            //Thread.Sleep(ms);
        }
    }
}
