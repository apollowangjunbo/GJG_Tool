using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Security.Principal;

namespace WindowsFormsApplication6
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] dir3)
        {
            LoadResourceDll.RegistDLL();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region//文件夹右键菜单
            try
            {
                    //创建项：shell 
                    RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"directory\shell", true);
                    if (shellKey == null)
                    {
                        shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                    }

                    //创建项：右键显示的菜单名称
                    RegistryKey rightCommondKey = shellKey.CreateSubKey("gjgjd");
                    rightCommondKey.SetValue("", "管理节点");
                    rightCommondKey.SetValue("icon", Application.ExecutablePath);
                    RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

                    //创建默认值：关联的程序
                    associatedProgramKey.SetValue("", Application.ExecutablePath + " %1");


                    //刷新到磁盘并释放资源
                    associatedProgramKey.Close();
                    rightCommondKey.Close();
                    shellKey.Close();
                    //MessageBox.Show("添加右键菜单成功");
                }
                catch
                {
                    
                }
            #endregion
            #region//文件右键菜单
            try
            {
                //创建项：shell 
                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                if (shellKey == null)
                {
                    shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                }

                //创建项：右键显示的菜单名称
                RegistryKey rightCommondKey = shellKey.CreateSubKey("gjgjd");
                rightCommondKey.SetValue("", "节点辅助");
                rightCommondKey.SetValue("icon", Application.ExecutablePath);
                RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

                //创建默认值：关联的程序
                associatedProgramKey.SetValue("", Application.ExecutablePath + " %1");


                //刷新到磁盘并释放资源
                associatedProgramKey.Close();
                rightCommondKey.Close();
                shellKey.Close();
                //MessageBox.Show("添加右键菜单成功");
            }
            catch
            {

            }
            #endregion






            if (dir3.Length == 0)
                {
                    if ((Path.GetFileName(Application.StartupPath)).Contains("节点库") || Directory.GetDirectories(Application.StartupPath, "1-节点库").Length == 1)
                        Application.Run(new tongji());
                    else
                        Application.Run(new frmhuitu());
                }
                else if (Directory.Exists(dir3[0]))
                {
                    DirectoryInfo dir1 = new DirectoryInfo(dir3[0]);
                    if (dir1.GetFiles("1.png", SearchOption.AllDirectories).Length < 2)
                    {
                        string png3 = Path.Combine(dir3[0], "1.png");
                        if (File.Exists(png3))
                        {
                            Application.Run(new frmhuitu(png3));
                        }
                        else
                        {
                            dir3[0] = string.Empty;
                            MessageBox.Show("该文件夹不包含节点文件！");
                        }

                    }
                    else
                    {
                        Application.Run(new tongji(dir3[0]));
                    }

                }
                else if (File.Exists(dir3[0]))
                {
                    Application.Run(new frmhuitu(dir3[0]));
                }

            
            






        }


    }
}
