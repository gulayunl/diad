using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class addmember : System.Web.UI.Page
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

                DataRow dr = db.getValueWhere3("select usr_email_address from tbl_users", "usr_status", "=", "1", "and", "usr_email_address", "=", email.Text, "or", "usr_phone", "=", telefon.Text, "", "ConString");
                if (dr != null)
                {
                    islemsonucu.Text = islembildirimi(false, "Bu email adresi yada telefon numarasına kayıtlı üye bulunmaktadır.");
                    return;
                }

                string memberid = Guid.NewGuid().ToString();
                string code = "";
                Random rnd = new Random();
                code = db.MD5(memberid + "~" + email.Text + "~" + telefon.Text + "~" + DateTime.Now.ToString() + "~" + rnd.Next(1111, 9999));
                string result = db.insertValue("tbl_users", "usr_user_id,usr_name,usr_surname,usr_email_address,usr_phone,usr_regist_date,usr_status,usr_rank,usr_code,usr_codeuse,usr_pass", memberid + "~" + db.clr(ad.Text) + "~" + db.clr(soyad.Text) + "~" + db.clr(email.Text) + "~" + db.clr(telefon.Text) + "~é" + DateTime.Now + "~" + "1" + "~" + rol.SelectedItem.Value + "~" + code + "~" + "0" + "~"+string.Empty, "ConString");

                string usaid = Guid.NewGuid().ToString();
                db.insertValue("tbl_users_action_logs", "usa_log_id,usa_action_type,usa_page_type,usa_operation_date,usa_usr_user_id,usa_status", usaid + "~" + "Insert / Veri Ekleme" + "~" + ad.Text + " " + soyad.Text + " ad soyadlı " + email.Text + " emaili ile yeni yönetici eklendi." + "~é" + DateTime.Now + "~" + db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1] + "~" + "1", "ConString");

               

                if (result.Contains("true") == true)
                {
                    islemsonucu.Text = islembildirimi(true, "Üye başarıyla eklenmiştir.");
                    ad.Text = soyad.Text = telefon.Text = email.Text = "";
                }
                else
                {
                    islemsonucu.Text = islembildirimi(false, "Üye eklenirken bir hata oluştu. Lütfen sistem yöneticisi ile iletişime geçiniz.");
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