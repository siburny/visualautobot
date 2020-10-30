using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class ScreenshotTreeNode : BaseTreeNode
    {
        //bool saveScreenshot = false;
        //string outputPath = "output";

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
            //Thread.Sleep(ms);
        }
    }
}
