using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsFirewallHelper;

namespace gta_cayo_ATM
{
    public partial class GTA_Cayo_ATM : Form
    {
        public bool btn1_activate = false;

        // Initialize from
        public GTA_Cayo_ATM()
        {
            InitializeComponent();
            Helper.check_firewall_on();
            Helper.load_path_from_file(textBox1);
        }

        // Drag and move window
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void toolbar_panel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
        }

        // Checkbox style is worse than my color choices
        // So imma turn this button into a toggle button, sort of
        private void button1_Click(object sender, EventArgs e)
        {
            if (btn1_activate == false)
            {
                if (Helper.check_input(textBox1))
                {
                    Helper.create_and_add_rule("temp rule", textBox1.Text);
                    rich_label.Text = "Very Rich";
                    rich_label.ForeColor = ColorTranslator.FromHtml("#73a942");
                    btn1_activate = true;
                }
            }
            else
            {
                // Not removing rule directly cuz there's always some
                // mad scientist remove the role manually
                Helper.remove_rule_if_exist("temp rule");
                rich_label.Text = "Not Rich :(";
                rich_label.ForeColor = Color.FromArgb(216, 140, 154);
                btn1_activate = false;
            }
            
        }

        // Opens file dialog and lets user selects the file
        // text box will be the path of the executable
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                Helper.load_path_to_file(dialog.FileName);
            }
        }

        // Check if the rule exist
        // remove it if so
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Helper.remove_rule_if_exist("temp rule");
            base.OnFormClosing(e);
        }

        // close button click
        private void close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Change close button color to red on hover
        private void close_button_MouseEnter(object sender, EventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#f15152");
            close_button.BackColor = color;
        }

        // Change close button color back to original on mouuse leave
        private void close_button_MouseLeave(object sender, EventArgs e)
        {
            close_button.BackColor = Color.FromArgb(132, 165, 157);
        }

        // minimize button click
        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Add drop shadow
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }
    }
}
