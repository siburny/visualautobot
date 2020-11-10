using VisualAutoBot.ProgramNodes;

namespace VisualAutoBot
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.programTreeView = new System.Windows.Forms.TreeView();
            this.buttonAddNode = new System.Windows.Forms.Button();
            this.panelEditNode = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolAddNode = new System.Windows.Forms.ToolStripButton();
            this.toolRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStartScript = new System.Windows.Forms.ToolStripButton();
            this.toolStopScript = new System.Windows.Forms.ToolStripButton();
            this.scriptDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptDelayUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // programTreeView
            // 
            this.programTreeView.AllowDrop = true;
            this.programTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.programTreeView.FullRowSelect = true;
            this.programTreeView.HideSelection = false;
            this.programTreeView.Location = new System.Drawing.Point(0, 36);
            this.programTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.programTreeView.Name = "programTreeView";
            this.programTreeView.PathSeparator = "::";
            this.programTreeView.ShowNodeToolTips = true;
            this.programTreeView.ShowRootLines = false;
            this.programTreeView.Size = new System.Drawing.Size(556, 1631);
            this.programTreeView.TabIndex = 0;
            this.programTreeView.TabStop = false;
            this.programTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ProgramTreeView_ItemDrag);
            this.programTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.programTreeView_BeforeSelect);
            this.programTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.programTreeView_NodeMouseClick);
            this.programTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ProgramTreeView_DragDrop);
            this.programTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.ProgramTreeView_DragOver);
            this.programTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.programTreeView_MouseDown);
            // 
            // buttonAddNode
            // 
            this.buttonAddNode.Location = new System.Drawing.Point(0, 0);
            this.buttonAddNode.Name = "buttonAddNode";
            this.buttonAddNode.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNode.TabIndex = 4;
            // 
            // panelEditNode
            // 
            this.panelEditNode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEditNode.Location = new System.Drawing.Point(575, 36);
            this.panelEditNode.Name = "panelEditNode";
            this.panelEditNode.Size = new System.Drawing.Size(779, 1631);
            this.panelEditNode.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddNode,
            this.toolRemove,
            this.toolStripSeparator2,
            this.toolStartScript,
            this.toolStopScript});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1354, 31);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "MainToolStrip";
            // 
            // toolAddNode
            // 
            this.toolAddNode.Image = ((System.Drawing.Image)(resources.GetObject("toolAddNode.Image")));
            this.toolAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAddNode.Name = "toolAddNode";
            this.toolAddNode.Size = new System.Drawing.Size(65, 28);
            this.toolAddNode.Text = "Add";
            this.toolAddNode.Click += new System.EventHandler(this.ToolAddNode_Click);
            // 
            // toolRemove
            // 
            this.toolRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolRemove.Image")));
            this.toolRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRemove.Name = "toolRemove";
            this.toolRemove.Size = new System.Drawing.Size(81, 28);
            this.toolRemove.Text = "Delete";
            this.toolRemove.Click += new System.EventHandler(this.ToolRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStartScript
            // 
            this.toolStartScript.Image = ((System.Drawing.Image)(resources.GetObject("toolStartScript.Image")));
            this.toolStartScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStartScript.Name = "toolStartScript";
            this.toolStartScript.Size = new System.Drawing.Size(68, 28);
            this.toolStartScript.Text = "Start";
            this.toolStartScript.Click += new System.EventHandler(this.ToolStartScript_Click);
            // 
            // toolStopScript
            // 
            this.toolStopScript.Enabled = false;
            this.toolStopScript.Image = ((System.Drawing.Image)(resources.GetObject("toolStopScript.Image")));
            this.toolStopScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStopScript.Name = "toolStopScript";
            this.toolStopScript.Size = new System.Drawing.Size(68, 28);
            this.toolStopScript.Text = "Stop";
            this.toolStopScript.Click += new System.EventHandler(this.ToolStopScript_Click);
            // 
            // scriptDelayUpDown
            // 
            this.scriptDelayUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptDelayUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.scriptDelayUpDown.Location = new System.Drawing.Point(1233, 0);
            this.scriptDelayUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.scriptDelayUpDown.Name = "scriptDelayUpDown";
            this.scriptDelayUpDown.Size = new System.Drawing.Size(120, 30);
            this.scriptDelayUpDown.TabIndex = 5;
            this.scriptDelayUpDown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.scriptDelayUpDown.ValueChanged += new System.EventHandler(this.ScriptDelayUpDown_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 1667);
            this.Controls.Add(this.scriptDelayUpDown);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelEditNode);
            this.Controls.Add(this.buttonAddNode);
            this.Controls.Add(this.programTreeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Visual AutoBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scriptDelayUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView programTreeView;
        private System.Windows.Forms.Button buttonAddNode;
        internal System.Windows.Forms.FlowLayoutPanel panelEditNode;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolAddNode;
        private System.Windows.Forms.ToolStripButton toolStartScript;
        private System.Windows.Forms.ToolStripButton toolStopScript;
        private System.Windows.Forms.ToolStripButton toolRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.NumericUpDown scriptDelayUpDown;
    }
}
