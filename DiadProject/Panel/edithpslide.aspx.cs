using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class edithpslide : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["secretkey"] == null) Response.Redirect("default.aspx");
                else
                {
                    checkUser();
                    DataRow dr = db.getValueWhere1("select * from tbl_homepageslider", "HS_ID", "=", db.Decrypt(Request.QueryString["secretkey"].ToString().Replace(" ", "+")).Split('|')[0].ToString(), "", "ConString");
                    mevcutslideresmi.ImageUrl = "/" + dr["HS_ImageUrl"].ToString();
                    anaslogan.Text = dr["HS_Text1"].ToString();
                    altslogan.Text = dr["HS_Text2"].ToString();
                    btnfaiz.Text = dr["HS_BtnText1"].ToString();
                    btnslogan.Text = dr["HS_BtnText2"].ToString();
                    yazirengi.SelectedValue = dr["HS_TextColor"].ToString();
                    editbtn.Text = "<a class=\"btn btn-primary w-min-sm mb-1-00 waves-effect waves-light\" style=\"color: #fff;\" onclick=\"edithpslide('" + db.Decrypt(Request.QueryString["secretkey"].ToString().Replace(" ", "+")).Split('|')[0].ToString() + "');\">Kaydet</a>";
                    link.Text = dr["HS_Link"].ToString();
                    yonlendirmeturu.SelectedValue = dr["HS_RedirectType"].ToString();
                }
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