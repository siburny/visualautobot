using System.Drawing;
using System.Windows.Forms;

namespace VisualAutoBot.ProgramNodes
{
    class CommentTreeNode : BaseTreeNode
    {
        public CommentTreeNode()
        {
            NodeText = "Comment";

            Parameters.Add("Name", "Comment");

            BackColor = _backColor = Color.LightGray;
        }

        public override void Refresh()
        {
            Text = $"   ### {Parameters["Name"]} ###   ";
        }

        public override void Execute()
        {
        }
    }
}
