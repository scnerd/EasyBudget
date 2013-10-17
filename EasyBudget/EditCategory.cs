using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyBudget
{
    public partial class EditCategory : Form
    {
        private string previous_text;
        private long category;

        public EditCategory(long Category = -1)
        {
            this.category = Category;

            InitializeComponent();
            previous_text = textBox1.Text;
            var name_list = Categories.GetField<object>("name").ToList();
            comboBox1.Items.AddRange(name_list.ToArray());
            if (Category >= 0)
                for (int i = 0; i < name_list.Count(); i++)
                {
                    var res = Categories.GetField<long>("id", "name = '" + name_list[i] + '\'', 1);
                    if (res.Count() > 0 && res.ElementAt(0) == Category)
                    {
                        comboBox1.SelectedIndex = i;
                        break;
                    }
                }
            else
                comboBox1.Text = "";

            textBox1.Text = "0.00";
            if (Category != -1)
            {
                var results = Categories.GetField<double>("capacity", "id = " + Category);
                if (results.Count() > 0)
                    textBox1.Text = results.ElementAt(0).ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double d;
            if (!double.TryParse(textBox1.Text, out d))
            { textBox1.Text = previous_text; }
            else
            { previous_text = textBox1.Text; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                long cat_id = Categories.GetField<long>("id", "name = '" + comboBox1.Text + '\'', 1).ElementAt(0);
                Main.execute_db_command(string.Format
                ("UPDATE category SET capacity={0} WHERE name='{1}'",
                textBox1.Text,
                comboBox1.Text));
                Main.RefreshDisplay();
                this.Close();
            }
            catch
            { MessageBox.Show("Invalid input"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var results = Categories.GetField<double>("capacity", "name = '" + comboBox1.Text + '\'');
            if (results.Count() > 0)
                textBox1.Text = results.ElementAt(0).ToString();
            else
                textBox1.Text = "error";
        }

    }
}
