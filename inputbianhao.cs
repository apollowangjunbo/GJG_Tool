using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class inputbianhao : Form
    {
        tongji zct;
        string bh = string.Empty;
        DirectoryInfo d;
        public inputbianhao(tongji zct,string bh,DirectoryInfo d)
        {
            InitializeComponent();
            this.zct = zct;
            this.d = d;
            this.bh = bh;
        }

        private void inputbianhao_Load(object sender, EventArgs e)
        {
            textBox1.Text = bh;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DirectoryInfo[] d1 = d.GetDirectories();
            if (d1.Length > 0)
                foreach (DirectoryInfo d2 in d1)
                    if (d2.Name == textBox1.Text && d2.Name != bh)
                    { MessageBox.Show("编号重复！"); goto end2; }

            zct.xgbh = textBox1.Text;
            this.Close();
        end2:;
        }
    }
}
