using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EasyBudget
{
    public partial class Main : Form
    {

        internal static SQLiteConnection database;

        private static Main current_instance;

        public static bool execute_db_command(string cmd)
        {
            try
            {
                new SQLiteCommand(cmd, database).ExecuteNonQuery();
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid SQL command: " + cmd);
                return false;
            }
        }

        public static SQLiteDataReader execute_db_query(string qry)
        {
            try
            {
                return new SQLiteCommand(qry, database).ExecuteReader();
            }
            catch
            {
                MessageBox.Show("Invalid SQL query: " + qry);
                return null;
            }
        }

        public Main()
        {
            //Don't allow multiple instances to run
            var me = System.Diagnostics.Process.GetCurrentProcess();
            foreach (var other in System.Diagnostics.Process.GetProcesses())
            {
                if (me.ProcessName == other.ProcessName)
                    Application.Exit();
            }

            current_instance = this;

            //Load data
            if (!System.IO.File.Exists(Constants.DATABASE))
            {
                SQLiteConnection.CreateFile(Constants.DATABASE);
                database = new SQLiteConnection(Constants.CONNECTION_STRING);
                database.Open();
                if (!
                execute_db_command("create table category (id integer primary key autoincrement, name varchar unique, capacity float)") || !
                execute_db_command("create table spend (id integer primary key autoincrement, category int, amount float, timestamp datetime default current_timestamp, foreign key (category) references category(id))")
                    )
                {
                    MessageBox.Show("Failed to create the database");
                    System.IO.File.Delete(Constants.DATABASE);
                    Application.Exit();
                }
            }
            else
            {
                database = new SQLiteConnection(Constants.CONNECTION_STRING);
                database.Open();
            }

            InitializeComponent();

#if DEBUG
            tray_icon.Text += " (DEBUG)";            
#endif
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;

            base.OnLoad(e);
            RefreshMenu();

            tray_icon.ShowBalloonTip(5000);
        }

        private void RefreshMenu()
        {
            context_menu.Items.Clear();

            SQLiteDataReader rows = Categories.GetRows();

            while(rows.Read())
            {
                double used = 0d;
                var used_query = execute_db_query("select sum(amount) from spend where category = " + rows["id"] + " and timestamp >= date('now', '-" + Constants.DAYS_PER_MONTH + " day')");
                if (used_query.Read() && !used_query.IsDBNull(0))
                    used = used_query.GetDouble(0);

                string display_text = rows["name"] + " (" + used + " / " + rows["capacity"] + ") ("
                    + (int)Math.Round(used / (double)rows["capacity"] * 100d) + "%)";

                long temp_id = (long)rows["id"];
                var to_add = new ToolStripButton(display_text, null, (o, a) => context_button_Click(temp_id));
                context_menu.Items.Add(to_add);
            }
            rows.Close();
            context_menu.Items.Add(new ToolStripButton("Add Category", null, (o, a) => new AddCategory().ShowDialog()));
            context_menu.Items.Add(new ToolStripButton("Edit Category", null, (o, a) => new EditCategory().ShowDialog()));
            context_menu.Items.Add(new ToolStripButton("Add Purchase", null, (o, a) => new AddSpending().ShowDialog()));
            context_menu.Items.Add(new ToolStripButton("Dump CSV Files", null, (o, a) => new CSVDump().ShowDialog()));
            context_menu.Items.Add(new ToolStripButton("Exit", null, (o, a) => Application.Exit()));
        }

        void context_button_Click(long clicked)
        {
            AddSpending add = new AddSpending(clicked);
            add.ShowDialog();
        }

        protected void SaveData()
        {
            //Categories.Save();
            //Storage.Save();
        }

        private void saver_Tick(object sender, EventArgs e)
        {
            SaveData();
        }

        public static void RefreshDisplay()
        {
            current_instance.RefreshMenu();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.Close();
#if DEBUG
            System.IO.File.Delete(Constants.DATABASE);
#endif
        }

        private void tray_icon_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                System.Reflection.MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                mi.Invoke(tray_icon, null);
            }

        }
    }
}
