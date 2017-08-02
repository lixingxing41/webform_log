using System;
using System.Data.SqlClient;
using System.IO;

namespace HTTPBASE
{
    public partial class Edit : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            remind.Visible = false;
            error_type_1.Visible = false;
            error_type_2.Visible = false;
            if (Session["eid"] != null)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = " select * from Employees where EmployeeID=@eid ";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@eid", Convert.ToInt32(Session["eid"]));

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name.Text = reader["EmployeeName"].ToString();
                            title.Text = reader["Title"].ToString();
                            titleofc.Text = reader["TitleOfCourtesy"].ToString();
                            bdate.Text = reader["BirthDate"].ToString();
                            hdate.Text = reader["HireDate"].ToString();
                            addr.Text = reader["Address"].ToString();
                            hphone.Text = reader["HomePhone"].ToString();
                            exten.Text = reader["Extension"].ToString();
                            ppath.Text = reader["PhotoPath"].ToString();
                            notes.Text = reader["Notes"].ToString();
                            mid.Text = reader["ManagerID"].ToString();
                            salary.Text = reader["Salary"].ToString();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        ///存檔
        protected void Save(object sender, System.EventArgs e)
        {
            DateTime dt = new DateTime();
            //存取欄位修改值
            if (!string.IsNullOrEmpty(Request.Form["name"])) name.Text = Request.Form["name"];

            if (!string.IsNullOrEmpty(Request.Form["title"])) title.Text = Request.Form["title"];

            if (!string.IsNullOrEmpty(Request.Form["titleofc"])) titleofc.Text = Request.Form["titleofc"];

            if (!string.IsNullOrEmpty(Request.Form["bdate"])) bdate.Text = Request.Form["bdate"];

            if (!string.IsNullOrEmpty(Request.Form["hdate"])) hdate.Text = Request.Form["hdate"];

            if (!string.IsNullOrEmpty(Request.Form["addr"])) addr.Text = Request.Form["addr"];

            if (!string.IsNullOrEmpty(Request.Form["hphone"])) hphone.Text = Request.Form["hphone"];

            if (!string.IsNullOrEmpty(Request.Form["exten"])) exten.Text = Request.Form["exten"];

            if (!string.IsNullOrEmpty(Request.Form["ppath"])) ppath.Text = Request.Form["ppath"];

            if (!string.IsNullOrEmpty(Request.Form["notes"])) notes.Text = Request.Form["notes"];

            if (!string.IsNullOrEmpty(Request.Form["mid"])) mid.Text = Request.Form["mid"];

            if (!string.IsNullOrEmpty(Request.Form["salary"])) salary.Text = Request.Form["salary"];

            //判斷欄位是否為空
            if (string.IsNullOrEmpty(Request.Form["name"]) || string.IsNullOrEmpty(Request.Form["title"]) || string.IsNullOrEmpty(Request.Form["titleofc"]) || string.IsNullOrEmpty(Request.Form["bdate"])
               || string.IsNullOrEmpty(Request.Form["hdate"]) || string.IsNullOrEmpty(Request.Form["addr"]) || string.IsNullOrEmpty(Request.Form["hphone"]) || string.IsNullOrEmpty(Request.Form["exten"])
               || string.IsNullOrEmpty(Request.Form["ppath"]) || string.IsNullOrEmpty(Request.Form["notes"]) || string.IsNullOrEmpty(Request.Form["mid"]) || string.IsNullOrEmpty(Request.Form["salary"]))
            {
                remind.Visible = true;
            }
            else
            {
                //判斷時間格式是否正確
                if (DateTime.TryParse(Request.Form["bdate"], out dt) == false)
                {
                    error_type_1.Visible = true;
                }
                if (DateTime.TryParse(Request.Form["hdate"], out dt) == false)
                {
                    error_type_2.Visible = true;
                }
                else
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnection"].ToString();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string queryString = "";
                        string logpath = Server.MapPath("~/Log/");
                        string txt = "";
                        if (Session["eid"] != null)
                        { //編輯
                            queryString = " update Employees set EmployeeName=@EmployeeName, Title=@Title, TitleOfCourtesy=@TitleOfCourtesy, " +
                                          " BirthDate=@BirthDate, HireDate=@HireDate, Address=@Address, HomePhone=@HomePhone, Extension=@Extension, " +
                                          " PhotoPath=@PhotoPath, Notes=@Notes, ManagerID=@ManagerID, Salary=@Salary where EmployeeID=@eid";
                            logpath += "EDIT_LOG_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                            txt = Convert.ToInt32(Session["eid"]) + " 修改時間 " + DateTime.Now.ToString();
                        }
                        else
                        { //新增
                            queryString = " insert into Employees(EmployeeName,Title,TitleOfCourtesy,BirthDate,HireDate,Address," +
                                             " HomePhone,Extension,PhotoPath,Notes,ManagerID,Salary) values(@EmployeeName,@Title,@TitleOfCourtesy," +
                                             " @BirthDate,@HireDate,@Address,@HomePhone,@Extension,@PhotoPath,@Notes,@ManagerID,@Salary)";
                            logpath += "ADD_LOG_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                            txt = (string)Request.Form["name"] + " 新增時間 " + DateTime.Now.ToString();
                        }
                        SqlCommand command = new SqlCommand(queryString, connection);
                        command.Parameters.AddWithValue("@eid", Convert.ToInt32(Session["eid"]));
                        command.Parameters.AddWithValue("@EmployeeName", (string)Request.Form["name"]);
                        command.Parameters.AddWithValue("@Title", (string)Request.Form["title"]);
                        command.Parameters.AddWithValue("@TitleOfCourtesy", (string)Request.Form["titleofc"]);
                        command.Parameters.AddWithValue("@BirthDate", DateTime.Parse(Request.Form["bdate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@HireDate", DateTime.Parse(Request.Form["hdate"]).ToShortDateString());
                        command.Parameters.AddWithValue("@Address", (string)Request.Form["addr"]);
                        command.Parameters.AddWithValue("@HomePhone", (string)Request.Form["hphone"]);
                        command.Parameters.AddWithValue("@Extension", (string)Request.Form["exten"]);
                        command.Parameters.AddWithValue("@PhotoPath", (string)Request.Form["ppath"]);
                        command.Parameters.AddWithValue("@Notes", (string)Request.Form["notes"]);
                        command.Parameters.AddWithValue("@ManagerID", Convert.ToInt32(Request.Form["mid"]));
                        command.Parameters.AddWithValue("@Salary", Convert.ToInt32(Request.Form["salary"]));
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
                    Session.Remove("eid");
                    Response.Redirect("Default.aspx");
                }
            }
        }

        ///取消
        protected void Cancel(object sender, System.EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}