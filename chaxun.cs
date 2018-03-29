using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class chaxun : Form
    {
        bool isdrag = false;
        int x = 0, y = 0;
        public chaxun()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chaxun_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdrag)
            {
                this.Left = this.Left + e.X - x;
                this.Top = this.Top + e.Y - y;
            }
            else

            //控制退出按钮显示
            if (e.Y < 70 && e.X > 700)
                btnexit.Visible = true;
            if (e.Y > 70 || e.X <700)
                btnexit.Visible = false;



        }
        /// <summary>
        /// 鼠标弹起，解除拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chaxun_MouseUp(object sender, MouseEventArgs e)
        {
            isdrag = false;
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 鼠标按下，开始拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chaxun_MouseDown(object sender, MouseEventArgs e)
        {
            isdrag = true;
            x = e.X;
            y = e.Y;
        }

    }
}
