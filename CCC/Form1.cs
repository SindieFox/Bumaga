using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCC {
    public partial class Form1 : Form {
        private Color[] colorBG = { Color.White, Color.LightBlue, Color.LightCoral, Color.PaleGreen, Color.Gainsboro, Color.LemonChiffon, Color.Thistle };
        private RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Bumaga");

        public Form1() {
            InitializeComponent();

            this.Location = (key.GetValue("PosX") == null) ? new Point(200, 200) : new Point(Convert.ToInt32(key.GetValue("PosX")), Convert.ToInt32(key.GetValue("posY")));

            if (key.GetValue("SizeWidht") != null)
                this.Size = new Size(Convert.ToInt32(key.GetValue("SizeWidht")), Convert.ToInt32(key.GetValue("SizeHeight")));
            
            this.textBox1.Text = Convert.ToString(key.GetValue("Text"));
            this.textBox1.BackColor = colorBG[Convert.ToInt32(key.GetValue("Color"))];

            this.comboBox1.SelectionChangeCommitted += new EventHandler(this.СolorSelection);
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            key.SetValue("SizeWidht", this.Width);
            key.SetValue("SizeHeight", this.Height);
            key.SetValue("PosX", this.Location.X);
            key.SetValue("PosY", this.Location.Y);
            key.SetValue("Text", this.textBox1.Text);
        }

        private void СolorSelection(object sender, EventArgs e) {
            this.textBox1.BackColor = colorBG[this.comboBox1.SelectedIndex];
            key.SetValue("Color", this.comboBox1.SelectedIndex);
        }

    }
}
