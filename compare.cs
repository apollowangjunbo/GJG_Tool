using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class compare : Form
    {
        
        public compare(string fd)
        {
            InitializeComponent();
            this.richTextBox1.Text = fd;
        }

        private void btncomp_Click(object sender, EventArgs e)
        {
            
            //检测左面
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                bool contain = false;
                for (int k=0;k<richTextBox2.Lines.Length;k++)
                {
                    if (richTextBox1.Lines[i] == richTextBox2.Lines[k])
                    {
                        //含有
                        contain = true;
                    }

                }
                if (contain)
                { }
                else
                {
                    richTextBox1.Select(richTextBox1.GetLineFromCharIndex(i), richTextBox1.Lines[i].Length);
                    richTextBox1.SelectionColor =Color.Red;
                }
            }
        }
    }
}
