using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFirewallHelper;

namespace gta_cayo_ATM
{
    class Helper
    {
        public static void check_firewall_on()
        {
            if (!FirewallManager.IsServiceRunning)
            {
                MessageBox.Show("You have to turn on windows firewall for this program to work");
            }
        }

        public static bool check_input(TextBox textbox)
        {
            if (textbox.Text == String.Empty)
            {
                MessageBox.Show("You need to select your GTA5 executable location");
                return false;
            }

            if (!File.Exists(textbox.Text))
            {
                MessageBox.Show("File not exist");
                return false;
            }

            if (textbox.Text.Split('\\').Last().ToString() != "GTA5.exe")
            {
                MessageBox.Show("Make sure you select 'GTA5.exe'");
                return false;
            }

            return true;
        }

        public static void create_and_add_rule(string rule_name, string path)
        {
            // Create Rule
            var rule = FirewallWAS.Instance.CreateApplicationRule(
                rule_name,
                FirewallAction.Block,
                FirewallDirection.Outbound,
                path,
                FirewallProtocol.Any);

            // Add Rule
            FirewallWAS.Instance.Rules.Add(rule);
        }

        public static void remove_rule_if_exist(string rule_name)
        {
            if (FirewallWAS.Instance.Rules.Select(x => x.Name).Contains(rule_name))
            {
                FirewallWAS.Instance.Rules.Remove(rule_name);
            }
        }

        public static void load_path_from_file(TextBox textbox)
        {
            if (!File.Exists("save_file.txt"))
            {
                File.Create("save_file.txt").Close();
            }
            else
            {
                string path = File.ReadAllText("save_file.txt");
                textbox.Text = path;
            }
        }

        public static void load_path_to_file(string path)
        {
            if (!File.Exists("save_file.txt"))
            {
                File.Create("save_file.txt").Close();
            }

            File.WriteAllText("save_file.txt", path);
        }
    }
}
