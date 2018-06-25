using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class editmember : System.Web.UI.Page
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
                    DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.Decrypt(Request.QueryString["secretkey"].ToString().Replace(" ", "+")).Split('|')[0].ToString(), "", "ConString");
                    ad.Text = dr["usr_name"].ToString();
                    soyad.Text = dr["usr_surname"].ToString();
                    email.Text = dr["usr_email_address"].ToString();
                    telefon.Text = dr["usr_phone"].ToString();
                    rol.SelectedValue = dr["usr_rank"].ToString();
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

        protected void kaydet_Click(object sender, EventArgs e)
        {
            if (db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())) == "false")
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                if (ad.Text.Length < 2)
                {
                    islemsonucu.Text = islembildirimi(false, "Lütfen geçerli bir ad giriniz.");
                    return;
                }
                if (soyad.Text.Length < 2)
                {
                    islemsonucu.Text = islembildirimi(false, "Lütfen geçerli bir soyad giriniz.");
                    return;
                }
                if (email.Text.Length < 6 || email.Text.Contains("@") == false || email.Text.Contains(".") == false)
                {
                    islemsonucu.Text = islembildirimi(false, "Lütfen geçerli bir mail adresi giriniz.");
                    return;
                }
                if (telefon.Text.Length < 11)
                {
                    islemsonucu.Text = islembildirimi(false, "Lütfen geçerli bir telefon numarası giriniz.");
                    return;
                }

                string result;
                result = db.updateValueWhere1("tbl_users", "usr_name,usr_surname,usr_email_address,usr_phone,usr_rank", db.clr(ad.Text) + "~" + db.clr(soyad.Text) + "~" + db.clr(email.Text) + "~" + db.clr(telefon.Text) + "~" + rol.SelectedItem.Value, "usr_user_id", "=", db.Decrypt(Request.QueryString["secretkey"].ToString().Replace(" ", "+")).Split('|')[0].ToString(), "ConString");


                DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.Decrypt(Request.QueryString["secretkey"].ToString().Replace(" ", "+")).Split('|')[0].ToString(), "", "ConString");
                string usaid = Guid.NewGuid().ToString();
                db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Edit / Veri Düzenleme" + "~" + dr["usr_name"].ToString() + " " + dr["usr_surname"].ToString() + ", " + dr["usr_email_address"].ToString() + ", " + dr["usr_phone"].ToString() + " bilgilerine sahip kullanıcı " + ad.Text + " " + soyad.Text + ", " + email.Text + ", " + telefon.Text + " olarak düzenlendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

                // insert user action logs
                if (result.Contains("true") == true)
                {
                    islemsonucu.Text = islembildirimi(true, "Üyelik başarıyla düzenlenmiştir.");
                }
                else
                {
                    islemsonucu.Text = islembildirimi(false, "Üyelik düzenlenirken bir hata oluştu. Lütfen site yöneticisi ile iletişime geçiniz.");
                }
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