using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class addhpslide : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUser();
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
    }
}