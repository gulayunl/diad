using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DiadProject
{
    public partial class Login : System.Web.UI.Page
    {
        dbLibrary db = new dbLibrary();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Diad"] != null)
            {
                DataRow dr = db.getValueWhere1("select * from tbl_users", "usr_lastsession_id", "=", db.Decrypt(Request.Cookies["Diad"]["SessId"].ToString()), "", "ConString");
                if (dr != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            DataRow dr2 = db.getValueWhere3("select count(log_id) as count from tbl_login_logs", "log_IP", "=", db.getIPAdress(""), "and", "log_datetime", ">", DateTime.Now.AddDays(-1).ToString("MM.dd.yyyy hh:mm"), "and", "log_status", "=", "0", "", "ConString");
            if (dr2 != null)
            {
                if (Convert.ToInt32(dr2["count"].ToString()) >= 5)
                {
                    Response.Redirect("/" + "error.aspx?code=403");
                }
            }
        }

  

        protected void girisyap_Click1(object sender, EventArgs e)
        {
            DataRow dr = db.getValueWhere3("select * from tbl_users", "usr_email_address", "=", db.clr(exampleInputEmail.Text), "and", "usr_pass", "=", db.Encrypt(exampleInputPassword.Text), "and", "usr_status", "=", "1", "", "ConString");
            if (dr != null)
            {
                string sessionID, ual;
                sessionID = Guid.NewGuid().ToString();
                ual = Guid.NewGuid().ToString();

                db.updateValueWhere1("tbl_users", "usr_lastlogin_date,usr_lastsession_id", "é" + DateTime.Now + "~" + sessionID.ToString(), "usr_user_id", "=", dr["usr_user_id"].ToString(), "ConString");

                string logid = Guid.NewGuid().ToString();
                db.insertValue("tbl_login_logs", "log_id,usr_user_id,log_IP,log_datetime,log_status", logid + "~" + dr["usr_user_id"].ToString() + "~" + db.getIPAdress("") + "~é" + DateTime.Now + "~" + "1", "ConString");

                HttpCookie adminCookie = new HttpCookie("Diad");
                adminCookie["SessID"] = db.Encrypt(sessionID.ToString());
                adminCookie["Name"] = dr["usr_name"].ToString() + " " + dr["usr_surname"].ToString();
                adminCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(adminCookie);
                Response.Redirect("Default.aspx");
            }
            else
            {
                DataRow dr2 = db.getValueWhere2("select * from tbl_users", "usr_email_address", "=", db.clr(exampleInputEmail.Text), "and", "usr_status", "=", "1", "", "ConString");
                if (dr2 != null)
                {
                    string logid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_login_logs", "log_id,usr_user_id,log_IP,log_datetime,log_status", logid + "~" + dr2["usr_user_id"].ToString() + "~" + db.getIPAdress("") + "~é" + DateTime.Now + "~" + "0", "ConString");
                    DataRow dr3 = db.getValueWhere3("select count(log_id) as count from tbl_login_logs", "usr_user_id", "=", dr2["usr_user_id"].ToString(), "and", "log_datetime", ">", DateTime.Now.AddDays(-1).ToString("MM.dd.yyyy hh:mm"), "and", "log_status", "=", "0", "", "ConString");
                    if (Convert.ToInt32(dr3["count"].ToString()) >= 5)
                    {
                        db.updateValueWhere1("tbl_users", "usr_status", "0", "usr_user_id", "=", dr2["usr_user_id"].ToString(), "ConString");
                    }
                }
                else
                {
                    string logid = Guid.NewGuid().ToString();
                    db.insertValue("tbl_login_logs", "log_id,log_IP,log_datetime,log_status", logid + "~" + db.getIPAdress("") + "~é" + DateTime.Now + "~" + "0", "ConString");
                }
                Label1.Text = "Kullanıcı adınız veya şifreniz hatalı.";
            }
        }
    }
}