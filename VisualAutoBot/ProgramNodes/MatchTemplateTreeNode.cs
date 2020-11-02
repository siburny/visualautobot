using Newtonsoft.Json.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class MatchTemplateTreeNode : BaseTreeNode
    {
        ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();

        public MatchTemplateTreeNode()
        {
            NodeText = "Match";

            Parameters.Add("Variable", "screenshot");
            Parameters.Add("XY", "");
            Parameters.Add("WidthHeight", "");

            MenuItem previewMenu = new MenuItem("Match Preview");
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
            Mat mat = new Mat();

            if (!string.IsNullOrEmpty(Parameters["XY"].ToString()) && !string.IsNullOrEmpty("WidthHeight"))
            {
                string[] xy = Parameters["XY"].ToString().Split(','),
                widthheight = Parameters["WidthHeight"].ToString().Split(',');

                Mat full = bitmap.ToMat();
                Mat n = new Mat(full, new Rect(int.Parse(xy[0]), int.Parse(xy[1]), int.Parse(widthheight[0]), int.Parse(widthheight[1])));
                n.CopyTo(mat);
            }
            else
            {
                mat = bitmap.ToMat();
            }



            preview.ShowDialog(mat.ToBitmap());
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

            base.Save(_data);
        }

        public override void Refresh()
        {
            Text = $"{NodeText} ({Parameters["Variable"]})";
        }

        public override void Execute()
        {

            return;
            /*

            if (GetVariable("screenshot") == null || !(GetVariable("screenshot") is Bitmap))
            {

                return;
            }
            Bitmap original = GetVariable("screenshot");


            Bitmap bitmap;
            if (!string.IsNullOrEmpty(Parameters["XY"].ToString()) && !string.IsNullOrEmpty("WidthHeight"))
            {
                string[] xy = Parameters["XY"].ToString().Split(','),
                    widthheight = Parameters["WidthHeight"].ToString().Split(',');

                RECT rect = new RECT()
                {
                    left = int.Parse(xy[0]),
                    top = int.Parse(xy[1]),
                };

                rect.right = rect.left + int.Parse(widthheight[0]);
                rect.bottom = rect.top + int.Parse(widthheight[1]);

                bitmap = ScreenUtilities.CaptureWindow(window, rect);
            }
            else
            {
                bitmap = ScreenUtilities.CaptureWindow(window);
            }

            SetVariable(Parameters["Variable"].ToString(), bitmap);*/
        }
    }
}
