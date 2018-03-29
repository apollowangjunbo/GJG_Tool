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
    public partial class frmjietusmall : Form
    {
        bool isdrag = false;
        int x = 0, y = 0;
        Bitmap jietusmallre = new Bitmap(140, 160);
        bool iscatch = false;
        frmhuitu zct;

        public frmjietusmall(frmhuitu zct)//初始化
        {
            InitializeComponent();
            this.zct = zct;
            Class1.RegisterHotKey(Handle, 101, Class1.KeyModifiers.None, Keys.NumPad1);
            Class1.RegisterHotKey(Handle, 102, Class1.KeyModifiers.None, Keys.NumPad2);
            Class1.RegisterHotKey(Handle, 103, Class1.KeyModifiers.None, Keys.B);
            Class1.RegisterHotKey(Handle, 104, Class1.KeyModifiers.None, Keys.H);
            Class1.RegisterHotKey(Handle, 105, Class1.KeyModifiers.None, Keys.L);
        }
        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietusmall_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdrag)
            {
                this.Left = this.Left + e.X - x;
                this.Top = this.Top + e.Y - y;
            }
            else
            //控制保存按钮显示
                if(e.Y>110)
                btnsave.Visible = true;
                if (e.Y < 110)
                btnsave.Visible = false;
            //控制退出按钮显示
            if (e.Y < 40&&e.X>120)
                btnexit.Visible = true;
            if (e.Y > 40 ||e.X < 120)
                btnexit.Visible = false;



        }
        /// <summary>
        /// 鼠标弹起，解除拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietusmall_MouseUp(object sender, MouseEventArgs e)
        {
            isdrag = false;
        }
        /// <summary>
        /// 双击截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietusmall_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label2.Visible = false;
            this.Hide();
            //显示用图
            /*Bitmap jietusmall = new Bitmap(460, 590);
            Graphics jietusmallgr = Graphics.FromImage(jietusmall);
            jietusmallgr.CopyFromScreen(this.Left+10,this.Top+10,0,0, new Size(460, 590));*/

            //存储用图

            Graphics jietusmallgrre = Graphics.FromImage(jietusmallre);
            jietusmallgrre.CopyFromScreen(this.Left + 10, this.Top + 10,0, 0, new Size(140, 160));
            iscatch = true;

            this.Show();
            this.BackgroundImage = jietusmallre;
           

        }
        //退出按钮
        private void btnexit_Click(object sender, EventArgs e)
        {
            zct.WindowState=FormWindowState.Normal;
            this.Close();
        }
        //保存按钮
        private void btnsave_Click(object sender, EventArgs e)
        {
            SaveFileDialog file1 = new SaveFileDialog();
            file1.FileName = "1.png";
            file1.Filter = "节点缩略图|1.png";
            if (file1.ShowDialog() == DialogResult.OK)
            {
                if (iscatch == false)
                {
                    this.Hide();
                     Graphics jietusmallgrre = Graphics.FromImage(jietusmallre);
                    jietusmallgrre.CopyFromScreen(this.Left + 10, this.Top + 10, 0, 0, new Size(140, 160));
                    iscatch = true;
                    this.Show();
                    this.BackgroundImage = jietusmallre;
                }
                jietusmallre.Save(file1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(file1.FileName));

            }
            zct.WindowState = FormWindowState.Normal;
            this.Close();

        }

        /// <summary>
        /// 鼠标按下，开始拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietusmall_MouseDown(object sender, MouseEventArgs e)
        {
            isdrag = true;
            x = e.X;
            y = e.Y;
        }

        #region//输入辅助
        int wait = 0;
        Timer timer1 = new Timer();
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 101:    //按下的是1
                            wait = 1;
                            timer1.Enabled = false;
                            timer1.Enabled = true;
                            SendKeys.Send("1");
                            break;
                        case 102:    //按下的是2
                            wait = 2;
                            timer1.Enabled = false;
                            timer1.Enabled = true;
                            SendKeys.Send("2");
                            //此处填写快捷键响应代码
                            break;
                        case 103:    //按下的是B
                            timer1.Enabled = false;
                            if (wait == 1)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S1_b");

                            }
                            else if (wait == 2)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S2_b");

                            }
                            else
                            {
                                Class1.UnregisterHotKey(Handle, 103);
                                SendKeys.Send("b");
                                Class1.RegisterHotKey(Handle, 103, Class1.KeyModifiers.None, Keys.B);
                            }

                            //此处填写快捷键响应代码
                            break;
                        case 104:    //按下的是H
                            timer1.Enabled = false;
                            if (wait == 1)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S1_h");

                            }
                            else if (wait == 2)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S2_h");

                            }
                            else
                            {
                                Class1.UnregisterHotKey(Handle, 104);
                                SendKeys.Send("h");
                                Class1.RegisterHotKey(Handle, 104, Class1.KeyModifiers.None, Keys.H);
                            }

                            //此处填写快捷键响应代码
                            break;

                        case 105:    //按下的是L
                            timer1.Enabled = false;
                            if (wait == 1)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S1_L");

                            }
                            else if (wait == 2)
                            {
                                wait = 0;
                                SendKeys.Send("{BKSP}S2_L");

                            }
                            else
                            {
                                Class1.UnregisterHotKey(Handle, 105);
                                SendKeys.Send("l");
                                Class1.RegisterHotKey(Handle, 105, Class1.KeyModifiers.None, Keys.L);
                            }

                            //此处填写快捷键响应代码
                            break;

                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MessageBox.Show("一秒钟过去了");
            wait = 0;
            timer1.Enabled = false;
        }
        #endregion
    }
}
