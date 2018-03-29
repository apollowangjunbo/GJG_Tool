using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsFormsApplication6
{
    public partial class frmhuitu : Form
    {
        [DllImportAttribute("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        bool isdrag = false;//判断鼠标是否在拖拽
        int x = 0, y = 0;//鼠标拖拽前坐标临时存储
        public string fileName = string.Empty;//拖拽进来图片的地址
        bool asksave = false;//是否询问保存
        bool isopen = false;
        //bool dragicon = false;


        /// <summary>
        /// 初始化
        /// </summary>
        public frmhuitu()
        {
            InitializeComponent();//初始化 
            //阻止监测是否被其他线程调用（拖入图片和载入config文件为双线程，如使用单线程默认先载入config）
            Label.CheckForIllegalCrossThreadCalls = false;
            Class1.RegisterHotKey(Handle, 110, Class1.KeyModifiers.Alt, Keys.D);
            Class1.RegisterHotKey(Handle, 111, Class1.KeyModifiers.Alt, Keys.X);

        }
        public frmhuitu(string dir4)
        {
            InitializeComponent();//初始化 
            //阻止监测是否被其他线程调用（拖入图片和载入config文件为双线程，如使用单线程默认先载入config）
            Label.CheckForIllegalCrossThreadCalls = false;
            this.fileName = dir4;
            try
            {
                //载入图片
                this.pictureBox1.ImageLocation = fileName;
                findconfig();
            }
            catch (Exception) { MessageBox.Show("文件格式不对"); }
            
            

        }

        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmhuitu_Load(object sender, EventArgs e)
        {
            //this.Opacity = 0.1;
            this.Width = 558;
            this.Height = 689;
            if(label14.Location != new Point(477, 542)|| label13.Location != new Point(477, 501))
                   this.Width = 640;
            AnimateWindow(this.Handle, 600, 0x00080000);
            /* for (int i = 0; i < 18; i++)
             {
                 this.Opacity += 0.05;
                 this.Update();
                 System.Threading.Thread.Sleep(50);
             }*/

        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownpm(object sender, MouseEventArgs e)
        {
            Label labelpm = sender as Label;
            isdrag = true;
            x = e.X;
            y = e.Y;
        }
        /// <summary>
        /// 鼠标移动（拖动参数框）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMovepm(object sender, MouseEventArgs e)
        {
            Label labelpm = sender as Label;
            if (isdrag)
            {
                labelpm.Left = labelpm.Left + e.X - x;
                labelpm.Top = labelpm.Top + e.Y - y;
                asksave = true;

            }
        }
        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseUppm(object sender, MouseEventArgs e)
        {
            Label labelpm = sender as Label;
            //解除拖动状态
            isdrag = false;
            //确保参数框不跑出范围
            if (labelpm.Top < 0)
                labelpm.Top = 0;
            if (labelpm.Top > pictureBox1.Height - labelpm.Height)
                labelpm.Top = pictureBox1.Height - labelpm.Height;
            if (labelpm.Left < 0)
                labelpm.Left = 0;
            if (labelpm.Left > ClientSize.Width - labelpm.Width)
                labelpm.Left = ClientSize.Width - labelpm.Width;

            //在边界上自动移开
            if (labelpm.Left > pictureBox1.Width - labelpm.Width && labelpm.Left < pictureBox1.Width - labelpm.Width / 2)
                labelpm.Left = pictureBox1.Width - labelpm.Width;
            if (labelpm.Left >= pictureBox1.Width - labelpm.Width / 2 && labelpm.Left < pictureBox1.Width)
                labelpm.Left = pictureBox1.Width;

        }
        /// <summary>
        /// 移动PM14自动出现剩下的参数框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label14_LocationChanged(object sender, EventArgs e)
        {
            this.Width = 640;
        }

        /// <summary>
        /// 拖入效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmhuitu_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;

            else e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// 打开拖入文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmhuitu_DragDrop(object sender, DragEventArgs e)
        {


            //获取第一个文件名
            fileName = (e.Data.GetData(DataFormats.FileDrop, false) as String[])[0];
            if (Directory.Exists(fileName))
            {
                DirectoryInfo dir1 = new DirectoryInfo(fileName);
                if (dir1.GetFiles("1.png", SearchOption.AllDirectories).Length < 2)
                {
                    fileName = Path.Combine(fileName, "1.png");
                    if (File.Exists(fileName) == false)
                    {
                        fileName = string.Empty;
                        MessageBox.Show("该文件夹不包含节点文件！");
                    }

                }
                else
                {
                    tongji fr = new tongji(fileName,this);
                    fr.Show();
                    this.Hide();
                }

            }
            if (File.Exists(fileName))
            {
                try
                {
                    //载入图片
                    this.pictureBox1.ImageLocation = fileName;

                    //这里开起了个新线程，用来载入config文件
                    //如不开启新线程，程序会先移动参数框，再载入图片。
                    ThreadStart ths = new ThreadStart(findconfigthr);
                    Thread threadA = new Thread(ths);
                    threadA.IsBackground = true;
                    threadA.Priority = ThreadPriority.Lowest;
                    threadA.Start();
                    //this.findconfig();


                }
                catch (Exception) { MessageBox.Show("文件格式不对"); }
            }
        }
        /// <summary>
        /// 拖入图像后判断是否有config文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void findconfigthr()
        {
            button1.Enabled = true;
            Control[] labelx;
            int lx = 0, ly = 0;
            #region//判断是否有config文件

            if (fileName != string.Empty && File.Exists(Path.Combine(Path.GetDirectoryName(fileName), "config.txt")))
            {
                #region//label归位
                label1.Location = new Point(477, 9);
                label2.Location = new Point(477, 50);
                label3.Location = new Point(477, 91);
                label4.Location = new Point(477, 132);
                label5.Location = new Point(477, 173);
                label6.Location = new Point(477, 214);
                label7.Location = new Point(477, 255);
                label8.Location = new Point(477, 296);
                label9.Location = new Point(477, 337);
                label10.Location = new Point(477, 378);
                label11.Location = new Point(477, 419);
                label12.Location = new Point(477, 460);
                label13.Location = new Point(477, 501);
                label14.Location = new Point(477, 542);
                label15.Location = new Point(554, 9);
                label16.Location = new Point(554, 50);
                label17.Location = new Point(554, 91);
                label18.Location = new Point(554, 132);
                label19.Location = new Point(554, 173);
                label20.Location = new Point(554, 214);
                label21.Location = new Point(554, 255);
                label22.Location = new Point(554, 296);
                label23.Location = new Point(554, 337);
                label24.Location = new Point(554, 378);
                label25.Location = new Point(554, 419);
                label26.Location = new Point(554, 460);
                label27.Location = new Point(554, 501);
                label28.Location = new Point(554, 542);
                //this.Width = 558;
                #endregion//

                StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(fileName), "config.txt"), Encoding.UTF8);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        if (line[0] == 'P' && line[1] == 'M')
                        {
                            if (line[3] == ':')
                                labelx = this.Controls.Find("label" + line[2], false);
                            else
                                labelx = this.Controls.Find("label" + line[2] + line[3], false);

                            lx = Int32.Parse(line.Substring(line.IndexOf("PositionX") + 10, line.IndexOf("PositionY") - line.IndexOf("PositionX") - 10));
                            ly = Int32.Parse(line.Substring(line.IndexOf("PositionY") + 10, line.Length - line.IndexOf("PositionY") - 10));
                            #region//移动参数框（动画）
                            if (labelx.Length > 0)
                            {
                                int addx = 0, addy = 0;
                                //labelx[0].Left = lx;
                                //labelx[0].Top = ly;
                                addx = (lx - labelx[0].Left) / 5;
                                addy = (ly - labelx[0].Top) / 5;
                                for (int i = 0; i < 4; i++)
                                {
                                    labelx[0].Left += addx;
                                    labelx[0].Top += addy;
                                    Thread.Sleep(25);
                                    pictureBox1.Update();
                                }
                                labelx[0].Left = lx;
                                labelx[0].Top = ly;


                            }
                            #endregion

                        }
                    }
                    catch
                    {
                        //MessageBox.Show("config文件编码格式错误");
                    }
                }
                sr.Close();
            }

            #endregion
        }

        /// <summary>
        /// 截图后判断是否有config文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void findconfig()
        {
            button1.Enabled = true;
            Control[] labelx;
            int lx = 0, ly = 0;
            #region//判断是否有config文件
            if (fileName != string.Empty && File.Exists(Path.Combine(Path.GetDirectoryName(fileName), "config.txt")))
            {
                #region//label归位
                label1.Location = new Point(477, 9);
                label2.Location = new Point(477, 50);
                label3.Location = new Point(477, 91);
                label4.Location = new Point(477, 132);
                label5.Location = new Point(477, 173);
                label6.Location = new Point(477, 214);
                label7.Location = new Point(477, 255);
                label8.Location = new Point(477, 296);
                label9.Location = new Point(477, 337);
                label10.Location = new Point(477, 378);
                label11.Location = new Point(477, 419);
                label12.Location = new Point(477, 460);
                label13.Location = new Point(477, 501);
                label14.Location = new Point(477, 542);
                label15.Location = new Point(554, 9);
                label16.Location = new Point(554, 50);
                label17.Location = new Point(554, 91);
                label18.Location = new Point(554, 132);
                label19.Location = new Point(554, 173);
                label20.Location = new Point(554, 214);
                label21.Location = new Point(554, 255);
                label22.Location = new Point(554, 296);
                label23.Location = new Point(554, 337);
                label24.Location = new Point(554, 378);
                label25.Location = new Point(554, 419);
                label26.Location = new Point(554, 460);
                label27.Location = new Point(554, 501);
                label28.Location = new Point(554, 542);
                //this.Width = 558;
                #endregion//

                StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(fileName), "config.txt"), Encoding.UTF8);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line[0] == 'P' && line[1] == 'M')
                    {
                        if (line[3] == ':')
                            labelx = this.Controls.Find("label" + line[2], false);
                        else
                            labelx = this.Controls.Find("label" + line[2] + line[3], false);

                        lx = Int32.Parse(line.Substring(line.IndexOf("PositionX") + 10, line.IndexOf("PositionY") - line.IndexOf("PositionX") - 10));
                        ly = Int32.Parse(line.Substring(line.IndexOf("PositionY") + 10, line.Length - line.IndexOf("PositionY") - 10));
                        #region//移动参数框（动画）
                        if (labelx.Length > 0)
                        {
                            labelx[0].Left = lx;
                            labelx[0].Top = ly;
                        }
                        #endregion

                    }
                }
                sr.Close();
            }
            #endregion
        }



        /// <summary>
        /// 保存按钮（生成2.png/生成config，副本.png）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            #region//生成2.png
            try
            {
                string file2 = fileName.Substring(0, fileName.LastIndexOf("\\")+1)+"2.png";
            new System.Drawing.Bitmap(this.pictureBox1.Image,390,500).Save(file2, System.Drawing.Imaging.ImageFormat.Png);
            
            }
            catch { MessageBox.Show("2.png文件写入失败，可能正在被其他程序访问！"); }
            #endregion
            #region//生成副本.png
            try
            {
                Bitmap fuben = new Bitmap(460, 590);
                Graphics fubengr = Graphics.FromImage(fuben);
                fubengr.CopyFromScreen(PointToScreen(pictureBox1.Location), new Point(0, 0), new Size(460, 590));
                //this.DrawToBitmap(fuben, new Rectangle(0, 0, 460, 590));
                string filefuben = fileName.Substring(0, fileName.LastIndexOf("\\") + 1) + "副本.png";
                fuben.Save(filefuben, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch { MessageBox.Show("副本.png文件写入失败，可能正在被其他程序访问！"); }
            #endregion

            #region//坐标文本
            string configpos = string.Empty;
            foreach (Control labelpm in Controls)
            {
               if (labelpm is Label)
                {
                    if(labelpm.Left <= pictureBox1.Width - labelpm.Width)
                configpos += labelpm.Text + ":PositionX=" + labelpm.Left + " PositionY=" + labelpm.Top + "\r\n";
                    
                }
            }
            
               
            #endregion

            #region//生成config.txt文件
            //string types =string.Empty;
            //string name= string.Empty;
            string erro = string.Empty;
            
            #region//获取types
            //string adr = fileName.Substring(0, fileName.LastIndexOf("\\") -1);
            //try
            //{ types = adr.Substring(adr.LastIndexOf("\\") + 1, 3);
            //    if (types[0] != '1' && types[0] != '2' || types[1] < '0' || types[1] > '9' || types[2] < '0' || types[2] > '9')
            //    { types = "";
            //        erro = "获取types失败，请手动填写";
            //    }
            //}
            //catch
            //{types = "";
            //    erro = "获取types失败，请手动填写\n";
            //}
            #endregion
            //#region//获取name
            //try
            //{
            //    DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(fileName));
            //    FileInfo[] files = dir.GetFiles("*.gjgsn");
            //    FileInfo myfile = files[0];
            //    name = Path.GetFileNameWithoutExtension(myfile.Name);
            //}
            //catch {
            //    name = "";
            //    erro += "获取name失败，请手动填写"; }
            
            //#endregion
            //string config = "Types:"+types+"\r\n"+ "Name:"+name+"\r\n"+configpos;

            //写文件
            try {
                using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(fileName), "config.txt"), false, Encoding.UTF8))
                {
                    sw.Write(configpos);
                }
            }
            catch { MessageBox.Show("config文件写入失败，可能正在被其他程序访问！"); }
            //if (erro != string.Empty)

            #endregion
            System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(fileName));
            if (erro == string.Empty)
            {
                MessageBox.Show("已在源目录下生成:\r\n(1)  2.png、 \r\n(2)  副本.png\r\n(3)  config.txt\r\n感谢使用本工具", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                asksave = false;
            }
            else
            {
                MessageBox.Show(erro, "注意");
                System.Diagnostics.Process.Start(Path.Combine(Path.GetDirectoryName(fileName), "config.txt"));
            }
            
            //MessageBox.Show("该功能正在完善中","敬请期待");
            /*OK(1)获得3.png所在文件夹，作为之后保存路径      
              OK（2）生成2.png                                
               OK（3）窗口内截图，生成副本.png
                OK (4)循环判断28个label是否在图片范围内（按面积一半判断），如有在边界上的参数框，弹出警告
                  OK（5）生成config.txt
                   OK（6）截图按钮，截图后要求用户输入地址
                    OK（7）读入config文件功能*/
        }
        /// <summary>
        /// “关于”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用说明\n"+
                             "      1.拖拽3.png到窗口即可打开图片开始编辑。\n" +
                             "      2.拖动右侧“参数框”即可移动到指定位置。\n" +
                             "      3.当拖动PM14时，会自动出现第二列。\n" +
                             "      4.程序会根据文件夹名称获取types，根据文件夹下的.gjgsn获取name，\n        请确保对应名称正确。\n" +
                             "      5.截图工具中双击截图\n" +
                             "      6.读取作者\n"+
                             "      7.删掉config文件里的types和name"+
                             "                                                         " +
                             "——made by 钢结构产品组 王俊博", "广联达钢结构算量软件节点制作辅助工具6.6", MessageBoxButtons.OK);
        }

        /// <summary>
        /// 截大图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            frmjietu fro = new frmjietu(this,this.pictureBox1.Image,true);
            fro.Show();
            this.WindowState=FormWindowState.Minimized;
        }
        /// <summary>
        /// 工具箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (isopen == false)
            {
                this.Height = 760;
                isopen = true;
            }
            else
            {

                this.Height = 689;
                isopen = false;
            }
        }
        /// <summary>
        /// 小截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            frmjietusmall fro1 = new frmjietusmall(this);
            fro1.Show();
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 查询节点编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            chaxun fro2 = new chaxun();
            fro2.Show();
        }

        /// <summary>
        /// 关闭命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmhuitu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = DialogResult.None;
            if (asksave && fileName.Length != 0)
            {
                re = MessageBox.Show("您还没有保存，是否保存？", "节点辅助程序6.02", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (re == DialogResult.Cancel)
                {//取消关闭
                    e.Cancel = true;
                }

                if (re == DialogResult.Yes)
                {//保存并关闭
                    button1_Click(sender, e);
                }
            }
            fromclosing();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tongji frm = new tongji();
            frm.Show();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 关闭动画
        /// </summary>
        private void fromclosing()
        {
            for (int i = 0; i < 20; i++)
            {
                this.Opacity -= 0.05;
                System.Threading.Thread.Sleep(30);
            }
      
        }

        private void btnsmcad_Click(object sender, EventArgs e)
        {
            frmjietu fro = new frmjietu(this, this.pictureBox1.Image, false);
            fro.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 截图快捷键
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 110:    //大图
                            frmjietu fro = new frmjietu(this,pictureBox1.Image,true);
                            fro.Show();
                            this.WindowState = FormWindowState.Minimized;
                            break;
                        case 111:    //小图
                            frmjietusmall fro1 = new frmjietusmall(this);
                            fro1.Show();
                            this.WindowState = FormWindowState.Minimized;
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

    }

}
