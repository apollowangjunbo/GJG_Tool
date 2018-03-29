using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace WindowsFormsApplication6
{
    public partial class tongji : Form
    {
        #region//声明全局变量
        string dir = string.Empty;
        bool ison = false;
        frmhuitu zct;
        bool back=false;
        public string xgbh = string.Empty;
        //测试移动文件夹
        bool movefortest = false;
        //遍历所有节点得到子节点的集合
        List<TreeNode> treelist = new List<TreeNode>();
        #endregion

        #region//程序初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public tongji()
            {
                InitializeComponent();
            }
        public tongji(string dir2)
        {
            InitializeComponent();
            this.dir = dir2;
        }
        public tongji(string dir2, frmhuitu zct)
        {
            InitializeComponent();
            
            this.dir = dir2;
            this.zct = zct;
            back = true;
        }
        #endregion

        #region//打开方式
        /// <summary>
        /// 判断是否在节点库文件夹下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tongji_Load(object sender, EventArgs e)
            {
            if (dir == string.Empty)
            {
                if ((Path.GetFileName(Application.StartupPath)).Contains("节点库") || Directory.GetDirectories(Application.StartupPath, "1-节点库").Length == 1)
                {
                    dir = Application.StartupPath;
                    filltreeview();
                }
            }
            else
                filltreeview();
            }
        /// <summary>
        /// 拖入打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tongji_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tongji_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string dir0 = (e.Data.GetData(DataFormats.FileDrop, false) as string[])[0];
                if (Directory.Exists(dir0))
                {
                    DirectoryInfo dir1 = new DirectoryInfo(dir0);
                    int len = dir1.GetFiles("config.txt", SearchOption.AllDirectories).Length;
                    if(len == 0)
                    {
                        MessageBox.Show("该文件夹不包含任何节点文件！");
                    }
                    else if (len == 1 && File.Exists(Path.Combine(dir0, "3.png")))
                    {
                        frmhuitu fr = new frmhuitu(Path.Combine(dir0, "3.png"));
                        fr.Show();
                        this.Hide();
                    }
                    else
                    {
                        dir = dir0;
                        filltreeview();
                    }
                }
                else if (File.Exists(dir0))
                {
                    FileInfo fi = new FileInfo(dir0);
                    if (fi.Extension == ".png")
                    {
                        frmhuitu fr = new frmhuitu(dir0);
                        fr.Show();
                        this.Hide();
                    }
                }
            }
            catch
            {
                MessageBox.Show("请拖入节点库文件夹");
            }
        }
        /// <summary>
        /// 手动选择目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opendir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog jiedianku = new FolderBrowserDialog();
            if (jiedianku.ShowDialog() == DialogResult.OK)
            {
                dir = jiedianku.SelectedPath;
                filltreeview();
            }
        }
        #endregion

        #region//生成树状图
        /// <summary>
        /// 填充树状图
        /// </summary>
        private void filltreeview()
        {
            this.treeView1.Nodes.Clear();
            this.btnonoff.Enabled = false;
            this.btncheck.Enabled = false;
            this.btnexcel.Enabled = false;

            try
            {
                DirectoryInfo dirinfo = new DirectoryInfo(dir);
                TreeNode node=new TreeNode(dirinfo.Name + "(" + dirinfo.GetFiles("1.png", SearchOption.AllDirectories).Length + "个)");
                node.Tag = dirinfo;
                this.treeView1.Nodes.Add(node);
                expendtree(dirinfo, node);
                this.btnonoff.Enabled = true;
                this.btncheck.Enabled = true;
                this.btnexcel.Enabled = true;
                this.button2.Enabled = true;
                /*DirectoryInfo[] s = dirinfo.GetDirectories();
                ///通过遍历去添加所有父节点
               
                foreach (DirectoryInfo m in s)
                {

                    if (m.GetFiles("config.txt",SearchOption.AllDirectories).Length!=0)
                    {///父节点
                        if (m.GetFiles("config.txt", SearchOption.AllDirectories).Length == 1 && (m.GetDirectories()).Length == 0)
                            node = new TreeNode(m.Name);
                        else
                            node = new TreeNode(m.Name+ "("+m.GetFiles("config.txt", SearchOption.AllDirectories).Length+"个)");
                        ///给treeview添加节点
                        this.treeView1.Nodes[0].Nodes.Add(node);
                        ///调用方法递归出磁盘的所有文件，并将父节点和路径传入
                        expendtree(m, node);
                    }
                }*/
            }
            catch { MessageBox.Show("根节点创建失败"); }
            treeView1.Nodes[0].Expand();
        }
        /// <summary>
        /// 拓展树状图
        /// </summary>
        /// <param name="dir1"></param>
        /// <param name="tn"></param>
        private void expendtree(DirectoryInfo dir1, TreeNode tn)
            {
            
                try
                {
                    ///获取父节点目录的子目录
                    DirectoryInfo[] s1 = dir1.GetDirectories();
                    ///子节点
                    TreeNode subnode;
                    ///通过遍历给传进来的父节点添加子节点
                    foreach (DirectoryInfo j in s1)
                    {
                    if (j.GetFiles("1.png",SearchOption.AllDirectories).Length != 0)
                    {
                        if (j.GetFiles("1.png", SearchOption.TopDirectoryOnly).Length == 1)
                            subnode = new TreeNode(j.Name);
                            
                        
                        else
                            subnode = new TreeNode(j.Name + "(" + j.GetFiles("1.png", SearchOption.AllDirectories).Length + "个)");
                        subnode.Tag = j;
                        tn.Nodes.Add(subnode);
                        ///对文件夹不断递归， 得到所有文件
                        expendtree(j, subnode);
                    }
                    }
                }
                catch { MessageBox.Show("递归失败"); }
            
            }
        #endregion

        /// <summary>
        /// 双击打开对应文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            System.Diagnostics.Process.Start((e.Node.Tag as DirectoryInfo).FullName);
        }
        /// <summary>
        /// 折叠/收起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnonoff_Click(object sender, EventArgs e)
        {
            if (ison)
            {
                treeView1.CollapseAll();
                ison = false;
            }
            else if(dir!=string.Empty)
            {
                treeView1.ExpandAll();
                treeView1.Nodes[0].EnsureVisible();
                ison = true;

            }
            //ToolTip thum = new ToolTip();
        }
        /// <summary>
        /// 展示节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DirectoryInfo dirinfo1 = e.Node.Tag as DirectoryInfo;
            if ((dirinfo1.GetFiles("1.png", SearchOption.TopDirectoryOnly)).Length == 1)
            {
                #region//设置缩略图
                try
                {
                    picthum.ImageLocation = (dirinfo1.GetFiles("1.png"))[0].FullName;
                    picslt.ImageLocation = (dirinfo1.GetFiles("2.png"))[0].FullName;

                }
                catch
                {
                    MessageBox.Show("设置缩略图出错");
                }
                #endregion
                #region//读取节点名称
                try {
                    //StreamReader sr = new StreamReader((dirinfo1.GetFiles("config.txt"))[0].FullName, Encoding.Default);
                    //sr.ReadLine();
                    //label1.Text = (sr.ReadLine()).Replace("Name:", "节点名称：\r\n");
                    //sr.Close();
                    label1.Text = Path.GetFileNameWithoutExtension((dirinfo1.GetFiles("*.gjgsn"))[0].Name);
    }
                catch
                {
                    label1.Text="节点名称：\r\n读取节点名称出错";

                }
                #endregion
                #region//读取节点编号
                try
                {
                    label2.Text = "节点编号：" + e.Node.Text;
                }
                catch
                {
                    label2.Text = "节点编号：读取节点编号出错";

                }
                #endregion
                #region//读取作者

                try
                    {
                    StreamReader gjg=null;
                    if ((dirinfo1.GetFiles("*.gjgsn")).Length != 0)
                        gjg = new StreamReader((dirinfo1.GetFiles("*.gjgsn"))[0].FullName, Encoding.UTF8);
                    else if ((dirinfo1.GetFiles("*.json")).Length != 0)
                        gjg = new StreamReader((dirinfo1.GetFiles("*.json"))[0].FullName, Encoding.UTF8);
                    string gjgread = gjg.ReadToEnd();
                    JObject jo = JObject.Parse(gjgread);
                        label5.Text = "  作   者  ：" + jo["model"]["Attachs"]["Author"].ToString();
                    gjg.Close();
                }

                    catch { label5.Text = "  作   者  ：未署名"; }

                    //int findau1 = gjgread.IndexOf("Author");
                    //int findau2 = gjgread.IndexOf("author");
                    //int findau = Math.Max(findau1, findau2); 
                    //string author0 = String.Empty;
                    //string author = String.Empty;
                    //if (findau > 0)
                    //{
                    //    author0 = gjgread.Substring(findau + 10);
                    //    author = author0.Substring(0, author0.IndexOf('"'));
                    //}
                    //else
                    //    author = "未署名";
                    //if (author.Contains("\n"))
                    //    author = "未署名";
                    //label5.Text = "  作   者  ：" + author;
                    
              
                #endregion
                #region//创建时间
                try
                {
                    FileInfo[] files1 = dirinfo1.GetFiles();
                    //Int64[] ct= { 0,0,0,0,0,0};
                    DateTime deti = new DateTime(2000, 01, 01, 00, 00, 00);
                    DateTime[] ct = { deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti };
                    int i = 0;
                    foreach (FileInfo file1 in files1)
                    {
                        ct[i] = file1.CreationTime;
                        //ct[i]=Convert.ToInt64(file1.CreationTime.ToString("yyyyMMddhhmmss"));
                        i++;
                    }
                    if (mintime(ct, i) == deti)
                        label3.Text = "创建日期：读取创建时间出错";
                    else
                    label3.Text = "创建日期：" + mintime(ct, i);
                }
                catch
                {
                    label3.Text = "创建日期：读取创建时间出错";
                }
                #endregion
                #region//修改时间
                try
                {
                    FileInfo[] files1 = dirinfo1.GetFiles();
                    //Int64[] ct= { 0,0,0,0,0,0};
                    DateTime deti = new DateTime(2000, 01, 01, 00, 00, 00);
                    DateTime[] wt = { deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti };
                    int i1 = 0;
                    foreach (FileInfo file1 in files1)
                    {
                        wt[i1] = file1.LastWriteTime;
                        //ct[i]=Convert.ToInt64(file1.CreationTime.ToString("yyyyMMddhhmmss"));
                        i1++;
                    }
                    if (maxtime(wt, i1) == deti)
                        label4.Text = "修改日期：读取修改时间出错";
                    else
                        label4.Text = "修改日期：" + maxtime(wt,i1);
                }
                catch
                {
                    label4.Text = "修改日期：读取修改时间出错";
                }
                #endregion

            }
            if (e.Node.BackColor == Color.Green)
            {
                cbintest.Checked = true;
            }
            else
                cbintest.Checked = false;
        }

        private DateTime mintime(DateTime[] ct,int len)
        {
            try
            {
                DateTime a = ct[0], b = ct[1];int i = 1;
                for (i = 1; i < len; i++)
                {
                    b = ct[i];

                    if (a > b)
                        a = b;
                }

                return a;
            }
            catch
            {
                
                return ct[0];
            }
        }
        private DateTime maxtime(DateTime[] ct, int len)
        {
            try
            {
                DateTime a = ct[0], b = ct[1]; int i = 1;
                for (i = 1; i < len; i++)
                {
                    b = ct[i];

                    if (a < b)
                        a = b;
                }

                return a;
            }
            catch
            {

                return ct[0];
            }
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            makeexcel.makexls(dir);
            

        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            ///这部分代码没写完2017.3.22，已经获取三个名称，文件夹名称，等待校队
            DirectoryInfo dirinfo = new DirectoryInfo(dir);
            FileInfo[] configs = dirinfo.GetFiles("1.png", SearchOption.AllDirectories);
            string erro1 = string.Empty;
            foreach (FileInfo config in configs)
            {
                DirectoryInfo jd = config.Directory;
                //if (jd.GetFiles().Length > 6)
                //    erro1 = "\r\n" + jd.Name + erro1;

                string filename=jd.GetFiles("*.gjgsn")[0].Name;
                string gjgname = string.Empty;
                try
                {
                    gjgname = JObject.Parse((new StreamReader(jd.GetFiles("*.gjgsn")[0].FullName)).ReadToEnd())["model"]["Attachs"]["Author"].ToString();
                    //if(filename!=gjgname)
                    //    erro1+=
                }
                catch { }

                string gvnname = string.Empty;
                if (jd.GetFiles("*.GVN").Length>0)
                gvnname = jd.GetFiles("*.GVN")[0].Name;


            }
            if (erro1 != string.Empty)
                MessageBox.Show("下列节点文件中含有多余文件:" + erro1);




        }

        private void btntool_Click(object sender, EventArgs e)
        {
            frmhuitu frm= new frmhuitu();
            frm.Show();

        }

        private void btncomp_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(dir);
            FileInfo[] configs = dirinfo.GetFiles("1.png", SearchOption.AllDirectories);
            string frdir = string.Empty;
            foreach (FileInfo config in configs)
            {
                DirectoryInfo jd = config.Directory;

                if (jd.Name.Length>6&&jd.Name[3] != '0' && jd.Parent.Name[3] == '0' && jd.Parent.GetFiles("1.png", SearchOption.AllDirectories).Length == 1)
                    frdir= jd.Parent.Name+"\n" + frdir;
                else
                    frdir =jd.Name+ "\n" + frdir;

            }
            compare cp = new compare(frdir);
            cp.Show();
        }

        private void tongji_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果是要整理测试节点
            if (movefortest)
            {
                picslt.ImageLocation = null;
                picthum.ImageLocation = null;

                getallnodes(treeView1.Nodes[0]);

                string dirintest = Path.Combine(dir, "进入测试");
                string dirintestcsh = Path.Combine(dirintest, "参数化构件");
                string dirintestjdxb = Path.Combine(dirintest, "节点细部");
                if (!Directory.Exists(dirintest))
                    Directory.CreateDirectory(dirintest);

                foreach (TreeNode t in treelist)
                {
                    if (t.BackColor == Color.Green)
                    {

                        DirectoryInfo d = t.Tag as DirectoryInfo;
                        if (d.Name[0] == '3')
                        {
                            if (!Directory.Exists(dirintestcsh))
                                Directory.CreateDirectory(dirintestcsh);
                            d.MoveTo(Path.Combine(dirintestcsh,d.Name));
                        }
                        else
                        {
                            if (!Directory.Exists(dirintestjdxb))
                                Directory.CreateDirectory(dirintestjdxb);
                            d.MoveTo(Path.Combine(dirintestjdxb, d.Name));
                        }
                        
                        
                    }
                }
            }
           


            if (back)
                zct.Show();
        }

        private void getallnodes(TreeNode t)
        {
            treelist.Add(t);
            if(t.Nodes.Count!=0)
            {
                foreach(TreeNode tc in t.Nodes)
                {
                    getallnodes(tc);
                }
            }

        }
        

        private void picthum_MouseEnter(object sender, EventArgs e)
        {
            if (picslt.ImageLocation != null)
            picslt.Visible = true;
        }

        private void picthum_MouseLeave(object sender, EventArgs e)
        {
            picslt.Visible = false;
        }

        /// <summary>
        /// 截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Image jtl=new Bitmap(this.Width,this.Height);
            Graphics jtlg = Graphics.FromImage(jtl);
            jtlg.CopyFromScreen(this.Left, this.Top, 0, 0, this.Size);
            picslt.Visible = false;
            Clipboard.SetImage(jtl);
        }

        private void picthum_Click(object sender, EventArgs e)
        {
            Image jtl = new Bitmap(460, 160);
            Graphics jtlg = Graphics.FromImage(jtl);  
                  
            jtlg.CopyFromScreen(PointToScreen(picthum.Location).X, PointToScreen(picthum.Location).Y, 0, 0, new Size(460,160));            
            Clipboard.SetImage(jtl);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (picslt.ImageLocation != null)
                picslt.Visible = true;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            picslt.Visible = false;
        }

        ///修改编号
        private void label2_Click(object sender, EventArgs e)//单击节点编号
        {
            DirectoryInfo j = treeView1.SelectedNode.Tag as DirectoryInfo;
            inputbianhao f1 = new inputbianhao(this, label2.Text.Replace("节点编号：", ""),j);
            
            f1.Left =this.Left+ label2.Left+15;
            f1.Top = this.Top + label2.Top+10;
            f1.ShowDialog();
           // 改界面上显示的编号
            label2.Text = "节点编号：" + xgbh;
            treeView1.SelectedNode.Text = xgbh;
            #region//获取新旧名字
            //DirectoryInfo j = treeView1.SelectedNode.Tag as DirectoryInfo;
            FileSystemInfo[] gjgsn = j.GetFiles("*.gjgsn", SearchOption.TopDirectoryOnly);

            string name = string.Empty;
            string newname = string.Empty;
            string dm = string.Empty;
            string or = string.Empty;

            if (gjgsn.Length == 1)
            {//获取旧名字
                name = gjgsn[0].Name.Replace(".gjgsn", "");
            }
            else { MessageBox.Show("gjgsn文件数量不为1");goto end1; }

            int i = name.IndexOf('.');
            //获取末尾编号
            if (xgbh[xgbh.Length - 2] == '0')
                or = xgbh[xgbh.Length - 1].ToString();
            else
                or = xgbh[xgbh.Length - 2] + xgbh[xgbh.Length - 1].ToString();
            //新名字
            newname = or + name.Substring(i);
            #endregion
            //改界面上显示的名字
            label1.Text = label1.Text.Replace(name,newname);
            //改gjgsn文件里的名字,及文件名
            StreamReader sr = new StreamReader(gjgsn[0].FullName);
            dm=sr.ReadToEnd().Replace(name,newname);
            sr.Close();
            gjgsn[0].Delete();
            StreamWriter wr= new StreamWriter(gjgsn[0].FullName.Replace(name, newname), false,Encoding.UTF8);
            wr.Write(dm);
            wr.Close();
            
            #region//改config文件
            FileSystemInfo[] config = j.GetFiles("config.txt", SearchOption.TopDirectoryOnly);
            if (config.Length==1)
            {
                //改config文件
                StreamReader sr1 = new StreamReader(config[0].FullName);
                dm = sr1.ReadToEnd().Replace(name, newname);
                sr1.Close();
                StreamWriter wr1 = new StreamWriter(config[0].FullName, false, Encoding.UTF8);
                wr1.Write(dm);
                wr1.Close();
            }
            #endregion

            //改gvd文件名
            FileSystemInfo[] gvd = j.GetFiles("*.GVD", SearchOption.TopDirectoryOnly);
            if (gvd.Length >0)
            {
                //改gvd文件
                FileInfo fi = new FileInfo(gvd[0].FullName);
                fi.MoveTo(gvd[0].FullName.Replace(name,newname));
            }
            //改文件夹名
            try { j.MoveTo(Path.Combine(j.Parent.FullName, xgbh)); }
            catch { }
            



        end1:;
                }

        //输出CSV文件
        private void btncsv_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(dir);
            FileInfo[] configs = dirinfo.GetFiles("1.png", SearchOption.AllDirectories);
            string excel = string.Empty;
            foreach (FileInfo config in configs)
            {
                DirectoryInfo jd = config.Directory;
                string line = string.Empty;
                #region//读取节点编号
                try
                {
                    if (jd.Name[3] != '0' && jd.Parent.Name[3] == '0' && jd.Parent.GetFiles("1.png", SearchOption.AllDirectories).Length == 1)
                        line += jd.Parent.Name + ",";
                    else
                        line += jd.Name + ",";
                }
                catch
                {
                    line += "读取节点编号出错,";

                }
                #endregion
                #region//读取节点名称

                try
                {
                    //StreamReader sr = new StreamReader((jd.GetFiles("config.txt"))[0].FullName, Encoding.Default);
                    //sr.ReadLine();
                    //line+= (sr.ReadLine()).Replace("Name:", "")+",";
                    line += Path.GetFileNameWithoutExtension((jd.GetFiles("*.gjgsn"))[0].Name) + ",";
                }
                catch
                {
                    line += "读取节点名称出错,";

                }
                #endregion
                #region//读取作者

                    StreamReader gjg = new StreamReader((jd.GetFiles("*.gjgsn"))[0].FullName, Encoding.UTF8);
                    string gjgread = gjg.ReadToEnd();

                try
                {
                    JObject jo = JObject.Parse(gjgread);
                    line += "  作   者  ：" + jo["model"]["Attachs"]["Author"].ToString() + ",";
                }
                catch
                {
                    line += "未署名,";
                }

                gjg.Close();
                    //int findau1 = gjgread.IndexOf("Author");
                    //int findau2 = gjgread.IndexOf("author");
                    //int findau = Math.Max(findau1, findau2);
                    //string author0 = String.Empty;
                    //string author = String.Empty;
                    //if (findau > 0)
                    //{
                    //    author0 = gjgread.Substring(findau + 11);
                    //    author = author0.Substring(0, author0.IndexOf('"'));
                    //}
                    //else
                    //    author = "未署名";
                    //if (author.Contains("\n"))
                    //    author = "未署名";
                    //line += author + ",";


                #endregion
                #region//创建时间
                /*
                try
                {
                    FileInfo[] files1 = jd.GetFiles();
                    //Int64[] ct= { 0,0,0,0,0,0};
                    DateTime deti = new DateTime(2000, 01, 01, 00, 00, 00);
                    DateTime[] ct = { deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti };
                    int i = 0;
                    foreach (FileInfo file1 in files1)
                    {
                        ct[i] = file1.CreationTime;
                        //ct[i]=Convert.ToInt64(file1.CreationTime.ToString("yyyyMMddhhmmss"));
                        i++;
                    }
                    if (mintime(ct, i) == deti)
                        label3.Text = "创建日期：读取创建时间出错,";
                    else
                        line+= mintime(ct,i)+",";
                }
                catch
                {
                    line+= "读取创建时间出错,";
                }
                #endregion
                #region//修改时间
                try
                {
                    FileInfo[] files1 = jd.GetFiles();
                    //Int64[] ct= { 0,0,0,0,0,0};
                    DateTime deti = new DateTime(2000, 01, 01, 00, 00, 00);
                    DateTime[] wt = { deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti, deti };
                    int i = 0;
                    foreach (FileInfo file1 in files1)
                    {
                        wt[i] = file1.LastWriteTime;
                        //ct[i]=Convert.ToInt64(file1.CreationTime.ToString("yyyyMMddhhmmss"));
                        i++;
                    }
                    if (maxtime(wt,i) == deti)
                        line+= "读取修改时间出错,";
                    else
                        line+= maxtime(wt,i)+",";
                }
                catch
                {
                    line += "读取修改时间出错,";
                }*/
                #endregion
                #region//类型
                if (line[0] == '1')
                    line += "节点,";
                else if (line[0] == '2')
                    line += "细部,";
                else if (line[0] == '3')
                    line += "参数化构件,";
                else
                    line += "获取类型出错,";
                #endregion
                excel = line + "\r\n" + excel;

                //MessageBox.Show("");
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(dir, "自动生成节点统计表格.csv"), false, Encoding.Default))
                {
                    sw.Write(excel);
                }
                System.Diagnostics.Process.Start(Path.Combine(dir, "自动生成节点统计表格.csv"));
            }
            catch
            {
                MessageBox.Show("写入文件失败，可能正在被访问！");
            }
        }

        private void cbintest_CheckedChanged(object sender, EventArgs e)
        {
            if (cbintest.Checked)
            {
                movefortest = true;
                treeView1.SelectedNode.BackColor = Color.Green;
            }
            else
            {
                treeView1.SelectedNode.BackColor = Color.White;
            }
        }
    }



}




