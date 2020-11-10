using LeaxDev.WindowStates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisualAutoBot.ProgramNodes;
using WK.Libraries.HotkeyListenerNS;

namespace VisualAutoBot
{
    public partial class MainForm : Form
    {
        public bool IsRunning = false;

        public MainForm()
        {
            InitializeComponent();

            EnsureLoopNode();

            LoadScript();

            SetupHotKeys();
        }

        readonly HotkeyListener _hotkeyListener = new HotkeyListener();
        Hotkey _startStopHotkey;
        private void SetupHotKeys()
        {
            _startStopHotkey = new Hotkey(Keys.Control, Keys.F8);
            _hotkeyListener.Add(_startStopHotkey);
            _hotkeyListener.HotkeyPressed += HotkeyListener_HotkeyPressed;
        }

        private void HotkeyListener_HotkeyPressed(object sender, HotkeyEventArgs e)
        {
            if(e.Hotkey == _startStopHotkey)
            {
                if(IsRunning)
                {
                    toolStopScript.PerformClick();
                }
                else
                {
                    toolStartScript.PerformClick();
                }
            }
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
            if(IsRunning)
            {
                MessageBox.Show("Please stop the script first");
                e.Cancel = true;

                return;
            }

            var settings = Properties.Settings.Default;
            settings.WindowStates.Save(this);
            settings.Save();

            SaveScript();
        }

        private void LoadScript()
        {
            string file = "";

            try
            {
                file = File.ReadAllText("script.json");
            }
            catch (Exception) { }

            JArray json = JArray.Parse(file);
            if (json.Count > 0 && json[0] != null && json[0] is JObject && (json[0] as JObject).ContainsKey("Type") && ((json[0] as JObject)["Type"] as JValue).Value.ToString() == "LoopTreeNode")
            {
                foreach (JObject obj in json)
                {
                    BaseTreeNode.Create(obj, programTreeView.Nodes);
                }
            }
            else
            {
                EnsureLoopNode();
            }
            programTreeView.ExpandAll();
        }

        private void SaveScript()
        {
            JArray json = new JArray();

            foreach(var node in programTreeView.Nodes)
            {
                json.Add(((BaseTreeNode)node).ToJSON());
            }

            try
            {
                Directory.CreateDirectory("backup");
                File.Move("script.json", "backup" + Path.PathSeparator + "script-" + DateTime.Now.ToString("yyyyMMdd'-'HHmmss") + ".json");
            }
            catch(Exception) { }

            File.WriteAllText("script.json", JsonConvert.SerializeObject(json));
        }

        #region TreeView code

        TreeNode clickedNode = null;
        private void programTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = clickedNode == null;
        }

        private void programTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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
                    else if(!IsRunning)
                    {
                        clickedNode = hit.Node;
                    }

                }
            }
            else
            {
                CancelButton_Click(this, null);
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
                        if (item.Value != null)
                        {
                            var label = new Label()
                            {
                                AutoSize = true,
                                Text = item.Key + ":"
                            };
                            panelEditNode.Controls.Add(label);


                            if (item.Value is Bitmap bitmap)
                            {
                                var pictureBox = new PictureBox()
                                {
                                    Width = panelEditNode.Width - 10,
                                    Height = 200,
                                    SizeMode = PictureBoxSizeMode.Normal,
                                    Image = bitmap,
                                };
                                panelEditNode.Controls.Add(pictureBox);
                            }
                            else
                            {
                                var edit = new TextBox()
                                {
                                    Text = item.Value.ToString(),
                                    Width = panelEditNode.Width - 10,
                                    Tag = item.Key,
                                };
                                edit.Margin = new Padding(edit.Margin.Left, edit.Margin.Top, edit.Margin.Right, 15);
                                panelEditNode.Controls.Add(edit);
                            }
                        }
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

        private void ProgramTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(e.Data.GetFormats()[0]) is BaseTreeNode draggedNode))
            {
                return;
            }

            var hit = programTreeView.HitTest(programTreeView.PointToClient(new System.Drawing.Point(e.X, e.Y)));
            if (hit.Node != null && hit.Node.Parent != null)
            {
                draggedNode.Remove();
                hit.Node.Parent.Nodes.Insert(hit.Node.Parent.Nodes.IndexOf(hit.Node), draggedNode);
            }
        }

        private void ProgramTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            BaseTreeNode node = (BaseTreeNode)e.Item;
            DoDragDrop(node, DragDropEffects.Move);
        }

        private void ProgramTreeView_DragOver(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(e.Data.GetFormats()[0]) is BaseTreeNode draggedNode))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var hit = programTreeView.HitTest(programTreeView.PointToClient(new System.Drawing.Point(e.X, e.Y)));
            if (hit.Node != null && hit.Node != draggedNode &&
                hit.Node.Parent != null && hit.Node.Parent == draggedNode.Parent)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        #endregion

        #region ToolBar code

        private void ToolAddNode_Click(object sender, EventArgs e)
        {
            if(clickedNode == null)
            {
                MessageBox.Show("Please select a location to insert.");
                return;
            }

            string selected = CustomDialog.ShowSelectionDialog(BaseTreeNode.AvailableTypes.Select(x => x.Key.Replace("TreeNode", "")).OrderBy(x => x).ToArray());
            if(!string.IsNullOrEmpty(selected))
            {
                BaseTreeNode node = Activator.CreateInstance(BaseTreeNode.AvailableTypes[selected + "TreeNode"]) as BaseTreeNode;
                clickedNode.Nodes.Add(node);

                clickedNode.Expand();
                programTreeView.SelectedNode = clickedNode = node;
                programTreeView_NodeMouseClick(sender, null);
            }
        }

        private void ToolStartScript_Click(object sender, EventArgs e)
        {
            if(IsRunning)
            {
                return;
            }

            CancelButton_Click(this, null);

            BaseTreeNode.SignalToExit = false;
            IsRunning = true;
            toolAddNode.Enabled = false;
            toolStartScript.Enabled = false;
            toolStopScript.Enabled = true;

            foreach (var node in programTreeView.Nodes)
            {
                (node as BaseTreeNode).ClearError();
            }

            Thread runner = new Thread(ScriptRunner);
            runner.Start();
        }

        private void ToolStopScript_Click(object sender, EventArgs e)
        {
            if (!IsRunning)
            {
                return;
            }

            BaseTreeNode.SignalToExit = true;
            toolStopScript.Text = "Stopping ...";
            toolStopScript.Enabled = false;
        }

        private void ToolRemove_Click(object sender, EventArgs e)
        {
            if(clickedNode == null)
            {
                return;
            }

            if (MessageBox.Show("Are you sure?", "Confirm the deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                clickedNode.Remove();
                CancelButton_Click(this, null);
            }
        }

        #endregion

        public void ScriptRunner()
        {
            while (true)
            {
                try
                {
                    (programTreeView.Nodes[0] as LoopTreeNode).Run();
                }
                catch(ScriptException e)
                {
                    if (e.IsFatal)
                    {
                        BaseTreeNode.SignalToExit = true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Exception Throw");
                    BaseTreeNode.SignalToExit = true;
                }

                if (BaseTreeNode.SignalToExit) break;
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

        private void ScriptDelayUpDown_ValueChanged(object sender, EventArgs e)
        {
            BaseTreeNode.Delay = (int)scriptDelayUpDown.Value;
        }
    }
}
