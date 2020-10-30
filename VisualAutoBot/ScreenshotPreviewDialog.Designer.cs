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
            this.PictureBoxOutput = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxOutput
            // 
            this.PictureBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxOutput.Name = "PictureBoxOutput";
            this.PictureBoxOutput.Size = new System.Drawing.Size(1343, 1152);
            this.PictureBoxOutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PictureBoxOutput.TabIndex = 0;
            this.PictureBoxOutput.TabStop = false;
            // 
            // ScreenshotPreviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 1152);
            this.Controls.Add(this.PictureBoxOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScreenshotPreviewDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.ScreenshotPreviewDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenshotPreviewDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxOutput;
    }
}