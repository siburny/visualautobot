using LeaxDev.WindowStates;
using System;
using System.Windows.Forms;
using VisualAutoBot.ProgramNodes;

namespace VisualAutoBot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            EnsureLoopNode();
        }

        private void EnsureLoopNode()
        {
            if (programTreeView.Nodes.Count == 0)
            {
                var node = new LoopTreeNode() { Text = "Loop" };
                programTreeView.Nodes.Add(node);
            }
            programTreeView.SelectedNode = null;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var settings = Properties.Settings.Default;

            if (settings.WindowStates == null)
            {
                settings.WindowStates = new WindowStates();
            }

            settings.WindowStates.Restore(this, false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var settings = Properties.Settings.Default;

            settings.WindowStates.Save(this);

            settings.Save();
        }

        private void buttonAddNode_Click(object sender, EventArgs e)
        {

        }

        private void programTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = clicked == null;
        }

        private void programTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = programTreeView.HitTest(e.X, e.Y);

            if (hit.Node == null)
            {
                programTreeView.SelectedNode = null;
            }
            else
            {
                if (hit.Node.IsSelected)
                {
                    CancelButton_Click(this, null);
                }
                else
                {
                    clicked = hit.Node;
                }

            }
        }

        TreeNode clicked = null;
        private void programTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (clicked != null)
            {
                panelEditNode.Controls.Clear();

                var node = clicked as BaseTreeNode;

                if (node.Parameters.Count > 0)
                {
                    foreach (var item in node.Parameters)
                    {
                        var label = new Label()
                        {
                            AutoSize = true,
                            Text = item.Key + ":"
                        };

                        var edit = new TextBox()
                        {
                            Text = item.Value.ToString(),
                            Width = panelEditNode.Width - 10,
                            Tag = item.Key,
                        };
                        edit.Margin = new Padding(edit.Margin.Left, edit.Margin.Top, edit.Margin.Right, 15);

                        panelEditNode.Controls.Add(label);
                        panelEditNode.Controls.Add(edit);
                    }

                    var saveButton = new Button()
                    {
                        Text = "SAVE",
                        Width = 100,
                        Height = 35,
                    };
                    saveButton.Click += SaveButton_Click;

                    var cancelButton = new Button()
                    {
                        Text = "Cancel",
                        Width = 100,
                        Height = 35,
                    };
                    cancelButton.Click += CancelButton_Click;

                    panelEditNode.Controls.Add(saveButton);
                    panelEditNode.Controls.Add(cancelButton);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var node = clicked as BaseTreeNode;
            foreach(var control in panelEditNode.Controls)
            {
                if(control is TextBox)
                {
                    var textBox = control as TextBox;
                    node.Parameters[textBox.Tag.ToString()] = textBox.Text;
                }
            }

            CancelButton_Click(this, e);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            panelEditNode.Controls.Clear();
            clicked = null;
            programTreeView.SelectedNode = null;
        }
    }
}
