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

            Parameters.Add("Name", "");
            Parameters.Add("XY", "");
            Parameters.Add("WidthHeight", "");
            Parameters.Add("ClickOffset", "");
            Parameters.Add("Template", null);

            MenuItem matchSelectionMenu = new MenuItem("Match Selection");
            matchSelectionMenu.Click += MatchSelectionMenu_Click;

            MenuItem testMatchMenu = new MenuItem("Test Match");
            testMatchMenu.Click += TestMatchMenu_Click;

            ContextMenu = new ContextMenu(new MenuItem[] { matchSelectionMenu, testMatchMenu });
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
            }
            else
            {
                mat = bitmap.ToMat();
            }

            return mat;
        }

        private void TestMatchMenu_Click(object sender, EventArgs e)
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

            if (Parameters["Template"] == null || !(Parameters["Template"] is Bitmap))
            {
                MessageBox.Show("Template is null or not Bitmap");
                return;
            }

            try
            {
                Mat template = (Parameters["Template"] as Bitmap).ToMat().CvtColor(ColorConversionCodes.BGRA2BGR);
                Mat mat = GetMat(bitmap);

                using (Mat result = mat.MatchTemplate(template, TemplateMatchModes.CCoeffNormed))
                {
                    Cv2.MinMaxLoc(result, out _, out double maxValue, out _, out OpenCvSharp.Point maxLocation);

                    if (maxValue > 0.9)
                    {
                        mat.Rectangle(new Rect(maxLocation.X, maxLocation.Y, template.Width, template.Height), Scalar.LightGreen, 3);
                        mat.PutText(maxValue.ToString("0.##"), new OpenCvSharp.Point(maxLocation.X, maxLocation.Y), HersheyFonts.HersheyPlain, 2, Scalar.White, 3);
                    }
                    else if (maxValue > 0.5)
                    {
                        mat.Rectangle(new Rect(maxLocation.X, maxLocation.Y, template.Width, template.Height), Scalar.LightCoral, 3);
                        mat.PutText(maxValue.ToString("0.##"), new OpenCvSharp.Point(maxLocation.X, maxLocation.Y), HersheyFonts.HersheyPlain, 2, Scalar.White, 3);
                    }
                }

                ScreenshotPreviewDialog preview = new ScreenshotPreviewDialog();
                DialogResult res = preview.ShowDialog(mat.ToBitmap());
            }
            catch (OpenCVException ex)
            {
                MessageBox.Show(ex.Message, "OpenCV Exception");
            }
        }

        private void MatchSelectionMenu_Click(object sender, EventArgs e)
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

            if (_data.ContainsKey("ClickOffset"))
            {
                string[] str = _data["ClickOffset"].ToString().Split(',');
                if (str.Length != 2)
                {
                    _data["ClickOffset"] = "";
                }
                else
                {
                    str[0] = str[0].Trim();
                    str[1] = str[1].Trim();

                    Regex regex = new Regex("^-?[0-9]+$");
                    if (!regex.IsMatch(str[0]) || !regex.IsMatch(str[1]))
                    {
                        _data["ClickOffset"] = "";
                    }
                }
            }

            base.Save(_data);
        }

        public override void Refresh()
        {
            Text = $"{NodeText}{(string.IsNullOrEmpty(Parameters["Name"].ToString()) ? "" : ": " + Parameters["Name"])}";
        }

        public override void Execute()
        {
            SetVariable("MatchX", 0);
            SetVariable("MatchY", 0);
            SetVariable("MatchValue", 0);

            Bitmap bitmap = GetVariable<Bitmap>("Screenshot");
            if (bitmap == null)
            {
                throw new ScriptException($"Cannot load screenshot from variable Screenshot", this);
            }

            if (Parameters["Template"] == null || !(Parameters["Template"] is Bitmap))
            {
                throw new ScriptException("Template is null or not Bitmap", this);
            }

            try
            {
                using (Mat template = (Parameters["Template"] as Bitmap).ToMat().CvtColor(ColorConversionCodes.BGRA2BGR))
                using (Mat mat = GetMat(bitmap))
                using (Mat result = mat.MatchTemplate(template, TemplateMatchModes.CCoeffNormed))
                {
                    Cv2.MinMaxLoc(result, out _, out double maxValue, out _, out OpenCvSharp.Point maxLocation);

                    System.Drawing.Point location = new System.Drawing.Point(maxLocation.X, maxLocation.Y);

                    if (!string.IsNullOrEmpty(Parameters["XY"].ToString()))
                    {
                        string[] xy = Parameters["XY"].ToString().Split(',');

                        location.X += Convert.ToInt32(xy[0]);
                        location.Y += Convert.ToInt32(xy[1]);
                    }

                    if (!string.IsNullOrEmpty(Parameters["ClickOffset"].ToString()))
                    {
                        string[] offset = Parameters["ClickOffset"].ToString().Split(',');

                        location.X += Convert.ToInt32(offset[0]);
                        location.Y += Convert.ToInt32(offset[1]);
                    }

                    SetVariable("MatchX", location.X);
                    SetVariable("MatchY", location.Y);
                    SetVariable("MatchValue", maxValue);
                }
            }
            catch (OpenCVException ex)
            {
                throw new ScriptException($"OpenCV Exception: ${ex.Message}", this);
            }
        }
    }
}
