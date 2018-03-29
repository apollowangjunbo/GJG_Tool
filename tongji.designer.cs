namespace WindowsFormsApplication6
{
    partial class tongji
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnopen = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnonoff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btncheck = new System.Windows.Forms.Button();
            this.btnexcel = new System.Windows.Forms.Button();
            this.btncomp = new System.Windows.Forms.Button();
            this.btntool = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.picslt = new System.Windows.Forms.PictureBox();
            this.picthum = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbintest = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picslt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picthum)).BeginInit();
            this.SuspendLayout();
            // 
            // btnopen
            // 
            this.btnopen.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnopen.Location = new System.Drawing.Point(454, 408);
            this.btnopen.Name = "btnopen";
            this.btnopen.Size = new System.Drawing.Size(125, 34);
            this.btnopen.TabIndex = 1;
            this.btnopen.Text = "打开节点库";
            this.btnopen.UseVisualStyleBackColor = true;
            this.btnopen.Click += new System.EventHandler(this.opendir_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(393, 503);
            this.treeView1.TabIndex = 2;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // btnonoff
            // 
            this.btnonoff.Enabled = false;
            this.btnonoff.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnonoff.Location = new System.Drawing.Point(454, 457);
            this.btnonoff.Name = "btnonoff";
            this.btnonoff.Size = new System.Drawing.Size(125, 34);
            this.btnonoff.TabIndex = 3;
            this.btnonoff.Text = "展开/收起";
            this.btnonoff.UseVisualStyleBackColor = true;
            this.btnonoff.Click += new System.EventHandler(this.btnonoff_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(560, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 88);
            this.label1.TabIndex = 5;
            this.label1.Text = "节点名称：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(560, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 34);
            this.label2.TabIndex = 6;
            this.label2.Text = "节点编号：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(418, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(444, 34);
            this.label3.TabIndex = 7;
            this.label3.Text = "创建日期：";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(418, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(444, 34);
            this.label4.TabIndex = 8;
            this.label4.Text = "最后修改：";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(560, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(302, 34);
            this.label5.TabIndex = 9;
            this.label5.Text = "  作   者  ：";
            // 
            // btncheck
            // 
            this.btncheck.Enabled = false;
            this.btncheck.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btncheck.Location = new System.Drawing.Point(585, 408);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(125, 34);
            this.btncheck.TabIndex = 10;
            this.btncheck.Text = "检测节点库";
            this.btncheck.UseVisualStyleBackColor = true;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // btnexcel
            // 
            this.btnexcel.Enabled = false;
            this.btnexcel.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnexcel.Location = new System.Drawing.Point(585, 457);
            this.btnexcel.Name = "btnexcel";
            this.btnexcel.Size = new System.Drawing.Size(125, 34);
            this.btnexcel.TabIndex = 11;
            this.btnexcel.Text = "导出Excel";
            this.btnexcel.UseVisualStyleBackColor = true;
            this.btnexcel.Click += new System.EventHandler(this.btnexcel_Click);
            // 
            // btncomp
            // 
            this.btncomp.Enabled = false;
            this.btncomp.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btncomp.Location = new System.Drawing.Point(716, 360);
            this.btncomp.Name = "btncomp";
            this.btncomp.Size = new System.Drawing.Size(125, 34);
            this.btncomp.TabIndex = 12;
            this.btncomp.Text = "对比节点表";
            this.btncomp.UseVisualStyleBackColor = true;
            this.btncomp.Visible = false;
            this.btncomp.Click += new System.EventHandler(this.btncomp_Click);
            // 
            // btntool
            // 
            this.btntool.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btntool.Location = new System.Drawing.Point(716, 457);
            this.btntool.Name = "btntool";
            this.btntool.Size = new System.Drawing.Size(125, 34);
            this.btntool.TabIndex = 13;
            this.btntool.Text = "节点制作辅助";
            this.btntool.UseVisualStyleBackColor = true;
            this.btntool.Click += new System.EventHandler(this.btntool_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(716, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 34);
            this.button1.TabIndex = 15;
            this.button1.Text = "截图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // picslt
            // 
            this.picslt.Location = new System.Drawing.Point(0, 0);
            this.picslt.Name = "picslt";
            this.picslt.Size = new System.Drawing.Size(390, 500);
            this.picslt.TabIndex = 14;
            this.picslt.TabStop = false;
            this.picslt.Visible = false;
            // 
            // picthum
            // 
            this.picthum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picthum.Image = global::WindowsFormsApplication6.Properties.Resources.节点缩略图;
            this.picthum.Location = new System.Drawing.Point(414, 12);
            this.picthum.Name = "picthum";
            this.picthum.Size = new System.Drawing.Size(140, 160);
            this.picthum.TabIndex = 4;
            this.picthum.TabStop = false;
            this.picthum.Click += new System.EventHandler(this.picthum_Click);
            this.picthum.MouseEnter += new System.EventHandler(this.picthum_MouseEnter);
            this.picthum.MouseLeave += new System.EventHandler(this.picthum_MouseLeave);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(585, 360);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 34);
            this.button2.TabIndex = 16;
            this.button2.Text = "导出csv";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btncsv_Click);
            // 
            // cbintest
            // 
            this.cbintest.AutoSize = true;
            this.cbintest.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbintest.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.cbintest.Location = new System.Drawing.Point(417, 272);
            this.cbintest.Name = "cbintest";
            this.cbintest.Size = new System.Drawing.Size(115, 32);
            this.cbintest.TabIndex = 17;
            this.cbintest.Text = "进入测试";
            this.cbintest.UseVisualStyleBackColor = true;
            this.cbintest.CheckedChanged += new System.EventHandler(this.cbintest_CheckedChanged);
            // 
            // tongji
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 503);
            this.Controls.Add(this.cbintest);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picslt);
            this.Controls.Add(this.btntool);
            this.Controls.Add(this.btncomp);
            this.Controls.Add(this.btnexcel);
            this.Controls.Add(this.btncheck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picthum);
            this.Controls.Add(this.btnonoff);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btnopen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "tongji";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节点库管理工具 V2.0.17.12.02";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tongji_FormClosing);
            this.Load += new System.EventHandler(this.tongji_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.tongji_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.tongji_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.picslt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picthum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnopen;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btnonoff;
        private System.Windows.Forms.PictureBox picthum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Button btnexcel;
        private System.Windows.Forms.Button btncomp;
        private System.Windows.Forms.Button btntool;
        private System.Windows.Forms.PictureBox picslt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbintest;
    }
}

