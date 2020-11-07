namespace VisualAutoBot
{
    partial class ScreenshotPreviewDialog
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
            this.PictureBoxOutput = new VisualAutoBot.ImageBoxEx();
            this.PanelMatchSelection = new System.Windows.Forms.Panel();
            this.ButtonSaveSelection = new System.Windows.Forms.Button();
            this.PictureBoxMatchSelection = new VisualAutoBot.ImageBoxEx();
            this.PanelMatchSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBoxOutput
            // 
            this.PictureBoxOutput.AlwaysShowHScroll = true;
            this.PictureBoxOutput.AlwaysShowVScroll = true;
            this.PictureBoxOutput.AutoCenter = false;
            this.PictureBoxOutput.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PictureBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxOutput.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.Medium;
            this.PictureBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxOutput.Name = "PictureBoxOutput";
            this.PictureBoxOutput.SelectionMode = Cyotek.Windows.Forms.ImageBoxSelectionMode.Rectangle;
            this.PictureBoxOutput.Size = new System.Drawing.Size(1836, 1420);
            this.PictureBoxOutput.TabIndex = 0;
            this.PictureBoxOutput.TextDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
            this.PictureBoxOutput.SelectionMoved += new System.EventHandler(this.PictureBoxOutput_Selected);
            this.PictureBoxOutput.SelectionResized += new System.EventHandler(this.PictureBoxOutput_Selected);
            this.PictureBoxOutput.Selected += new System.EventHandler<System.EventArgs>(this.PictureBoxOutput_Selected);
            this.PictureBoxOutput.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxOutput_MouseMove);
            // 
            // PanelMatchSelection
            // 
            this.PanelMatchSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMatchSelection.Controls.Add(this.ButtonSaveSelection);
            this.PanelMatchSelection.Controls.Add(this.PictureBoxMatchSelection);
            this.PanelMatchSelection.Location = new System.Drawing.Point(1643, 1180);
            this.PanelMatchSelection.Name = "PanelMatchSelection";
            this.PanelMatchSelection.Size = new System.Drawing.Size(193, 240);
            this.PanelMatchSelection.TabIndex = 2;
            // 
            // ButtonSaveSelection
            // 
            this.ButtonSaveSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSaveSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSaveSelection.Location = new System.Drawing.Point(-1, 188);
            this.ButtonSaveSelection.Name = "ButtonSaveSelection";
            this.ButtonSaveSelection.Size = new System.Drawing.Size(193, 54);
            this.ButtonSaveSelection.TabIndex = 3;
            this.ButtonSaveSelection.Text = "SELECT";
            this.ButtonSaveSelection.UseVisualStyleBackColor = true;
            this.ButtonSaveSelection.Click += new System.EventHandler(this.ButtonSaveSelection_Click);
            // 
            // PictureBoxMatchSelection
            // 
            this.PictureBoxMatchSelection.AllowZoom = false;
            this.PictureBoxMatchSelection.AlwaysShowHScroll = true;
            this.PictureBoxMatchSelection.AlwaysShowVScroll = true;
            this.PictureBoxMatchSelection.AutoPan = false;
            this.PictureBoxMatchSelection.AutoScroll = false;
            this.PictureBoxMatchSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PictureBoxMatchSelection.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.Tiny;
            this.PictureBoxMatchSelection.Location = new System.Drawing.Point(-1, -1);
            this.PictureBoxMatchSelection.Name = "PictureBoxMatchSelection";
            this.PictureBoxMatchSelection.Size = new System.Drawing.Size(192, 192);
            this.PictureBoxMatchSelection.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
            this.PictureBoxMatchSelection.TabIndex = 2;
            this.PictureBoxMatchSelection.TabStop = false;
            // 
            // ScreenshotPreviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1836, 1420);
            this.Controls.Add(this.PanelMatchSelection);
            this.Controls.Add(this.PictureBoxOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreenshotPreviewDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preview";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenshotPreviewDialog_KeyDown);
            this.PanelMatchSelection.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageBoxEx PictureBoxOutput;
        private System.Windows.Forms.Panel PanelMatchSelection;
        private System.Windows.Forms.Button ButtonSaveSelection;
        private ImageBoxEx PictureBoxMatchSelection;
    }
}