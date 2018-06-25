using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DiadProject.Panel
{
    public partial class homepageslider : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUser();
                getslides();
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
                    //namesurname.Text = namesurname2.Text = db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[0];
                    DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1], "", "ConString");
                    if (dr["usr_rank"].ToString() == "Admin")
                    {
                        //foradmin.Visible = true;
                        //fornormal.Visible = true;
                    }
                    else if (dr["usr_rank"].ToString() == "Normal")
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

        void getslides()
        {
            paginator.DataSource = db.getDataTable("select * from tbl_homepageslider h inner join tbl_users u on h.HS_AddedAdmin = u.usr_user_id where HS_status = '1' order by HS_AddedDateTime desc", "ConString").DefaultView;
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
                Response.Redirect("edithpslide.aspx?secretkey=" + parameter);
            }
            else if (e.CommandName == "delete")
            {
                string usaid = Guid.NewGuid().ToString();
                db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Delete / Veri Silme" + "~" + "Anasayfa slide resmi silindi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                db.updateValueWhere1("tbl_homepageslider", "hs_status", "0", "HS_ID", "=", e.CommandArgument.ToString(), "ConString");
                getslides();
            }
        }

    }
}