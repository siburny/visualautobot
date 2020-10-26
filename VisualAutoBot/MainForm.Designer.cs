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
            this.programTreeView = new System.Windows.Forms.TreeView();
            this.buttonAddNode = new System.Windows.Forms.Button();
            this.panelEditNode = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // programTreeView
            // 
            this.programTreeView.FullRowSelect = true;
            this.programTreeView.HideSelection = false;
            this.programTreeView.Location = new System.Drawing.Point(18, 60);
            this.programTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.programTreeView.Name = "programTreeView";
            this.programTreeView.PathSeparator = "::";
            this.programTreeView.ShowRootLines = false;
            this.programTreeView.Size = new System.Drawing.Size(556, 1537);
            this.programTreeView.TabIndex = 0;
            this.programTreeView.TabStop = false;
            this.programTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.programTreeView_BeforeSelect);
            this.programTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.programTreeView_NodeMouseClick);
            this.programTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.programTreeView_MouseDown);
            // 
            // buttonAddNode
            // 
            this.buttonAddNode.Location = new System.Drawing.Point(18, 14);
            this.buttonAddNode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAddNode.Name = "buttonAddNode";
            this.buttonAddNode.Size = new System.Drawing.Size(112, 36);
            this.buttonAddNode.TabIndex = 1;
            this.buttonAddNode.Text = "Add";
            this.buttonAddNode.UseVisualStyleBackColor = true;
            this.buttonAddNode.Click += new System.EventHandler(this.buttonAddNode_Click);
            // 
            // panelEditNode
            // 
            this.panelEditNode.Location = new System.Drawing.Point(590, 60);
            this.panelEditNode.Name = "panelEditNode";
            this.panelEditNode.Size = new System.Drawing.Size(806, 346);
            this.panelEditNode.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2058, 1670);
            this.Controls.Add(this.panelEditNode);
            this.Controls.Add(this.buttonAddNode);
            this.Controls.Add(this.programTreeView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Visual AutoBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView programTreeView;
        private System.Windows.Forms.Button buttonAddNode;
        internal System.Windows.Forms.FlowLayoutPanel panelEditNode;
    }
}
