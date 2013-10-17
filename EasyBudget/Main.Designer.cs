namespace EasyBudget
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tray_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.context_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saver = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tray_icon
            // 
            this.tray_icon.BalloonTipText = "... is now running";
            this.tray_icon.BalloonTipTitle = "EasyBudget";
            this.tray_icon.ContextMenuStrip = this.context_menu;
            this.tray_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("tray_icon.Icon")));
            this.tray_icon.Text = "EasyBudget";
            this.tray_icon.Visible = true;
            this.tray_icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tray_icon_MouseUp);
            // 
            // context_menu
            // 
            this.context_menu.Name = "contextMenuStrip1";
            this.context_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.context_menu.Size = new System.Drawing.Size(61, 4);
            // 
            // saver
            // 
            this.saver.Enabled = true;
            this.saver.Interval = 60000;
            this.saver.Tick += new System.EventHandler(this.saver_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon tray_icon;
        private System.Windows.Forms.ContextMenuStrip context_menu;
        private System.Windows.Forms.Timer saver;
    }
}

