using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class MouseClickTreeNode : BaseTreeNode
    {
        public MouseClickTreeNode()
        {
            NodeText = "MouseClick";

            Parameters.Add("X", 0);
            Parameters.Add("Y", 0);
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if (_data.ContainsKey("Delay"))
            {
                if(int.TryParse(_data["Delay"].ToString(), out int res) && res >= 0)
                {
                    _data["Delay"] = res;
                }
                else
                {
                    _data["Delay"] = 0;
                }
            }

            base.Save(_data);
        }

        public override void Refresh()
        {
            int x = Convert.ToInt32(Parameters["X"]),
                y = Convert.ToInt32(Parameters["Y"]);
            Text = $"{NodeText} ({x}, {y})";
        }

        public override void Execute()
        {
            int x = Convert.ToInt32(Parameters["X"]),
                y = Convert.ToInt32(Parameters["Y"]);

            MouseControl.Click(x, y);
        }
    }
}
