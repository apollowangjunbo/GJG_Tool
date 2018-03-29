using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Microsoft.Office.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication6
{
    class makeexcel
    {

        

        public static void makexls(string dir)
        {
            Dictionary<string, string> gjgtype = new Dictionary<string, string>();
            gjgtype.Add("101", "柱梁节点（框架）");
            gjgtype.Add("102", "柱梁节点（门钢）");
            gjgtype.Add("103", "主次梁节点");
            //gjgtype.Add("104", "系杆与柱梁节点");
            gjgtype.Add("105", "托柱、托梁节点");
            gjgtype.Add("106", "支撑与支撑节点");
            gjgtype.Add("107", "H型钢桁架节点");
            gjgtype.Add("108", "柱与支撑节点");
            gjgtype.Add("109", "梁与支撑节点");
            gjgtype.Add("110", "柱与柱对接节点");
            gjgtype.Add("111", "系杆节点");
            gjgtype.Add("112", "檩条节点");
            gjgtype.Add("120", "其他节点");

            gjgtype.Add("201", "柱脚");
            gjgtype.Add("202", "柱帽、柱顶");
            gjgtype.Add("203", "牛腿、肩梁细部");
            gjgtype.Add("204", "吊车梁车挡");
            gjgtype.Add("205", "加劲肋、隔板");
            gjgtype.Add("206", "柱身环板");
            gjgtype.Add("207", "拼接段");
            gjgtype.Add("208", "过渡段");
            gjgtype.Add("209", "人孔、管道孔");
            gjgtype.Add("210", "补强板");
            gjgtype.Add("211", "加工工艺版");
            gjgtype.Add("212", "吊装工艺板");
            gjgtype.Add("213", "预埋件节点");
            gjgtype.Add("299", "其他细部");

            gjgtype.Add("301", "格构柱");
            gjgtype.Add("302", "转换桁架");
            gjgtype.Add("303", "巨型柱");
            gjgtype.Add("304", "钢楼梯");
            gjgtype.Add("305", "钢爬梯");
            gjgtype.Add("306", "电梯井");
            gjgtype.Add("307", "型钢桁架");
            gjgtype.Add("308", "组合吊车梁");
            gjgtype.Add("309", "钢板剪力墙");
            gjgtype.Add("310", "天窗架");
            gjgtype.Add("311", "异形构件");
            gjgtype.Add("312", "其他");
            gjgtype.Add("313", "柱间支撑");
            gjgtype.Add("314", "组合门架");
            gjgtype.Add("315", "女儿墙柱");
            gjgtype.Add("316", "抗风柱");







            int wrong = 0;
            Excel.Application xls = new Excel.Application();
            xls.Caption = "节点、细部、参数化构件统计表";
            Excel.Workbooks wbs = xls.Workbooks;
            Excel.Workbook wb = wbs.Add();
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ws.Name = "节点";
            xls.Visible = true;

            DirectoryInfo dirinfo = new DirectoryInfo(dir);
            FileInfo[] png1 = dirinfo.GetFiles("1.png", SearchOption.AllDirectories);
            ws.Cells[1, 1] = "编号";
            ws.Cells[1, 2] = "名称";
            ws.Cells[1, 3] = "配图";
            ws.Cells[1, 4] = "分类";
            ws.Cells[1, 5] = "状态";
            ws.Cells[1, 6] = "备注";
            int rownum = 2;
            Excel.Range r2 = ws.Cells[1, 3];
            Excel.Range r3 = ws.Cells[1, 2];
            r3.EntireColumn.ColumnWidth =50;
            r2.EntireColumn.ColumnWidth = 145*0.035267 / 0.214975;
            Excel.Range r4 = ws.Cells[1, 4];
            r4.EntireColumn.ColumnWidth =30;
            foreach (FileInfo png in png1)
            {
                //编号：文件夹名称
                //名称：gjgsn名称
                //作者：已添加
                //分类，由编号获取，允许未定义
                //入库，未测试手动添加
                try
                {
                    DirectoryInfo jd = png.Directory;
                    ws.Cells[rownum, 1] = jd.Name;
                    ws.Cells[rownum, 2] = Path.GetFileNameWithoutExtension((jd.GetFiles("*.gjgsn"))[0].Name);
                    Excel.Range ran = ws.Cells[rownum, 3];
                    Excel.Shape tu = ws.Shapes.AddPicture((jd.GetFiles("1.png"))[0].FullName, MsoTriState.msoTrue, MsoTriState.msoTrue, (float)(ran.Left+3), (float)(ran.Top+2), 140, 160);
                    ran.EntireRow.RowHeight = 165;
                    try
                    { ws.Cells[rownum, 4] = gjgtype[jd.Name.Substring(0,3)]; }
                    catch
                    { ws.Cells[rownum, 4] = "未识别的分类"; }

                    StreamReader gjg = new StreamReader((jd.GetFiles("*.gjgsn"))[0].FullName,System.Text.Encoding.UTF8);
                    string gjgread = gjg.ReadToEnd();

                    try
                    {
                        JObject jo = JObject.Parse(gjgread);
                        ws.Cells[rownum, 5] = jo["model"]["Attachs"]["Author"].ToString() + ",";
                    }
                    catch
                    {
                        ws.Cells[rownum, 5] = "未署名,";
                    }

                    gjg.Close();


                    //ws.Cells[rownum, 6] ="已入库";



                }
                catch
                {
                    wrong++;
                }
                rownum++;

            }

            if (wrong != 0)
                MessageBox.Show(wrong.ToString());
        } 
    }
}
