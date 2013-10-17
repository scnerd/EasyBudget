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
    public partial class AddCategory : Form
    {
        private string previous_text;

        public AddCategory()
        {
            InitializeComponent();
            previous_text = capacity.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Categories.GetField<object>("id", "name = '" + capacity.Text + '\'').Count() > 0)
            {
                MessageBox.Show("That category already exists");
            }
            else
            {
                if (Categories.Add(name.Text, double.Parse(capacity.Text)))
                {
                    Main.RefreshDisplay();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid input");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double d;
            if (!double.TryParse(capacity.Text, out d))
            { capacity.Text = previous_text; }
            else
            { previous_text = capacity.Text; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
