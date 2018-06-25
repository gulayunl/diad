using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class homepageslidersort : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        int selected;
        ListItem value;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUser();
                getListboxItems();
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

        void getListboxItems()
        {
            siralama.Text = "";
            SqlDataReader dr = db.getDataReader("select * from tbl_homepageslider where HS_Status = '1' order by HS_Order asc", "ConString");
            while (dr.Read())
            {
                siralama.Text += "<li data-id=\"" + dr["HS_Id"].ToString() + "\" class=\"ui-state-default\"><img src=\"/" + dr["HS_ImageUrl"].ToString() + "\" width=\"150\">" + dr["HS_Text1"].ToString() + " / " + dr["HS_Text2"].ToString() + " / " + dr["HS_BtnText1"].ToString() + " / " + dr["HS_BtnText2"].ToString() + "</li>";
            }
        }

        public static string islembildirimi(bool islemdurumu, string mesaj)
        {
            string sonuc = "";
            if (islemdurumu == true)
            {
                sonuc = "<div class=\"alert alert-success-fill alert-dismissible fade in\" role=\"alert\">"
                      + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">"
                      + "<span aria-hidden=\"true\">×</span>"
                      + "</button>"
                      + "<strong>" + mesaj + "</div>";
            }
            else
            {
                sonuc = "<div class=\"alert alert-danger-fill alert-dismissible fade in mb-0\" role=\"alert\">"
                      + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">"
                      + "<span aria-hidden=\"true\">×</span>"
                      + "</button>"
                      + "<strong>" + mesaj + "</div>";
            }
            return sonuc;
        }
    }
}