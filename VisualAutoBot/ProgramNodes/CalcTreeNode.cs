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
    class CalcTreeNode : BaseTreeNode
    {
        public CalcTreeNode()
        {
            NodeText = "Delay";

            Parameters.Add("Delay", 0);
        }

        public override void Save(Dictionary<string, object> _data)
        {
            if(_data.ContainsKey("Delay"))
            {
                if(int.TryParse(_data["Delay"].ToString(), out int res) && res >= 0)
                {
                    _data["Delay"] = res;
                }
                else
                {
                    _data["Delay"] = 0;
                }

                Text = NodeText + " (" + (res == 0 ? "no delay" : res.ToString() + " ms") + ")";
            }

            base.Save(_data);
        }

        public override void Execute()
        {
            int ms = (int)Parameters["Delay"];

            Thread.Sleep(ms);
        }
    }
}
