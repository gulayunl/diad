using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class member : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUser();
                getmembers();
            }
        }

        void checkUser()
        {
            if (Request.Cookies["Diad"] != null)
            {
                if (db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())) == "false")
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //Label namesurname = (Label)this.Page.Master.FindControl("namesurname");
                    //Label namesurname2 = (Label)this.Page.Master.FindControl("namesurname2");
                    //namesurname.Text = namesurname2.Text = db.checkCookie(db.Decrypt(Request.Cookies["Mercedes"]["SessId"].ToString())).Split(',')[0];
                    DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1], "", "ConString");
                    if (dr["usr_rank"].ToString() == "Admin")
                    {
                        //foradmin.Visible = true;
                        //fornormal.Visible = true;
                    }
                    else if (dr["usr_rank"].ToString() == "Marketing")
                    {
                        Response.Redirect("/");
                    }
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        void getmembers()
        {
            paginator.DataSource = db.getDataTable("select * from tbl_users where usr_status = '1' order by usr_name asc", "ConString").DefaultView;
            paginator.BindToControl = rptmembers;
            rptmembers.DataSource = paginator.DataSourcePaged;
            rptmembers.DataBind();
        }

       

        protected void rptmembers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                Random rnd = new Random();
                string parameter = db.Encrypt(e.CommandArgument.ToString() + "|" + rnd.Next(1111, 9999)).ToString();
                Response.Redirect("editmember.aspx?secretkey=" + parameter);
            }
            else if (e.CommandName == "delete")
            {
                DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", e.CommandArgument.ToString(), "", "ConString");
                string usaid = Guid.NewGuid().ToString();
                db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Delete / Veri Silme" + "~" + dr["usr_name"].ToString() + " " + dr["usr_surname"].ToString() + " ad soyadlı " + dr["usr_email_address"].ToString() + " email adresili kullanıcı silindi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");


                db.updateValueWhere1("tbl_users", "usr_status,usr_last_update_datetime", "0" + "~é" + DateTime.Now, "usr_user_id", "=", e.CommandArgument.ToString(), "ConString");
                getmembers();
            }
        }
    }
}