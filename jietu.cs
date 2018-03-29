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

    public partial class frmjietu : Form
    {
        bool isdrag = false;
        int x = 0, y = 0;
        Bitmap jieture = new Bitmap(460, 590);
        bool pm0isdrag = false;
        int pm0x = 0, pm0y = 0;
        frmhuitu zct;
        bool iscatch = false;
        bool large = true;

        public frmjietu(frmhuitu zct,Image pic,bool large)//初始化
        {
            InitializeComponent();
            this.zct = zct;
            this.large = large;
            if (pic.Width==460)
                this.BackgroundImage = pic;
        }
        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietu_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdrag)
            {
                this.Left = this.Left + e.X - x;
                this.Top = this.Top + e.Y - y;
            }
            else
            //控制保存按钮显示
                if(e.Y>450)
                btnsave.Visible = true;
                if (e.Y < 450)
                btnsave.Visible = false;
            //控制退出按钮显示
            if (e.Y < 70&&e.X>420)
                btnexit.Visible = true;
            if (e.Y > 70 ||e.X < 420)
                btnexit.Visible = false;
            //PM参数框显示
            if (e.Y < 70 && e.X < 70)
                label1.Visible = true;
            if (e.Y > 70 || e.X > 70)
                label1.Visible = false;


        }
        /// <summary>
        /// 鼠标弹起，解除拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietu_MouseUp(object sender, MouseEventArgs e)
        {
            isdrag = false;
        }
        /// <summary>
        /// 双击截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label2.Visible = false;
            this.Hide();
            //显示用图
            /*Bitmap jietu = new Bitmap(460, 590);
            Graphics jietugr = Graphics.FromImage(jietu);
            jietugr.CopyFromScreen(this.Left+10,this.Top+10,0,0, new Size(460, 590));*/

            //存储用图

            Graphics jietugrre = Graphics.FromImage(jieture);
            jietugrre.CopyFromScreen(this.Left + 10, this.Top + 10,0, 0, new Size(460, 590));
            iscatch = true;

            this.Show();
            this.BackgroundImage = jieture;
           



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
            SaveFileDialog file3 = new SaveFileDialog();
            if (large)
            {
                file3.FileName = "3.png";
                file3.Filter = "节点CAD截图|3.png";
            }
            else
            {
                file3.FileName = "2.png";
                file3.Filter = "节点CAD截图|2.png";
            }
                if (file3.ShowDialog() == DialogResult.OK)
                {
                    /*StreamWriter sw = File.AppendAllText(file3.FileName);
                    sw.Write(jieture);
                    sw.Flush();
                    sw.Close();*/
                    if (iscatch == false)
                    {
                        this.Hide();
                        Graphics jietugrre = Graphics.FromImage(jieture);
                        jietugrre.CopyFromScreen(this.Left + 10, this.Top + 10, 0, 0, new Size(460, 590));
                        iscatch = true;
                        this.Show();
                        this.BackgroundImage = jieture;
                    }
                if (large)
                    jieture.Save(file3.FileName, System.Drawing.Imaging.ImageFormat.Png);
                else
                    new System.Drawing.Bitmap(jieture, 390, 500).Save(file3.FileName, System.Drawing.Imaging.ImageFormat.Png);
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(file3.FileName));
                
            }
            zct.WindowState = FormWindowState.Normal;
            #region//在主窗体内载入图片
            zct.fileName = file3.FileName;
            try
            {
                zct.pictureBox1.ImageLocation = file3.FileName;
                zct.findconfig();
                
            }
            catch (Exception) { MessageBox.Show("文件格式不对"); }
            #endregion

            this.Close();

        }



        /// <summary>
        /// 鼠标按下，开始拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmjietu_MouseDown(object sender, MouseEventArgs e)
        {
            isdrag = true;
            x = e.X;
            y = e.Y;
        }


        #region//PM0拖拽
        private void MouseDownpm0(object sender, MouseEventArgs e)
        {

            pm0isdrag = true;
            pm0x = e.X;
            pm0y = e.Y;
        }

        private void frmjietu_Load(object sender, EventArgs e)
        {

        }

        private void MouseMovepm0(object sender, MouseEventArgs e)
        {
            if (pm0isdrag)
            {
                label1.Left = label1.Left + e.X - pm0x;
                label1.Top = label1.Top + e.Y - pm0y;
            }
        }

        private void MouseUppm0(object sender, MouseEventArgs e)
        {

            //解除拖动状态
            pm0isdrag = false;
            label1.Left = 0;
            label1.Top = 0;
        }



        #endregion
    }
}
