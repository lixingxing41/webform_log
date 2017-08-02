using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTPBASE
{
    public partial class Default : System.Web.UI.Page
    {
        int num, sid; //資料筆數, 目前位置
        int flag = 1; //判別搜尋種類
        int[] ID = new int[5];
        public void Page_Load(object sender, EventArgs e)
        {
            GetPageCount();  // 取得資料筆數
            GridView1.VirtualItemCount = num;

            //取得目前位置
            if (Session["sid"] != null)
            {
                sid = Convert.ToInt32(Session["sid"]);
                if (sid == 0)
                { //位於第一頁
                    BtnFirstPage.Enabled = false;
                    BtnPPage.Enabled = false;
                }
                if (sid + 5 >= num)
                { //位於最後頁
                    BtnNPage.Enabled = false;
                    BtnLastPage.Enabled = false;
                }
            }
            else //初始化
            {
                sid = 0;
                Session["sid"] = sid;
                BtnFirstPage.Enabled = false;
                BtnPPage.Enabled = false;
            }
            //搜尋欄位
            if (Session["query_name"] != null) TxtQueryName.Text = Session["query_name"].ToString();
            if (Session["query_title"] != null) TxtQueryTitle.Text = Session["query_title"].ToString();

            GridView1.DataSource = GetPageData(); //連結DataTable

            if (!IsPostBack)
            {
                string path = Server.MapPath("~/Log/");
                //若不存在Log資料夾，則建立資料夾
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                GridView1.DataBind(); //只能寫於!IsPostBack內，否則會產生驗證問題
            }
            else
            {
                //取得EmployeeID
                int i = 0;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    if (Session["query_name"] != null || Session["query_title"] != null)
                    {
                        switch (Convert.ToInt32(Session["flag"]))
                        {
                            case 0: //查詢姓名
                                queryString = " select * from Employees where EmployeeName like @query_name " +
                                              "order by EmployeeID offset @sid rows fetch next 5 rows only";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@sid", sid);
                                command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                                Response.Write(Session["query_name"]);
                                //  TxtQueryName.Text = Session["query_name"].ToString();
                                break;
                            case 1: //查詢姓名+職稱
                                queryString = " select * from Employees where EmployeeName like @query_name and Title like @query_title " +
                                              "order by EmployeeID offset @sid rows fetch next 5 rows only";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@sid", sid);
                                command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                                command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                                //   TxtQueryName.Text = Session["query_name"].ToString();
                                //  TxtQueryTitle.Text = Session["query_title"].ToString();
                                break;
                            case 2: //查詢職稱
                                queryString = " select * from Employees where Title like @query_title " +
                                              "order by EmployeeID offset @sid rows fetch next 5 rows only";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@sid", sid);
                                command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                                //  TxtQueryTitle.Text = Session["query_title"].ToString();
                                break;
                        }
                    }
                    else //全部資料
                    {
                        queryString = " select * from Employees order by EmployeeID offset @sid rows fetch next 5 rows only";
                        command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@sid", sid);
                    }
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ID[i] = Convert.ToInt32(reader["EmployeeID"]);
                            i++;
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                //取得觸發事件按鈕
                string get_id = Request["__EVENTTARGET"];
                switch (get_id)
                {
                    ///新增
                    case "BtnAdd":
                        Session.RemoveAll();
                        Session["sid"] = sid;
                        Response.Redirect("Edit.aspx");
                        break;
                    ///查詢
                    case "BtnQuery":
                        if (!Request.Form["TxtQueryName"].Equals(""))
                        {
                            Session["query_name"] = Request.Form["TxtQueryName"];
                            --flag;
                        }
                        else Session.Remove("query_name");
                        if (!Request.Form["TxtQueryTitle"].Equals(""))
                        {
                            Session["query_title"] = Request.Form["TxtQueryTitle"];
                            ++flag;
                        }
                        else Session.Remove("query_title");
                        Session["flag"] = flag;
                        Session["sid"] = "0"; //初始化 --> 否則查詢後不會從第一頁開始顯示
                        Response.Redirect("Default.aspx");
                        break;
                    ///編輯
                    case "GridView1$ctl02$edit":
                        Session["eid"] = ID[0];
                        Response.Redirect("Edit.aspx");
                        break;
                    case "GridView1$ctl03$edit":
                        Session["eid"] = ID[1];
                        Response.Redirect("Edit.aspx");
                        break;
                    case "GridView1$ctl04$edit":
                        Session["eid"] = ID[2];
                        Response.Redirect("Edit.aspx");
                        break;
                    case "GridView1$ctl05$edit":
                        Session["eid"] = ID[3];
                        Response.Redirect("Edit.aspx");
                        break;
                    case "GridView1$ctl06$edit":
                        Session["eid"] = ID[4];
                        Response.Redirect("Edit.aspx");
                        break;
                    ///刪除
                    case "GridView1$ctl02$delete":
                        Session["did"] = ID[0];
                        Response.Redirect("Default.aspx");
                        break;
                    case "GridView1$ctl03$delete":
                        Session["did"] = ID[1];
                        Response.Redirect("Default.aspx");
                        break;
                    case "GridView1$ctl04$delete":
                        Session["did"] = ID[2];
                        Response.Redirect("Default.aspx");
                        break;
                    case "GridView1$ctl05$delete":
                        Session["did"] = ID[3];
                        Response.Redirect("Default.aspx");
                        break;
                    case "GridView1$ctl06$delete":
                        Session["did"] = ID[4];
                        Response.Redirect("Default.aspx");
                        break;
                }
            }

        }
        ///第一頁
        protected void toFirstPage(object sender, System.EventArgs e)
        {
            Session["sid"] = "0";
            Response.Redirect("Default.aspx");
        }
        ///上一頁
        protected void PreviousPage(object sender, System.EventArgs e)
        {
            if (sid != 0)
                Session["sid"] = (sid - 5);
            Response.Redirect("Default.aspx");
        }
        ///下一頁
        protected void NextPage(object sender, System.EventArgs e)
        {
            if (sid + 5 < num)
            {
                Session["sid"] = (sid + 5);
            }
            Response.Redirect("Default.aspx");
        }
        ///最後頁
        protected void toLastPage(object sender, System.EventArgs e)
        {
            if (num % 5 == 0)
                Session["sid"] = (num - 5);
            else
                Session["sid"] = (num - num % 5);
            Response.Redirect("Default.aspx");
        }

        ///建立DataTable
        protected DataTable GetPageData()
        {
            DataTable DT = new DataTable();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "";
                SqlCommand command = new SqlCommand(queryString, connection);
                if (Session["query_name"] != null || Session["query_title"] != null)
                {
                    switch (Convert.ToInt32(Session["flag"]))
                    {
                        case 0: //查詢姓名
                            queryString = " select EmployeeName,Title,BirthDate,Address,Salary from Employees where EmployeeName like @query_name " +
                                          "order by EmployeeID offset @sid rows fetch next 5 rows only";
                            command = new SqlCommand(queryString, connection);
                            command.Parameters.AddWithValue("@sid", sid);
                            command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                            Response.Write(Session["query_name"]);
                            break;
                        case 1: //查詢姓名+職稱
                            queryString = " select EmployeeName,Title,BirthDate,Address,Salary from Employees where EmployeeName like @query_name and Title like @query_title " +
                                          "order by EmployeeID offset @sid rows fetch next 5 rows only";
                            command = new SqlCommand(queryString, connection);
                            command.Parameters.AddWithValue("@sid", sid);
                            command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                            command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                            break;
                        case 2: //查詢職稱
                            queryString = " select EmployeeName,Title,BirthDate,Address,Salary from Employees where Title like @query_title " +
                                          "order by EmployeeID offset @sid rows fetch next 5 rows only";
                            command = new SqlCommand(queryString, connection);
                            command.Parameters.AddWithValue("@sid", sid);
                            command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                            break;
                    }
                }
                else //全部資料
                {
                    queryString = " select EmployeeName,Title,BirthDate,Address,Salary from Employees order by EmployeeID offset @sid rows fetch next 5 rows only";
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@sid", sid);
                }
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(DT);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return DT;
        }

        protected void GetPageCount()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString();
            ///刪除
            if (Session["did"] != null)
            {
                string logpath = Server.MapPath("~/Log/") + "DEL_LOG_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string txt = Convert.ToInt32(Session["did"]) + " 刪除時間 " + DateTime.Now.ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = " delete from  Employees where EmployeeID=@did ";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@did", Convert.ToInt32(Session["did"]));
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        //若無Log檔，建立Log檔
                        if (!File.Exists(logpath))
                        {
                            File.Create(logpath).Close();
                        }
                        //將文字寫入Log檔
                        using (StreamWriter w = new StreamWriter(logpath, true))
                        {
                            w.WriteLine(txt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Session.Remove("did");
                Response.Redirect("Default.aspx");
            }
            ///取得資料筆數
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    if (Session["query_name"] != null || Session["query_title"] != null)
                    {
                        switch (Convert.ToInt32(Session["flag"]))
                        {
                            case 0: //查詢姓名
                                queryString = " select count (*) from Employees where EmployeeName like @query_name ";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                                break;
                            case 1: //查詢姓名+職稱
                                queryString = " select count (*) from Employees where EmployeeName like @query_name and Title like @query_title ";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@query_name", "%" + Session["query_name"].ToString() + "%");
                                command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                                break;
                            case 2: //查詢職稱
                                queryString = " select count (*) from Employees where Title like @query_title ";
                                command = new SqlCommand(queryString, connection);
                                command.Parameters.AddWithValue("@query_title", "%" + Session["query_title"].ToString() + "%");
                                break;
                        }
                    }
                    else //全部資料
                    {
                        queryString = " select count (*) from Employees ";
                        command = new SqlCommand(queryString, connection);
                    }
                    try
                    {
                        connection.Open();
                        num = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }
        ///到Log頁面
        protected void toLog(object sender, EventArgs e)
        {
            Response.Redirect("Log.aspx");
        }
    }
}