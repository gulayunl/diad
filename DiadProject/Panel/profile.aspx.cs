using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject.Panel
{
    public partial class profile : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();
        }

        protected void kaydet_Click(object sender, EventArgs e)
        {
            if (db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())) == "false")
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_user_id", "=", db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1], "", "ConString");
                bool buyukharf = false, kucukharf = false, rakam = false, ozel = false;
                for (int i = 0; i < newPassword.Text.Length; i++)
                {
                    char ch = newPassword.Text[i];
                    if (ch >= 'A' && ch <= 'Z')
                    {
                        buyukharf = true;
                    }
                    if (ch >= 'a' && ch <= 'z')
                    {
                        kucukharf = true;
                    }
                    if (Char.IsNumber(ch))
                    {
                        rakam = true;
                    }
                    if (Char.IsPunctuation(ch))
                    {
                        ozel = true;
                    }
                }

                if (buyukharf == false || kucukharf == false || rakam == false || ozel == false || newPassword.Text.Length < 10)
                {
                    islemsonucu.Text = islembildirimi(false, "Şifreniz en az 10 karakter olmalı, en az 1 büyük harf, 1 küçük harf, 1 rakam ve 1 özel karakter içermelidir.");
                    return;
                }
                if (db.Encrypt(currentPassword.Text) != dr["usr_pass"].ToString())
                {
                    islemsonucu.Text = islembildirimi(false, "Mevcut şifrenizi hatalı girdiniz.");
                    return;
                }
                if (newPassword.Text != newPasswordAgain.Text)
                {
                    islemsonucu.Text = islembildirimi(false, "Yeni şifreleriniz uyuşmuyor.");
                    return;
                }
                string result = db.updateValueWhere1("tbl_users", "usr_pass", db.Encrypt(newPassword.Text), "usr_user_id", "=", db.checkCookie(db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString())).Split(',')[1], "ConString");

                if (result.Contains("true") == true)
                {
                    islemsonucu.Text = islembildirimi(true, "Şifreniz başarıyla güncellenmiştir.");
                    return;
                }
                else
                {
                    islemsonucu.Text = islembildirimi(true, "Şifre düzenlenirken bir hata oluştu. Lütfen site yöneticisi ile iletişime geçiniz.");
                    return;
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