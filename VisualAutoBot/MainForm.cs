using LeaxDev.WindowStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisualAutoBot.ProgramNodes;

namespace VisualAutoBot
{
    public partial class MainForm : Form
    {
        public bool IsRunning = false;

        public MainForm()
        {
            InitializeComponent();

            EnsureLoopNode();
        }

        private void EnsureLoopNode()
        {
            if (programTreeView.Nodes.Count == 0)
            {
                var node = new LoopTreeNode();
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

        #region TreeView code

        TreeNode clickedNode = null;
        private void programTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = clickedNode == null;
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
                    clickedNode = hit.Node;
                }

            }
        }

        private void programTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (clickedNode != null)
            {
                panelEditNode.Controls.Clear();

                var node = clickedNode as BaseTreeNode;

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
            var node = clickedNode as BaseTreeNode;
            var data = new Dictionary<string, object>();

            foreach(var control in panelEditNode.Controls)
            {
                if(control is TextBox)
                {
                    var textBox = control as TextBox;
                    data.Add(textBox.Tag.ToString(), textBox.Text);
                }
            }
            node.Save(data);

            CancelButton_Click(this, e);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            panelEditNode.Controls.Clear();
            clickedNode = null;
            programTreeView.SelectedNode = null;
        }

        private void programTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        #endregion

        #region ToolBar code

        private void toolAddNode_Click(object sender, EventArgs e)
        {
            if(clickedNode == null)
            {
                MessageBox.Show("Please select a location to insert.");
                return;
            }

            string selected = CustomDialog.ShowSelectionDialog(BaseTreeNode.AvailableTypes.Select(x => x.Key).ToArray());
            if(!string.IsNullOrEmpty(selected))
            {
                BaseTreeNode node = Activator.CreateInstance(BaseTreeNode.AvailableTypes[selected]) as BaseTreeNode;
                clickedNode.Nodes.Add(node);

                programTreeView.ExpandAll();
                programTreeView.SelectedNode = clickedNode = node;
                programTreeView_NodeMouseClick(sender, null);
            }
        }

        private void toolStartScript_Click(object sender, EventArgs e)
        {
            if(IsRunning)
            {
                return;
            }

            CancelButton_Click(this, null);

            SignalToExit = false;
            IsRunning = true;
            toolAddNode.Enabled = false;
            toolStartScript.Enabled = false;
            toolStopScript.Enabled = true;

            Thread runner = new Thread(ScriptRunner);
            runner.Start();
        }

        private void toolStopScript_Click(object sender, EventArgs e)
        {
            if (!IsRunning)
            {
                return;
            }

            SignalToExit = true;
            toolStopScript.Text = "Stopping ...";
            toolStopScript.Enabled = false;
        }

        #endregion

        bool SignalToExit = false;
        public void ScriptRunner()
        {
            while (true)
            {
                var start = programTreeView.Nodes[0] as LoopTreeNode;

                foreach(var node in start.Nodes)
                {
                    if (SignalToExit) break;
                    
                    (node as BaseTreeNode).Run();
                }

                if (SignalToExit) break;
            }

            Invoke(new Action(() =>
            {
                IsRunning = false;
                toolAddNode.Enabled = true;
                toolStartScript.Enabled = true;
                toolStopScript.Enabled = false;

                toolStopScript.Text = "Stop";
            }));
        }
    }
}
