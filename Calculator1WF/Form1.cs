using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Calculator1WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CalculateEquation()
        {
            try
            {

                var input = this.txtbx_UserInput.Text;

                input = input.Replace(" ", "");
                this.calculationResultLbl.Text = input;
                input = input.Replace("x", "*");
                var result = new DataTable().Compute(input, null);
                if (Double.IsInfinity(Convert.ToDouble(result)))
                {
                    throw new InvalidOperationException($"Impossible to divide any number to '0'");
                }
                this.txtbx_UserInput.Text = result.ToString();
                this.txtbx_UserInput.Select(txtbx_UserInput.Text.Length, 0);     

                FocusInputText();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show($"Invalid equation. {ex.Message}", "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearUserInput();
            }
        }

        private void InsertTextValue(string value)
        {
            var selectionStart = this.txtbx_UserInput.SelectionStart;

            this.txtbx_UserInput.Text = this.txtbx_UserInput.Text.Insert(this.txtbx_UserInput.SelectionStart, value);

            this.txtbx_UserInput.SelectionStart = selectionStart + value.Length;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Guna2CircleButton btn)
            {
                InsertTextValue(btn.Text);

                FocusInputText();
            }

            if (sender is Guna2GradientCircleButton btn1)
            {
                InsertTextValue(btn1.Text);

                FocusInputText();
            }

            if (sender is Guna2GradientButton btn2)
            {
                InsertTextValue(btn2.Text);

                FocusInputText();
            }

            if (sender is Guna2Button btn3)
            {
                InsertTextValue(btn3.Text);

                FocusInputText();
            }

        }

        private void FocusInputText()
        {
            this.txtbx_UserInput.Focus();
        }

        private void EqualBtn_Click(object sender, EventArgs e)
        {
            CalculateEquation();
        }

        private void CBtn_Click(object sender, EventArgs e)
        {
            ClearUserInput();
        }

        private void ClearUserInput()
        {
            this.txtbx_UserInput.Text = string.Empty;
        }
    }
}
