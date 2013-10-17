using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EasyBudget
{
    public partial class CSVDump : Form
    {
        public CSVDump()
        {
            InitializeComponent();
            button2.Text = button2.Text.Replace("#", Constants.DAYS_PER_MONTH.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.OpenWrite(saveFileDialog1.FileName));
                write.WriteLine("Category,Amount,Timestamp");

                var data = Main.execute_db_query("select name, amount, timestamp from spend join category on (category.id = spend.category)");
                while (data.Read())
                    write.WriteLine(string.Format("{0},{1},{2}", data[0], data[1], data[2]));

                data.Close();
                write.Close();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter write = new StreamWriter(File.OpenWrite(saveFileDialog1.FileName));
                write.WriteLine("Category,Amount,Timestamp");

                var data = Main.execute_db_query("select name, amount, timestamp from spend join category on (category.id = spend.category) where timestamp >= date('now','-" + Constants.DAYS_PER_MONTH + " days')");
                while (data.Read())
                    write.WriteLine(string.Format("{0},{1},{2}", data[0], data[1], data[2]));

                data.Close();
                write.Close();
                this.Close();
            }
        }
    }
}
