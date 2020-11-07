using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VisualAutoBot
{
    class CustomDialog : Form
    {
        string selected = "";

        CustomDialog()
        {
            Text = "Please select";
            Size = new Size(400, 500);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MinimizeBox = false;
            MaximizeBox = false;
            Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        }


        public static string ShowSelectionDialog(string[] options)
        {
            CustomDialog dlg = new CustomDialog();

            foreach(var option in options)
            {
                Button btn = new Button();
                btn.Tag = btn.Text = option;
                btn.DialogResult = DialogResult.OK;
                btn.Size = new Size(50, 48);
                btn.Dock = DockStyle.Top;
                btn.Click += dlg.Btn_Click;
                dlg.Controls.Add(btn);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.selected;
            }

            return "";
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            Control ctrl = sender as Control;
            selected = ctrl.Text;
        }
    }
}
