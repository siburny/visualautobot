using Newtonsoft.Json.Linq;
using System.Drawing;

namespace VisualAutoBot.ProgramNodes
{
    class ProgramTreeNode : BaseTreeNode
    {
        public ProgramTreeNode() : base()
        {
            NodeText = "Program";

            Parameters.Add("Name", "");

            BackColor = _backColor = Color.LightBlue;
        }

        public override void Execute()
        {
            foreach (var node in Nodes)
            {
                if (SignalToExit) break;
                (node as BaseTreeNode).Run();
            }
        }

        public override void Refresh()
        {
            Text = $"{NodeText}{(!string.IsNullOrEmpty(Parameters["Name"].ToString()) ? ": " + Parameters["Name"].ToString() : "")}";
        }

        public override void FromJSON(JObject json)
        {
            base.FromJSON(json);

            if(json.ContainsKey("Nodes"))
            {
                foreach(JObject obj in json["Nodes"])
                {
                    Create(obj, Nodes);
                }
            }
        }

        public override JToken ToJSON()
        {
            JToken json = base.ToJSON();
            JArray array = new JArray();

            json["Nodes"] = array;
            foreach (var node in Nodes)
            {
                array.Add(((BaseTreeNode)node).ToJSON());
            }

            return json;
        }
    }
}
