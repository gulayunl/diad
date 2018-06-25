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
    public partial class aboutus : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (!IsPostBack)
                {
                    checkUser();

                    SqlDataReader dr = db.getDataReader("SELECT * FROM tbl_AboutUs", "ConString");

                    while (dr.Read())
                    {
                        editor.Text += dr["Description"].ToString();
                    }

                    dr.Close();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (editor.Text.Length < 50)
            {
                Literal1.Text = islembildirimi(false, "<b>En az 50 karakterlik açıklama yapınız</h4>");
                return;
            }

            string result;
            try
            {
                result = db.updateValueWhere1("tbl_AboutUs", "Description", editor.Text, "Id", "=", "1", "ConString");
                Response.Redirect("Default.aspx");

            }
            catch (Exception)
            {
                Literal1.Text = "<br/><h4><b>Düzeltilirken hata oluştu</b></h4>";
                return;
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