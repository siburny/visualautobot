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
        public MatchTemplateTreeNode()
        {
            NodeText = "Match";

            Parameters.Add("Variable", "location");
            Parameters.Add("XY", "");
            Parameters.Add("WidthHeight", "");
            Parameters.Add("Template", null);

            MenuItem matchSelectionMenu = new MenuItem("Match Selection");
            matchSelectionMenu.Click += PreviewMenu_Click;

            ContextMenu = new ContextMenu(new MenuItem[] { matchSelectionMenu });
        }

        private Mat GetMat(Bitmap bitmap)
        {
            Mat mat;

            if (!string.IsNullOrEmpty(Parameters["XY"].ToString()))
            {
                string[] xy = Parameters["XY"].ToString().Split(',');
                string[] widthheight = { "", "" };
                if (Parameters["WidthHeight"].ToString() != "")
                {
                    widthheight = Parameters["WidthHeight"].ToString().Split(',');
                }

                int x = int.Parse(xy[0]),
                    y = int.Parse(xy[1]),
                    width,
                    height;

                if (widthheight[0] == "")
                {
                    width = bitmap.Width - x;
                }
                else
                {
                    width = int.Parse(widthheight[0]);
                }

                if (widthheight[1] == "")
                {
                    height = bitmap.Height - y;
                }
                else
                {
                    height = int.Parse(widthheight[1]);
                }

                Mat full = bitmap.ToMat();
                mat = new Mat(full, new Rect(x, y, width, height));

                //mat = new Mat();
                //n.CopyTo(mat);
            }
            else
            {
                mat = bitmap.ToMat();
            }

            return mat;
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
            if (bitmap == null)
            {
                MessageBox.Show($"Cannot find game window: {name}");
                return;
            }

            try
            {
                Mat mat = GetMat(bitmap);

                ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();
                DialogResult res = preview.ShowDialog(mat.ToBitmap(), true);

                if (res == DialogResult.OK)
                {
                    Parameters["Template"] = preview.Template;
                }
            }
            catch (OpenCVException ex)
            {
                MessageBox.Show(ex.Message, "OpenCV Exception");
            }
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
