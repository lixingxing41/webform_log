using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace HTTPBASE
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string path = Server.MapPath("~/Log/");
                DataTable LogFiles = new DataTable();
                LogFiles.Columns.Add("Name");
                string[] files = Directory.GetFiles(path);
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo fInfo = new FileInfo(files[i]);
                    DataRow row = LogFiles.NewRow();
                    row["Name"] = fInfo.Name;
                    LogFiles.Rows.Add(row);
                }

                GridView1.DataSource = LogFiles;
                GridView1.DataBind();
            }
        }
        ///按下"詳細資料"按鈕
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Button btn = (Button)e.CommandSource;
            GridViewRow rowNum = (GridViewRow)btn.NamingContainer;
            string filename = rowNum.Cells[0].Text;
            string path = Server.MapPath("~/Log/") + filename;
            string content = string.Empty;

            if (File.Exists(path)) //取出Log檔資料
            {
                string[] allcontent = File.ReadAllLines(path);
                foreach (string s in allcontent)
                {
                    content += s;
                    content += "<br/>";
                }
                result.Text = content;
            }
        }
        ///返回
        protected void goback(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");

        }
    }
}